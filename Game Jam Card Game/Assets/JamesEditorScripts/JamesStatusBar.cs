using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JamesStatusBar : MonoBehaviour
{
    public Image bar;

    int value;

    public int startingValue;
    int maxValue = 50;

    private void Awake()
    {
        value = startingValue;
        bar.fillAmount = (float)value / maxValue;
    }

    public void ModifyValue(int modifier)
    {
        value = Mathf.Clamp(value + modifier, 0, maxValue);
        bar.fillAmount = (float)value / maxValue;

        if (value <= 0)
        {
            JamesManager.instance.EndGame(gameObject.tag);
        }
    }

}
