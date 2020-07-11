using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class Card : MonoBehaviour
{
    public int m_RollDC = 0;

    public CardEffect[] m_PositiveEffects;

    public CardEffect[] m_NegativeEffects;

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
            for (int i = 0; i < m_PositiveEffects.Length; ++i)
            {
                switch(m_PositiveEffects[i].m_Effect)
                {
                    case Effect.None:
                        break;
                    case Effect.Count:
                        Debug.Log("A card was assigned Count as green effect, please change");
                        break;
                    case Effect.Happiness:
                        m_PlayerManager.IncreaseHappiness(m_PositiveEffects[i].m_Severity);
                        break;
                    case Effect.Population:
                        m_PlayerManager.IncreasePopulation(m_PositiveEffects[i].m_Severity);
                        break;
                }
            }
        }
        else
        {
            // Check red effects.
            for (int i = 0; i < m_NegativeEffects.Length; ++i)
            {
                switch (m_NegativeEffects[i].m_Effect)
                {
                    case Effect.None:
                        break;
                    case Effect.Count:
                        Debug.Log("A card was assigned Count as red effect, please change");
                        break;
                    case Effect.Happiness:
                        m_PlayerManager.DecreaseHappiness(m_NegativeEffects[i].m_Severity);
                        break;
                    case Effect.Population:
                        m_PlayerManager.DecreasePopulation(m_NegativeEffects[i].m_Severity);
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