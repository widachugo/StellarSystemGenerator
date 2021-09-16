using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    private GameObject star;

    public float lastPlanet;

    void Start()
    {
        target = null;
    }

    void Update()
    {
        star = GameObject.FindGameObjectWithTag("Star") as GameObject;

        if (target == null)
        {
            target = null;
        }

        if (star == null)
        {
            transform.position = transform.position;
        }
        else if (star != null && target == null)
        {
            StatsPlanets[] planets = GameObject.FindObjectsOfType<StatsPlanets>();
            System.Array.Reverse(planets);

            for (int i = 0; i < planets.Length; i++)
            {
                lastPlanet = Mathf.Max(planets[i].distanceStar);
            }

            gameObject.transform.LookAt(star.transform.position);
        }

        gameObject.transform.position = new Vector3(0, 60, (lastPlanet * 1.5f) * -1);
    }

    public void ChangeTarget(Transform targetSelect)
    {
        target = targetSelect;
    }
}
