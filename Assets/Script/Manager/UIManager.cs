﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Menus
{
    MAIN,
    INGAME,
    GAMEOVER
}

public sealed class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public MainMenu mainMenu;
    
    public GameObject activePopUp;
    public GameObject panel;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject gameWonUI;
    public GameObject settingsUI;

    public void ActivateUI(Menus menutype)
    {
        switch (menutype)
        {
            case Menus.MAIN:
                StartCoroutine(ActivateMainMenu());
                break;
            case Menus.INGAME:
                StartCoroutine(ActivateInGameUI());
                break;
        }
    }

    public IEnumerator ActivateMainMenu()
    {
        inGameUI.gameObject.SetActive(false);
        mainMenu.infoText.text = "Level " + PlayerPrefs.GetInt("Level");
        yield return new WaitForSeconds(0.3f);
        mainMenu.gameObject.SetActive(true);
    }

    public IEnumerator ActivateInGameUI()
    {
        mainMenu.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        inGameUI.gameObject.SetActive(true);
    }

    public void ActivateGameOverUI()
    {
        inGameUI.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
        activePopUp = gameOverUI;
    } 
    public void ActivateGameWonUI()
    {
        inGameUI.gameObject.SetActive(false);
        gameWonUI.SetActive(true);
        activePopUp = gameWonUI;
    }

    public void ActivateSettingsPopUp()
    {
        settingsUI.transform.parent.gameObject.SetActive(true);
        settingsUI.SetActive(true);
        activePopUp = settingsUI;
    }
}