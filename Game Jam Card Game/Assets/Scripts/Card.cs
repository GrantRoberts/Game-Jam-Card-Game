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

    private PlayerManager m_PlayerManager = null;

    private void Awake()
    {
        m_PlayerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void ApplyEffect()
    {
        if (m_DieRoll >= m_RollDC)
        {
            // Check green effects.
            for (int i = 0; i < m_SucceedEffects.Length; ++i)
            {
                switch(m_SucceedEffects[i].m_Effect)
                {
                    case Effect.Happiness:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PlayerManager.IncreaseHappiness(m_SucceedEffects[i].m_Severity);
                        else                            
                            m_PlayerManager.DecreaseHappiness(m_SucceedEffects[i].m_Severity);
                        break;
                    case Effect.Population:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PlayerManager.IncreasePopulation(m_SucceedEffects[i].m_Severity);
                        else
                            m_PlayerManager.DecreasePopulation(m_SucceedEffects[i].m_Severity);
                        break;
                }
            }
        }
        else
        {
            // Check red effects.
            for (int i = 0; i < m_FailEffects.Length; ++i)
            {
                switch (m_FailEffects[i].m_Effect)
                {
                    case Effect.Happiness:                    
                        if (m_FailEffects[i].m_Positive)
                            m_PlayerManager.IncreaseHappiness(m_FailEffects[i].m_Severity);
                        else
                            m_PlayerManager.DecreaseHappiness(m_FailEffects[i].m_Severity);
                        break;
                    case Effect.Population:
                        if (m_FailEffects[i].m_Positive)
                            m_PlayerManager.IncreasePopulation(m_FailEffects[i].m_Severity);
                        else
                            m_PlayerManager.DecreasePopulation(m_FailEffects[i].m_Severity);
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