public enum Effect
{
    Happiness,
    Population
}

[System.Serializable]
public class CardEffect
{
    public Effect m_Effect;

    public int m_Severity;

    public bool m_Positive;
}