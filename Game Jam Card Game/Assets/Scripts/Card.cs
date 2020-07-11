using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class Card : MonoBehaviour
{
    public int m_RollDC = 0;

    public CardEffect[] m_SucceedEffects;

    public CardEffect[] m_FailEffects;

    private int m_DieRoll = 0;

    private PlayerManager m_PM = null;

    private DiceManager m_DM = null;

    private void Awake()
    {
        m_PM = GameObject.FindObjectOfType<PlayerManager>();
        m_DM = GameObject.FindObjectOfType<DiceManager>();
    }

    public void ApplyEffect()
    {
        if (m_DieRoll >= m_RollDC)
        {
            // Check succeed effects.
            for (int i = 0; i < m_SucceedEffects.Length; ++i)
            {
                switch(m_SucceedEffects[i].m_Effect)
                {
                    case Effect.Happiness:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreaseHappiness(m_SucceedEffects[i].m_Severity);
                        else                            
                            m_PM.DecreaseHappiness(m_SucceedEffects[i].m_Severity);
                        break;

                    case Effect.Population:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreasePopulation(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.DecreasePopulation(m_SucceedEffects[i].m_Severity);
                        break;

                    case Effect.DiceScore:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.AddDiceScoreIncrease(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.AddDiceScoreDecrease(m_SucceedEffects[i].m_Severity);
                        break;

                    case Effect.DiceToRoll:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreaseDiceToRoll(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.DecreaseDiceToRoll(m_SucceedEffects[i].m_Severity);
                        break;
                }
            }
        }
        else
        {
            // Check fail effects.
            for (int i = 0; i < m_FailEffects.Length; ++i)
            {
                switch (m_FailEffects[i].m_Effect)
                {
                    case Effect.Happiness:                    
                        if (m_FailEffects[i].m_Positive)
                            m_PM.IncreaseHappiness(m_FailEffects[i].m_Severity);
                        else
                            m_PM.DecreaseHappiness(m_FailEffects[i].m_Severity);
                        break;
                        
                    case Effect.Population:
                        if (m_FailEffects[i].m_Positive)
                            m_PM.IncreasePopulation(m_FailEffects[i].m_Severity);
                        else
                            m_PM.DecreasePopulation(m_FailEffects[i].m_Severity);
                        break;

                    case Effect.DiceScore:
                    if (m_FailEffects[i].m_Positive)
                        m_PM.AddDiceScoreIncrease(m_FailEffects[i].m_Severity);
                    else
                        m_PM.AddDiceScoreDecrease(m_FailEffects[i].m_Severity);
                    break;

                    case Effect.DiceToRoll:
                    if (m_FailEffects[i].m_Positive)
                        m_PM.IncreaseDiceToRoll(m_FailEffects[i].m_Severity);
                    else
                        m_PM.DecreaseDiceToRoll(m_FailEffects[i].m_Severity);
                    break;
                }
            }
        }
    }

    public void SetDieRoll(int roll)
    {
        m_DieRoll = roll;
        GetComponentInChildren<Text>().text = m_DieRoll.ToString();
    }
}