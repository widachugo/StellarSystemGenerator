using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public Transform target;

    public float speed;

    void Update()
    {
        gameObject.transform.RotateAround(target.position, Vector3.down, speed * Time.deltaTime);
    }
}
