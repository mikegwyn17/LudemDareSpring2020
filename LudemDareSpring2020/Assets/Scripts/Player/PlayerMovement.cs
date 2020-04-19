using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller;
        public float moveSpeed = 40f;
        float _horizontalMove = 0f;
        bool _isJumping = false;
        State playerState = State.Normal;

        //For swinging
        private Vector3 spitTargetPosition;
        private Vector3 dirToTarget;
        private float spitTravelSpeed = 20f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch (playerState)
            {
                case State.Normal:
                    CheckForJumping();
                    CheckForSpitSwingStart();
                    break;
                case State.Jumping:
                    break;
                case State.SpitSwinging:
                    break;
                case State.SpitSwingStart:
                    break;
                case State.SpitSwingTravel:
                    break;
            }

            // get values for movement in update but don't apply them
            _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

            
        }

        private void FixedUpdate()
        {
            switch (playerState)
            {
                case State.Normal:
                    break;
                case State.Jumping:
                    break;
                case State.SpitSwinging:
                    HandleSpitSwinging();
                    break;
                case State.SpitSwingStart:
                    break;
                case State.SpitSwingTravel:
                    HandleSpitSwingTravel();
                    break;
            }

            controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _isJumping);
            _isJumping = false;

            if(playerState != State.SpitSwinging)
            {
                playerState = State.Normal;
            }
            
        }

        private void CheckForJumping()
        {
            if (Input.GetButton("Jump"))
            {
                _isJumping = true;
                playerState = State.Jumping;
            }
        }
        

        private void CheckForSpitSwingStart()
        {
            if (playerState != State.SpitSwinging && playerState != State.SpitSwingTravel && Input.GetMouseButtonDown(0))
            {
                SpitSwing ss = new SpitSwing();
                spitTargetPosition = GetMouseWorldPosition();
                dirToTarget = (spitTargetPosition - GetPosition()).normalized;
                spitTravelSpeed = 20f;
                playerState = State.SpitSwinging;
            }

        }

        private void HandleSpitSwinging()
        {
            transform.position += dirToTarget * Time.deltaTime * spitTravelSpeed;
            if (Vector3.Distance(GetPosition(), spitTargetPosition) < 2f)
            {
                playerState = State.SpitSwingTravel;
            }
        }

        private void HandleSpitSwingTravel()
        {
            spitTravelSpeed -= spitTravelSpeed * Time.deltaTime;
            transform.position += dirToTarget * Time.deltaTime * spitTravelSpeed;
            if(spitTravelSpeed < 0.5f)
            {
                playerState = State.Normal;
            }
        }

        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
