using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Slider sliderTimeUI;

    private SystemSolarGenerator systemSolarGenerator;

    public void Start()
    {
        systemSolarGenerator = GameObject.FindObjectOfType<SystemSolarGenerator>();
    }

    public void GenerateStellarSystem()
    {
        systemSolarGenerator.Generate();
    }

    public void ClearStellarSystem()
    {
        systemSolarGenerator.ClearSolarSystem();
    }

    public void ChangeTime()
    {
        systemSolarGenerator.sliderTime = sliderTimeUI.value;
    }

    public void StellarSystemView()
    {
        systemSolarGenerator.NullTarget();
    }
}
