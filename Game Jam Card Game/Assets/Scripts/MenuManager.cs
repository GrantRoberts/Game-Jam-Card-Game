using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup menuOptions;
    public CanvasGroup tutorial;

    public void LoadScreen(CanvasGroup canvasGroup)
    {
        StartCoroutine(Fade.FadeElement(menuOptions, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(canvasGroup, 1, 0, 1, 0.7f));
        canvasGroup.blocksRaycasts = true;
    }
    public void ReturnToMenu(CanvasGroup canvasGroup)
    {
        StartCoroutine(Fade.FadeElement(canvasGroup, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(menuOptions, 1, 0, 1, 0.7f));
        canvasGroup.blocksRaycasts = false;
    }
}
