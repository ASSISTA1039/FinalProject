using System;
using UnityEngine;


public class SceneControllerWrapper : MonoBehaviour
{
    public void RestartZone (bool resetHealth)
    {
        SceneChangeController.RestartZone (resetHealth);
    }

    public void TransitionToScene (TransitionPoint transitionPoint)
    {
        SceneChangeController.TransitionToScene (transitionPoint);
    }

    public void RestartZoneWithDelay(float delay)
    {
        SceneChangeController.RestartZoneWithDelay (delay, false);
    }

    public void RestartZoneWithDelayAndHealthReset (float delay)
    {
        SceneChangeController.RestartZoneWithDelay (delay, true);
    }
}
