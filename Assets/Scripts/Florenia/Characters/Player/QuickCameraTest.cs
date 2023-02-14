using System;
using UnityEngine;

namespace Florenia.Characters.Player
{
    public class QuickCameraTest : MonoBehaviour
    {
        public float Speed = 10f; 
        
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0,Speed * Time.deltaTime,0);
            }else if (Input.GetKey(KeyCode.S))
            {
                transform.position -= new Vector3(0,Speed * Time.deltaTime,0);
            }else if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(Speed * Time.deltaTime,0,0);
            }else if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(Speed * Time.deltaTime,0,0);
            }
        }
    }
}
