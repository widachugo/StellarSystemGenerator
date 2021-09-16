using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemSolarGenerator : MonoBehaviour
{
    [HideInInspector] public float scaleStarMin;
    [HideInInspector] public float scaleStarMax;

    [HideInInspector] public float scalePlanetMin;
    [HideInInspector] public float scalePlanetMax;

    [HideInInspector] [Range(0, 1)] public int nbPlanetMin;
    [HideInInspector] [Range(1, 10)] public int nbPlanetMax;

    [HideInInspector] public Color colorStar;

    [HideInInspector] [Range(0.001f, 0.1f)] public float sliderTime;

    private GameObject star;
    public List<GameObject> planets = new List<GameObject>();

    public GameObject spherePrefab;
    public GameObject spriteTargetPlanet;

    //Textures
    public Texture[] textureTelluricPlanet;
    public Texture[] textureGasPlanet;
    public Texture noiseStar;

    public void ClearSolarSystem()
    {
        DestroyImmediate(star);

        for (int i = 0; i < planets.Count; i++)
        {
            DestroyImmediate(planets[i]);
        }
        planets.Clear();
    }

    public void Generate()
    {
        if (star == null)
        {
            GenerateStar();
        }
        else
        {
            DestroyImmediate(star);
            GenerateStar();
        }

        if (planets == null)
        {
            GeneratePlanets();
        }
        else
        {
            for (int i = 0; i < planets.Count; i++)
            {
                DestroyImmediate(planets[i]);
            }
            planets.Clear();
            GeneratePlanets();
        }
    }

    private void GenerateStar()
    {
        star = Instantiate(spherePrefab, transform.position, transform.rotation);
        star.name = "Star";
        star.tag = "Star";

        //Transform Scale du soleil
        star.transform.position = Vector3.zero;
        float thisScaleStar = Random.Range(scaleStarMin, scaleStarMax);
        star.transform.localScale = new Vector3(thisScaleStar, thisScaleStar, thisScaleStar);

        star.transform.eulerAngles = new Vector3(90, 0, 0);

        //Renderer du soleil
        Material material = new Material(Shader.Find("ShaderDrill/0327/Fresnel"));
        material.SetTexture("_MainTex", noiseStar);
        material.color = colorStar;
        material.SetColor("_FresnelColor", Color.black);
        material.SetFloat("_FresnelBias", -0.1f);
        material.SetFloat("_FresnelScale", 0.8f);
        material.SetFloat("_FresnelPower", 1.2f);
        star.GetComponent<Renderer>().sharedMaterial = material;

        //Light
        Light starLight = star.AddComponent<Light>();
        starLight.range = 1000;
        starLight.intensity = 2;
        starLight.shadows = LightShadows.Hard;
        starLight.cullingMask = 1 << LayerMask.NameToLayer("Planet");
    }

    private void GeneratePlanets()
    {
        int thisNbPlanet = Random.Range(1, nbPlanetMax);

        for (int i = 0; i < thisNbPlanet; i++)
        {
            planets.Add(Instantiate(spherePrefab, transform.position, transform.rotation));
            planets[i].name = "Planet" + i;
            planets[i].layer = 9;

            //Scale de la planète
            float thisScalePlanet = Random.Range(scalePlanetMin, scalePlanetMax);
            planets[i].transform.localScale = new Vector3(thisScalePlanet, thisScalePlanet, thisScalePlanet);

            //Transform de la planète
            if (i == 0)
            {
                planets[i].transform.position = new Vector3(star.transform.localScale.x * 2 + planets[i].transform.localScale.x / 2 + Random.Range(1.5f, 3.0f), 0, 0);
            }
            else if (i > 0)
            {
                planets[i].transform.position = new Vector3(planets[i-1].transform.position.x + planets[i-1].transform.localScale.x / 2 + planets[i].transform.localScale.x / 2 + Random.Range(6.0f, 25.0f), 0, 0);
            }

            planets[i].transform.localRotation = Quaternion.Euler(90, planets[i].transform.rotation.y, planets[i].transform.rotation.z);

            planets[i].gameObject.AddComponent<StatsPlanets>();
        }
    }

    public void Update()
    {
        if (star != null)
        {
            star.GetComponent<Renderer>().sharedMaterial.color = colorStar;
        }

        if (planets != null)
        {
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].transform.RotateAround(Vector3.zero, Vector3.up, (planets[i].GetComponent<StatsPlanets>().speedTurnAroundSun * Time.deltaTime) * sliderTime);
            }
        }
    }

    public void NullTarget()
    {
        GameObject.FindObjectOfType<CameraControl>().target = null;
    }

    public void OnGUI()
    {
        GUILayout.Space(650);
        for (int i = 0; i < planets.Count; i++)
        {
            if (GUILayout.Button(planets[i].name, GUILayout.Width(150), GUILayout.Height(30)))
            {
                GameObject.FindObjectOfType<CameraControl>().ChangeTarget(planets[i].transform);
            }
        }
    }
}
