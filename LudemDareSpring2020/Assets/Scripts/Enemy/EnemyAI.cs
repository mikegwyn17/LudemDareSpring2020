using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace LD
{
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;

        public float speed = 200f;

        public float nextWaypointDistance = 3f;

        Path path;
        int currentWaypoint = 0;
        bool reachedEndOfPath = false;
        bool _hasColidedWithPlayer = false;

        Seeker seeker;
        Rigidbody2D rb;
        public Transform gfx;

        // used detect if the enemy if block and can no longer move
        bool _isMoving = true;

        Vector3 _previousPosition = new Vector3();

        // Start is called before the first frame update
        void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();

            // invoke this to update the path when the player is moving
            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        void OnPathComplete(Path p)
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

        private void Update()
        {
            _isMoving = Vector3.Distance(rb.transform.position, _previousPosition) >= .0001f;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // enemy has collided with player
            if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            {
                _hasColidedWithPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // enemy and player are no longer colliding
            if (collision.collider.CompareTag(Constants.PLAYER_TAG))
            {
                _hasColidedWithPlayer = false;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 force = new Vector2();

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

            // enemy doesn't seem to be moving and is not stuck on the player
            if (!_isMoving && !_hasColidedWithPlayer)
            {
                force = new Vector2(0f, 100f) * speed * Time.fixedDeltaTime;
            }
            else 
            { 
                force = direction * speed * Time.fixedDeltaTime; 
            }

            // set previous position before applying force
            _previousPosition = rb.transform.position;


            rb.AddForce(force);

            var distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            // figure out how to determine if you hit a an obstacle that can be jumped over

            if (distance < nextWaypointDistance)
            {
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
}