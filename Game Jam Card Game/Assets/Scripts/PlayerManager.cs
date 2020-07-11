using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int m_StartingPopulation = 3;
    private int m_Population = 0;
    public Slider m_PopulationBar = null;

    public int m_StartingHappiness = 3;
    private int m_Happiness = 0;
    public Slider m_HappinessBar = null;

    public int m_DefaultDiceToRoll = 5;
    private int m_DiceToRoll = 0;

    public int m_DefaultCardsToDraw = 5;
    private int m_CardsToDraw = 0;

    private List<int> m_DiceScoreIncreases = new List<int>();

    private List<int> m_DiceScoreDecreases = new List<int>();

    private void Awake()
    {
        m_Population = m_StartingPopulation;
        m_Happiness = m_StartingHappiness;

        m_DiceToRoll = m_DefaultDiceToRoll;
        m_CardsToDraw = m_DefaultCardsToDraw;
    }

    // Population.
    public void IncreasePopulation(int increase)
    {
        m_Population += increase;
    }
    public void DecreasePopulation(int decrease)
    {
        m_Population -= decrease;
    }
    public void UpdatePopulationBar()
    {
        m_PopulationBar.value = m_Population;
    }

    // Happiness.
    public void IncreaseHappiness(int increase)
    {
        m_Happiness += increase;
    }
    public void DecreaseHappiness(int decrease)
    {
        m_Happiness -= decrease;
    }
    public void UpdateHappinessBar()
    {
        m_HappinessBar.value = m_Happiness;
    }

    // Dice to roll.
    public int GetDiceToRoll()
    {
        return m_DiceToRoll;
    }
    public void SetDiceToRoll(int toRoll)
    {
        m_DiceToRoll = toRoll;
    }
    public void IncreaseDiceToRoll(int increase)
    {
        m_DiceToRoll += increase;
    }
    public void DecreaseDiceToRoll(int decrease)
    {
        m_DiceToRoll -= decrease;
    }

    // Cards to draw.
    public int GetCardsToDraw()
    {
        return m_CardsToDraw;
    }
    public void SetCardsToDraw(int toDraw)
    {
        m_CardsToDraw = toDraw;
    }
    public void IncreaseCardsToDraw(int increase)
    {
        m_CardsToDraw += increase;
    }
    public void DecreaseCardsToDraw(int decrease)
    {
        m_CardsToDraw -= decrease;
    }

    // Dice score increases.
    public void AddDiceScoreIncrease(int increase)
    {
        m_DiceScoreIncreases.Add(increase);
    }
    public int[] GetDiceScoreIncreases()
    {
        return m_DiceScoreIncreases.ToArray();
    }

    // Dice score decreases.
    public void AddDiceScoreDecrease(int decrease)
    {
        m_DiceScoreDecreases.Add(decrease);
    }
    public int[] GetDiceScoreDecreases()
    {
        return m_DiceScoreDecreases.ToArray();
    }
}