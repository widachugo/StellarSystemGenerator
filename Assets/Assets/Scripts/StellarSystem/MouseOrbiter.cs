using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbiter : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public void Update()
    {
        target = FindObjectOfType<CameraControl>().target;
    }

    void LateUpdate()
    {
        if (target)
        {
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            if (Input.GetButton("Fire2"))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.005f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.005f;
            }

            //x += Input.GetAxis("Horizontal_Joy_R") * xSpeed * 0.005f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            distance = Mathf.Clamp(distance * 5, distanceMin * (target.transform.localScale.x * 5), distanceMax * (target.transform.localScale.x * 5));
            //distance = Mathf.Clamp(distance - Input.GetAxis("Vertical_Joy_R") * 5, distanceMin * (target.transform.localScale.x * 5), distanceMax * (target.transform.localScale.x * 5));

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

