using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public Transform gfx;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent <Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // invoke this to update the path when the player is moving
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        else
        {
            reachedEndOfPath = false;
        }

        // move sprite to next waypoint
        var direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        var force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }

        // based on direction of bird flip the transform
        // traveling to the right
        if (force.x >= 0.01f)
        {
            gfx.localScale = new Vector3(-1f, 1f, 1f);
        }
        // traveling to the left
        else if (force.x <= -0.01f)
        {
            gfx.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
