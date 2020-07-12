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