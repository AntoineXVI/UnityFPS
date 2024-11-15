using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public float EnvironmentIntensity1 = 3.0f;
    public float EnvironmentIntensityStart = 10.0f;
    public float dayTime = 15.0f;
    private float dayTick = 600f;
    private float timeTick = 0.0f;
    
    private bool start;
    private bool darken = true;
    private bool lighten = false;
    private bool intensity = false;

    public Material environmentLight;
    GameObject sunMoon;
    

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = EnvironmentIntensityStart;
        start = true;
        sunMoon = GameObject.FindWithTag("SunMoon");
        environmentLight.SetColor("_EmissionColor", new Color(1, 1, 1) * 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (RenderSettings.ambientIntensity > EnvironmentIntensity1 && start == true)
        {
            RenderSettings.ambientIntensity -= 0.025f;
            if (RenderSettings.ambientIntensity <= EnvironmentIntensity1)
            {
                start = false;
            }
        }
        else
        {
            rotateSun();

            if (timeTick < dayTime * dayTick)
            {
                timeTick++;
            }
            else
            {
                intensity = true;
                timeTick = 0f;
            }
            if (RenderSettings.ambientIntensity >= 0 && darken == true && intensity == true)
            {
                RenderSettings.ambientIntensity -= 0.01f;
                environmentLight.SetColor("_EmissionColor", new Color(1, 1, 1) * -10f);
                if (RenderSettings.ambientIntensity <= 0f)
                {
                    darken = false;
                    lighten = true;
                    intensity = false;
                }
            }
            if (RenderSettings.ambientIntensity <= EnvironmentIntensity1 && lighten == true && intensity == true)
            {
                RenderSettings.ambientIntensity += 0.01f;
                environmentLight.SetColor("_EmissionColor", new Color(1 ,1 ,1) * 4f);
                if (RenderSettings.ambientIntensity >= EnvironmentIntensity1)
                {
                    darken = true;
                    lighten = false;
                    intensity = false;
                }
            }
        }
    }

    //à Rajouter une Coroutine pour optimiser le code
    public void rotateSun()
    {
        sunMoon.transform.Rotate(0.0f, 0.3f / dayTime, 0.0f);
    }
}
