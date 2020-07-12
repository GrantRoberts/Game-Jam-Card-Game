using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup menuOptions;
    public CanvasGroup tutorial;

    public void ToTutorial()
    {
        StartCoroutine(Fade.FadeElement(menuOptions, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(tutorial, 1, 0, 1, 0.7f));
        tutorial.blocksRaycasts = true;
    }
    public void ReturnToMenu()
    {
        StartCoroutine(Fade.FadeElement(tutorial, 1, 1, 0));
        StartCoroutine(Fade.FadeElement(menuOptions, 1, 0, 1, 0.7f));
        tutorial.blocksRaycasts = false;
    }
}
