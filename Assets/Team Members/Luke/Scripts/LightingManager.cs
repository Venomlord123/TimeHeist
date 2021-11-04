using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    //References
    public GameManager gameManager;
    public Timer timer;
    //Variables
    public List<Light> lights;
    public float lightsIntensity;
    public float blackoutDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        lights.AddRange(GetComponentsInChildren<Light>());

        foreach (Light light in lights)
        {
            light.intensity = lightsIntensity;
        }
    }

    private void OnEnable()
    {
        timer.BlackOutEvent += Blackout;
    }

    private void OnDisable()
    {
        timer.BlackOutEvent += Blackout;
    }

    public void Blackout()
    {
        StartCoroutine(BlackoutSequence());
    }

    public IEnumerator BlackoutSequence()
    {
        TurnOffLights();

        yield return new WaitForSeconds(blackoutDuration);
        
        TurnOnLights();
    }

    public void TurnOffLights()
    {
        foreach (Light light in lights)
        {
            light.intensity = 0;
        }
    }

    public void TurnOnLights()
    {
        foreach (Light light in lights)
        {
            light.intensity = lightsIntensity;
        }
    }
}
