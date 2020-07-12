using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JamesCard : MonoBehaviour
{
    string[] equivalentStrings = new string[]
    {
        "happiness",
        "population",
        "to a die",
        "dice next turn",
        "card next turn"
    };

    public TextMeshPro dcText;
    public TextMeshPro headerText;
    public TextMeshPro bodyText;
    public Transform diePosition;

    PhysicsDie dieOnCard;

    public CardDataContainer cardData;
    // Start is called before the first frame update
    void Start()
    {
        headerText.text = cardData.header;
        dcText.text = cardData.dc.ToString();
        bodyText.text = $"<color=green><b>Success:</b></color>\n{StringConstructor(cardData.successEffects)}\n<color=red><b>Failure:</b></color>\n{StringConstructor(cardData.failureEffects)}";
    }

    string StringConstructor(CardEffect[] effects)
    {
        if (effects.Length == 0)
        {
            return "   No effect";
        }
        string[] output = new string[effects.Length];
        for (int i = 0; i < effects.Length; i++)
        {
            output[i] = $"   {(effects[i].m_Positive ? "+" : "-")}{effects[i].m_Severity} {equivalentStrings[(int)effects[i].m_Effect]}";
        }

        return string.Join("\n", output);
    }

    public void SetDie(PhysicsDie die)
    {
        dieOnCard = die;
        Debug.Log("Die set!");
    }

    public void CheckResult()
    {
        JamesManager.instance.DoEffects((dieOnCard == null || dieOnCard.GetResult() < cardData.dc) ? cardData.failureEffects : cardData.successEffects);
    }
}
