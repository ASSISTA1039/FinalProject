﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yuliang.UI.Dark;

/// <summary>
/// This class is used to transition between scenes. This includes triggering all the things that need to happen on transition such as data persistence.
/// </summary>
public class SceneChangeController : MonoBehaviour
{

    public static SceneChangeController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<SceneChangeController>();

            if (instance != null)
                return instance;

            Create ();

            return instance;
        }
    }

    public static bool Transitioning
    {
        get { return Instance.m_Transitioning; }
    }

    protected static SceneChangeController instance;

    public static SceneChangeController Create ()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneChangeController>();

        return instance;
    }

    public SceneTransitionDestination initialSceneTransitionDestination;

    protected Scene m_CurrentZoneScene;
    protected SceneTransitionDestination.DestinationTag m_ZoneRestartDestinationTag;
    protected PlayerInput m_PlayerInput;
    protected bool m_Transitioning;

    void Awake()
    {
        //Singleton method
        if (instance == null)
        {
            //First run, set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        m_PlayerInput = FindObjectOfType<PlayerInput>();

        if (initialSceneTransitionDestination != null)
        {
            SetEnteringGameObjectLocation(initialSceneTransitionDestination);
            ScreenFader.SetAlpha(1f);
            StartCoroutine(ScreenFader.FadeSceneIn());
            initialSceneTransitionDestination.OnReachDestination.Invoke();
        }
        else
        {
            m_CurrentZoneScene = SceneManager.GetActiveScene();
            m_ZoneRestartDestinationTag = SceneTransitionDestination.DestinationTag.A;
        }
    }

    public static void RestartZone(bool resetHealth = true)
    {
        if(resetHealth && Movement.PlayerInstance != null)
        {
            Movement.PlayerInstance.health.SetHealth(PlayerHealth.temp_health);
        }

        Instance.StartCoroutine(Instance.Transition(Instance.m_CurrentZoneScene.name, true, Instance.m_ZoneRestartDestinationTag, TransitionPoint.TransitionType.DifferentZone));
    }

    public static void RestartZoneWithDelay(float delay, bool resetHealth = true)
    {
        Instance.StartCoroutine(CallWithDelay(delay, RestartZone, resetHealth));
    }

    public static void TransitionToScene(TransitionPoint transitionPoint)
    {
        Instance.StartCoroutine(Instance.Transition(transitionPoint.newSceneName, transitionPoint.resetInputValuesOnTransition, transitionPoint.transitionDestinationTag, transitionPoint.transitionType));
    }

    public static SceneTransitionDestination GetDestinationFromTag(SceneTransitionDestination.DestinationTag destinationTag)
    {
        return Instance.GetDestination(destinationTag);
    }

    protected IEnumerator Transition(string newSceneName, bool resetInputValues, SceneTransitionDestination.DestinationTag destinationTag, TransitionPoint.TransitionType transitionType = TransitionPoint.TransitionType.DifferentZone)
    {
        m_Transitioning = true;
        PersistentDataManager.SaveAllData();

        if (m_PlayerInput == null)
            m_PlayerInput = FindObjectOfType<PlayerInput>();
        m_PlayerInput.ReleaseControl(resetInputValues);
        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
        PersistentDataManager.ClearPersisters();
        yield return SceneManager.LoadSceneAsync(newSceneName);
        m_PlayerInput = FindObjectOfType<PlayerInput>();
        m_PlayerInput.ReleaseControl(resetInputValues);
        PersistentDataManager.LoadAllData();
        SceneTransitionDestination entrance = GetDestination(destinationTag);
        SetEnteringGameObjectLocation(entrance);
        SetupNewScene(transitionType, entrance);
        if(entrance != null)
            entrance.OnReachDestination.Invoke();
        yield return StartCoroutine(ScreenFader.FadeSceneIn());
        m_PlayerInput.GainControl();

        m_Transitioning = false;
    }

    protected SceneTransitionDestination GetDestination(SceneTransitionDestination.DestinationTag destinationTag)
    {
        SceneTransitionDestination[] entrances = FindObjectsOfType<SceneTransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }
        Debug.LogWarning("No entrance was found with the " + destinationTag + " tag.");
        return null;
    }

    protected void SetEnteringGameObjectLocation(SceneTransitionDestination entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Entering Transform's location has not been set.");
            return;
        }
        Transform entranceLocation = entrance.transform;
        Transform enteringTransform = entrance.transitioningGameObject.transform;
        enteringTransform.position = entranceLocation.position;
        enteringTransform.rotation = entranceLocation.rotation;
    }

    protected void SetupNewScene(TransitionPoint.TransitionType transitionType, SceneTransitionDestination entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Restart information has not been set.");
            return;
        }
        
        if (transitionType == TransitionPoint.TransitionType.DifferentZone)
            SetZoneStart(entrance);
    }

    protected void SetZoneStart(SceneTransitionDestination entrance)
    {
        m_CurrentZoneScene = entrance.gameObject.scene;
        m_ZoneRestartDestinationTag = entrance.destinationTag;
    }

    static IEnumerator CallWithDelay<T>(float delay, Action<T> call, T parameter)
    {
        yield return new WaitForSeconds(delay);
        call(parameter);
    }
}
