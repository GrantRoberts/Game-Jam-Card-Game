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
        bar.fillAmount = (float) value / maxValue;
        bar.color = Colorx.Slerp(new Color(0.75f, 0.1f, 0.1f), new Color(0.35f, 0.67f, 0.35f), bar.fillAmount);
    }

    public void ModifyValue(int modifier)
    {
        value = Mathf.Clamp(value + modifier, 0, maxValue);
        bar.fillAmount = (float)value / maxValue;
        bar.color = Colorx.Slerp(Color.red, Color.green, bar.fillAmount);

        if (value <= 0)
        {
            JamesManager.instance.EndGame(gameObject.tag);
        }
    }

}
