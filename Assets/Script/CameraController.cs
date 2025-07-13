using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Movement speed in units per second
    public float lookSpeed = 2f;       // Mouse look sensitivity

    private float rotationX = 0f;      // Rotation around the X axis (up/down)
    private float rotationY = 0f;      // Rotation around the Y axis (left/right)

    void Start()
    {
        // Lock and hide the cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize rotation based on current camera rotation
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.x;
        rotationY = angles.y;
    }

    void Update()
    {
        // --- CAMERA ROTATION (MOUSE LOOK) ---
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;

        // Clamp vertical rotation so you can't flip the camera upside down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply rotation to camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        // --- CAMERA MOVEMENT (KEYBOARD) ---
        float moveForward = Input.GetAxis("Vertical");   // W/S or Up/Down arrow
        float moveRight = Input.GetAxis("Horizontal");   // A/D or Left/Right arrow

        Vector3 move = transform.forward * moveForward + transform.right * moveRight;
        move *= moveSpeed * Time.deltaTime;

        transform.position += move;
    }
}