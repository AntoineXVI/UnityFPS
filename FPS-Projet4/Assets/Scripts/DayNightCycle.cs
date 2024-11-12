using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public float EnvironmentIntensity1 = 3.0f;
    public float EnvironmentIntensity2 = 10.0f;
    public float dayTime = 30.0f;


    bool start;
    bool darken;
    bool lighten;

    GameObject sunMoon;
    GameObject environmentLighting;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = EnvironmentIntensity2;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (RenderSettings.ambientIntensity > EnvironmentIntensity1 && start == true)
        {
            RenderSettings.ambientIntensity -= 0.02f;
        }
        if (RenderSettings.ambientIntensity == EnvironmentIntensity1)
        {
            start = false;

        }
        else
        {

            sunMoon = GameObject.FindWithTag("SunMoon");
            sunMoon.transform.Rotate(0.0f, 5f / dayTime, 0.0f);
            if (RenderSettings.ambientIntensity >= 0 && darken == true)
            {
                RenderSettings.ambientIntensity -= 0.1f / dayTime;
                lighten = true;
                darken = false;
            }
            else if (RenderSettings.ambientIntensity <= 0 && lighten == true)
            {

                
            }
            //environmentLighting = GameObject.FindWithTag("Environment");
            //environmentLighting.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0,0,0) * (0.1f));
        }
    }
}
