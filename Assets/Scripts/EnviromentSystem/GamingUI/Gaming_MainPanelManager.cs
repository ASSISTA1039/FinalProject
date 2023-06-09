﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gaming_MainPanelManager : MonoBehaviour
{
    [Header("PANEL LIST")]
    public List<GameObject> panels = new List<GameObject>();


    [Header("SETTINGS")]
    public int currentPanelIndex = 0;
    public bool enableBrushAnimation = true;
    public bool enableHomeBlur = true;
         
    private GameObject currentPanel;
    private GameObject nextPanel;
    private Animator currentPanelAnimator;
    private Animator nextPanelAnimator;

    string panelFadeIn = "Panel In";
    string panelFadeOut = "Panel Out";

    PanelBrushManager currentBrush;
    PanelBrushManager nextBrush;


    public void OpenFirstTab()
    {
        currentPanel = panels[currentPanelIndex];
        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        currentPanelAnimator.Play(panelFadeIn);
    }

    public void PanelAnim(int newPanel)
    {
        if (newPanel == currentPanelIndex)
        {
            currentPanel = panels[currentPanelIndex];

            //currentPanelIndex = newPanel;
            //nextPanel = panels[currentPanelIndex];

            currentPanelAnimator = currentPanel.GetComponent<Animator>();

            //nextPanelAnimator = nextPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeIn);
            //nextPanelAnimator.Play(panelFadeIn);

            if (enableBrushAnimation == true)
            {
                currentBrush = currentPanel.GetComponent<PanelBrushManager>();
                if (currentBrush.brushAnimator != null)
                    currentBrush.BrushSplashIn();
                //nextBrush = nextPanel.GetComponent<PanelBrushManager>();
                //if (nextBrush.brushAnimator != null)
                //    nextBrush.BrushSplashIn();
            }
        }
    }

    public void PanelAnim()
    {
        currentPanel = panels[currentPanelIndex];
        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        currentPanelAnimator.Play(panelFadeOut);

        if (enableBrushAnimation == true)
        {
            currentBrush = currentPanel.GetComponent<PanelBrushManager>();
            if (currentBrush.brushAnimator != null)
                currentBrush.BrushSplashOut();
        }
    }

    public void NextPage()
    {
        if (currentPanelIndex <= panels.Count - 2)
        {
            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeOut);

            currentPanelIndex += 1;
            nextPanel = panels[currentPanelIndex];

            nextPanelAnimator = nextPanel.GetComponent<Animator>();
            nextPanelAnimator.Play(panelFadeIn);

            if (enableBrushAnimation == true)
            {
                currentBrush = currentPanel.GetComponent<PanelBrushManager>();
                if (currentBrush.brushAnimator != null)
                    currentBrush.BrushSplashOut();
                nextBrush = nextPanel.GetComponent<PanelBrushManager>();
                if (nextBrush.brushAnimator != null)
                    nextBrush.BrushSplashIn();
            }
        }
    }

    public void PrevPage()
    {
        if (currentPanelIndex >= 1)
        {
            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeOut);

            currentPanelIndex -= 1;
            nextPanel = panels[currentPanelIndex];

            nextPanelAnimator = nextPanel.GetComponent<Animator>();
            nextPanelAnimator.Play(panelFadeIn);

            if (enableBrushAnimation == true)
            {
                currentBrush = currentPanel.GetComponent<PanelBrushManager>();
                if (currentBrush.brushAnimator != null)
                    currentBrush.BrushSplashOut();
                nextBrush = nextPanel.GetComponent<PanelBrushManager>();
                if (nextBrush.brushAnimator != null)
                    nextBrush.BrushSplashIn();
            }
        }
    }

    public void Exit2MainPanel()
    {
        SceneManager.LoadScene(0);
    }
}


