using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public CanvasGroup deathScreen;

    public GameOverState populationFail, moneyFail, happinessFail;

    public TextMeshProUGUI gameOverText, deathMessageText, m_DaysSurvivedCounter;

    public void GameOver(string barTag)
    {
        switch (barTag)
        {
            case "Happiness":
                SetDeathScreen(happinessFail);
                break;
            case "Population":
                SetDeathScreen(populationFail);
                break;
            case "Money":
                SetDeathScreen(moneyFail);
                break;
        default:
                break;
        }
        StartCoroutine(Fade.FadeElement(deathScreen, 1, 0, 1));
        deathScreen.blocksRaycasts = true;
        m_DaysSurvivedCounter.text = "You survived: " + JamesManager.instance.GetDaysSurvived().ToString() + " days!";
    }

    void SetDeathScreen(GameOverState deathReason)
    {
        gameOverText.color = deathReason.failColor;
        deathMessageText.text = deathReason.failMessage;
    }

}

[System.Serializable]
public class GameOverState
{
    public Color failColor;
    [TextArea(1, 5)]
    public string failMessage;
}
