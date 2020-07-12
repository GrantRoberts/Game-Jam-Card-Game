using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup blackScreen;
    public CanvasGroup menuOptions;
    public CanvasGroup tutorial;

    public AudioSource audioSource;

    public AudioClip clickSound;

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound, 0.8f);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Fade.FadeElement(blackScreen, 1, 1, 0));
    }

    public void LoadScreen(CanvasGroup canvasGroup)
    {
        menuOptions.blocksRaycasts = false;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(Fade.FadeElement(menuOptions, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(canvasGroup, 1, 0, 1, 0.7f));
    }
    public void ReturnToMenu(CanvasGroup canvasGroup)
    {
        menuOptions.blocksRaycasts = true;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(Fade.FadeElement(canvasGroup, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(menuOptions, 1, 0, 1, 0.7f));
    }

    public void Quit()
    {
        StartCoroutine(Fade.FadeElement(audioSource, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(blackScreen, 1f, 0, 1, Application.Quit));
    }
}
