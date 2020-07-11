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

    public int m_DiceToRoll = 5;

    private void Awake()
    {
        m_Population = m_StartingPopulation;
        m_Happiness = m_StartingHappiness;
    }

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

    public int GetDiceToRoll()
    {
        return m_DiceToRoll;
    }

    public void SetDiceToRoll(int toRoll)
    {
        m_DiceToRoll = toRoll;
    }
}