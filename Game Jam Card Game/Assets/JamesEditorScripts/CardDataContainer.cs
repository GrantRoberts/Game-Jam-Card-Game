using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    Happiness,
    Population,
    DiceScore,
    DiceToRoll,
    CardsToDraw,
    Money
}

[System.Serializable]
public class CardEffect
{
    public Effect m_Effect;
    public int m_Severity;
}

[CreateAssetMenu(fileName = "Card", menuName = "Card Data")]
public class CardDataContainer : ScriptableObject
{
    public string header;
    [Range(1, 10)]
    public int dc = 1;
    public CardEffect[] successEffects;
    public CardEffect[] failureEffects;

}
