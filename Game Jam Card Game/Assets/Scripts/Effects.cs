public enum Effect
{
     None,
    Happiness,
    Population,
    Count
}

[System.Serializable]
public class CardEffect
{
    public Effect m_Effect;
    public int m_Severity;
}