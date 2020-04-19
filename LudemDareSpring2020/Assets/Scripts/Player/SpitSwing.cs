using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class SpitSwing : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var camelHeadPos = transform.Find("camelHead");
            //camelHeadPos.eulerAngles += new Vector3(0, 0, 0.1f);
        }

        public Vector3 GetCamelHeadPosition()
        {
            var camelHeadPos = transform.Find("camelHead");
            return camelHeadPos.position;
        }
    }
}

