﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup blackScreen;

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadMainMenu()
    {
        StartCoroutine(Fade.FadeElement(blackScreen, 1, 0, 1, SceneManager.LoadScene, "Main Menu"));
    }
}
