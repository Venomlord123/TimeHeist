using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachFrench
{
    public class FPSCamera : MonoBehaviour
    {
        //References 
        public Transform player;

        //Variables 
        public float mouseX;
        public float mouseY;
        public float mouseSensitivity;
        public float xRotation;

        // Update is called once per frame
        void Update()
        {
            //Setting the mouse Axis's
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            //Setting Rotation for y axis 
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }
    }
}
