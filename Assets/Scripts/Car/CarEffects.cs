using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    [SerializeField]
    private List<TrailRenderer> trailRenderers;
    [SerializeField]
    private ParticleSystem smokeParticle;
    [SerializeField]
    private Material brakeLightOn;
    [SerializeField]
    private Material brakeLightOff;
    [SerializeField]
    private List<Renderer> lights;

    private bool tireMarksFlag;
    private bool smokeFlag;

    public void SwitchBrakeLights(bool shouldBeTurnedOn)
    {
        if(shouldBeTurnedOn)
        {
            foreach (var light in lights)
            {
                light.material = brakeLightOn;
            }
        }
        else
        {
            foreach (var light in lights)
            {
                light.material = brakeLightOff;
            }
        }
    }

    public void CheckIfBraking(bool shouldPlay)
    {
        if (shouldPlay)
        {
            StartEmitter();
        }
        else
        {
            StopEmitter();
        }          
    }

    public void CheckIfSmoking(bool shouldPlay)
    {
        if (shouldPlay)
        {
            PlaySmoke();
        }
        else
        {
            StopSmoke();
        }          
    }

    private void PlaySmoke()
    {
        if (smokeFlag)
        {
            return;
        }
        smokeParticle.Play();
        smokeFlag = true;
    }

    private void StopSmoke()
    {
        if(!smokeFlag)
        {
            return;
        }
        smokeParticle.Stop();
        smokeFlag = false;
    }

    private void StartEmitter()
    {
        if (!tireMarksFlag)
        {
            return;
        }
        foreach (TrailRenderer trail in trailRenderers)
        {
            trail.emitting = false;
        }
        tireMarksFlag = false;
    }

    private void StopEmitter()
    {
        if (tireMarksFlag)
        {
            return;
        }
        foreach (TrailRenderer trail in trailRenderers)
        {
            trail.emitting = true;
        }
        tireMarksFlag = true;
    }

}
