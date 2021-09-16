using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInfoPlanet : MonoBehaviour
{
    public Color colorHighlight;

    private Color iniColor;
    private Vector3 iniScale;

    void Start()
    {
        iniColor = Color.white;
        iniScale = gameObject.transform.localScale;
    }

    public void OnMouseEnter()
    {
        Debug.Log("ca marche");
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = colorHighlight;
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = iniColor;
    }
}
