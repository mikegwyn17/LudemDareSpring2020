using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class EggController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Constants.GROUND_TAG))
            {
                // when the egg hits the ground change the collor to red
                var spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.red;
            }
        }
    }
}
