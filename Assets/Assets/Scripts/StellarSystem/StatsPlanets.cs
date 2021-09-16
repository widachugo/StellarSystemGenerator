using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlanets : MonoBehaviour
{
    public float speedTurnAroundSun;
    public float speedTurnAroundLocal;

    public float distanceStar;
    public float distanceCamera;

    public enum PlanetType { Telluric, Gas }
    public PlanetType planetType;

    public float temp;

    private LineRenderer line;
    private GameObject goLine;

    public void Start()
    {
        int rdmNmb = Random.Range(0, 100);
        if (rdmNmb >= 35)
        {
            planetType = PlanetType.Telluric;
        }
        else if (rdmNmb < 35)
        {
            planetType = PlanetType.Gas;
        }

        GameObject star = GameObject.FindGameObjectWithTag("Star");

        distanceStar = Vector3.Distance(transform.position, star.transform.position);

        speedTurnAroundSun = Mathf.Exp(distanceStar * -0.1f) * 1000;

        Vector2 vector = Random.insideUnitCircle.normalized * distanceStar;
        transform.position = new Vector3(vector.x, 0, vector.y);

        goLine = new GameObject();
        goLine.name = "LineRenderer" + gameObject.name;
        goLine.gameObject.transform.parent = gameObject.transform;

        DrawCircle(distanceStar, 0.1f);

        SystemSolarGenerator systemSolarGenerator = FindObjectOfType<SystemSolarGenerator>();

        GameObject goSprite = new GameObject();
        goSprite = Instantiate(FindObjectOfType<SystemSolarGenerator>().spriteTargetPlanet, transform.position, transform.rotation);
        goSprite.gameObject.transform.parent = gameObject.transform;

        //Renderer
        Material material = new Material(Shader.Find("Standard"));
        material.SetFloat("_Metallic", 0.98f);
        material.SetFloat("_Glossiness", 0.0f);

        if (planetType == PlanetType.Telluric)
        {
            material.SetTexture("_MainTex", systemSolarGenerator.textureTelluricPlanet[Random.Range(0, systemSolarGenerator.textureTelluricPlanet.Length)]);
        }
        else
        {
            material.SetTexture("_MainTex", systemSolarGenerator.textureGasPlanet[Random.Range(0, systemSolarGenerator.textureGasPlanet.Length)]);
        }

        gameObject.GetComponent<Renderer>().sharedMaterial = material;
        gameObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void DrawCircle(float radius, float lineWidth)
    {
        int segments = 360;
        line = goLine.AddComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.material = new Material(Shader.Find("Standard"));
        line.material.SetFloat("_Metallic", 0.0f);
        line.material.SetFloat("_Glossiness", 0.0f);
        line.material.SetColor("_Color", Color.grey);

        line.SetPositions(points);
    }

    public void Update()
    {
        distanceCamera = Vector3.Distance(transform.position, FindObjectOfType<Camera>().gameObject.transform.position);

        if (GameObject.FindObjectOfType<CameraControl>().target == null)
        {
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }
}
