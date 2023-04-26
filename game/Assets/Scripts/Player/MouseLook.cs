using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 500;
    public Transform playerBody;
    private float _xRotation = 0; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -60, 60);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
