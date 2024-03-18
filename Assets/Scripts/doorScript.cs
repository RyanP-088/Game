using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public float openAngle = 90f;  // 90 degrees rotation
    public float rotationSpeed = 60f;  // degrees per second, adjust as needed
    private float currentAngle = 0f;  // Track the current angle of the door

    // This variable determines the target rotation (either open or closed)
    private float targetAngle = 0f;

    private void Update()
    {
        // Rotate the door towards its target angle
        if (currentAngle != targetAngle)
        {
            // Determine the direction of rotation based on the target
            float direction = Mathf.Sign(targetAngle - currentAngle);
            currentAngle += direction * rotationSpeed * Time.deltaTime;

            // Clamp the currentAngle to avoid overshooting
            currentAngle = Mathf.Clamp(currentAngle, 0, openAngle);

            transform.rotation = Quaternion.Euler(0, 0, -currentAngle);
        }
    }

    public void OpenDoor()
    {
        targetAngle = openAngle;
    }

    public void CloseDoor()
    {
        targetAngle = 0f;
    }
}