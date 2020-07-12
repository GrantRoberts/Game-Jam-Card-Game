using System.Collections;
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

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void Quit()
    {
        StartCoroutine(Fade.FadeElement(blackScreen, 1f, 0, 1, Application.Quit));
    }
}
