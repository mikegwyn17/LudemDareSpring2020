using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float moveSpeed = 40f;
    float _horizontalMove = 0f;
    bool _isJumping = false;

    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        // get values for movement in update but don't aplly them
        _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButton("Jump"))
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _isJumping);
        _isJumping = false;
    }
}
