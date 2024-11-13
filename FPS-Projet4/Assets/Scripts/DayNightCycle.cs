using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public float EnvironmentIntensity1 = 3.0f;
    public float EnvironmentIntensityStart = 10.0f;
    public float dayTime = 6.0f;
    public float dayTick = 600f;
    public float timeTick = 0.0f;


    public bool start;
    public bool darken = true;
    public bool lighten = false;
    public bool intensity = false;

    GameObject sunMoon;
    GameObject environmentLighting;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = EnvironmentIntensityStart;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (RenderSettings.ambientIntensity > EnvironmentIntensity1 && start == true)
        {
            RenderSettings.ambientIntensity -= 0.01f;
            if (RenderSettings.ambientIntensity == EnvironmentIntensity1)
            {
                start = false;

            }
        }
        else
        {
            sunMoon = GameObject.FindWithTag("SunMoon");
            sunMoon.transform.Rotate(0.0f, 0.3f / dayTime, 0.0f);
            //Task.Delay(TimeSpan.FromSeconds(30)).Wait();
            //Task.Delay(1000).ContinueWith(t=> //CodeDeLaSuite);

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
                if (RenderSettings.ambientIntensity >= EnvironmentIntensity1)
                {
                    //environmentLighting = GameObject.FindWithTag("Environment");
                    //environmentLighting.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0) * (-5f));
                    darken = true;
                    lighten = false;
                    intensity = false;
                }
            }
        }
    }
}
