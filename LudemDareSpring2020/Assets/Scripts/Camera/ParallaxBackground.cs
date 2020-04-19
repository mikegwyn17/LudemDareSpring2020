using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{

    public class ParallaxBackground : MonoBehaviour
    {
        private Transform cameraTransform;
        public GameObject player;        //Public variable to store a reference to the player game object
        private Vector3 lastCameraPosition;
        [SerializeField] private float parallaxEffectMultiplier;

        private void Start()
        {
            cameraTransform = player.transform;
            lastCameraPosition = cameraTransform.position;

        }

        private void LateUpdate()
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

            transform.position += deltaMovement * parallaxEffectMultiplier;
            lastCameraPosition = cameraTransform.position;
        }
    }
}