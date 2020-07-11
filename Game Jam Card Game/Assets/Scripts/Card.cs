using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int m_RollDC = 0;

    public List<Effect> m_GreenEffects = new List<Effect>();
    public List<int> m_GreenEffectsSeverity = new List<int>();

    public List<Effect> m_RedEffects = new List<Effect>();
    public List<int> m_RedEffectsSeverity = new List<int>();

    private int m_DieRoll = 0;

    private PlayerManager m_PlayerManager = null;

    private void Awake()
    {
        m_PlayerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void ApplyEffect()
    {
        // Check green effects.
        for (int i = 0; i < m_GreenEffects.Count; ++i)
        {
            switch(m_GreenEffects[i])
            {
                case Effect.None:
                    break;
                case Effect.Count:
                    Debug.Log("A card was assigned Count as green effect, please change");
                    break;
                case Effect.Happiness:
                    m_PlayerManager.IncreaseHappiness(m_GreenEffectsSeverity[i]);
                    break;
                case Effect.Population:
                    m_PlayerManager.IncreasePopulation(m_GreenEffectsSeverity[i]);
                    break;
            }
        }

        // Check red effects.
        for (int i = 0; i < m_RedEffects.Count; ++i)
        {
            switch (m_RedEffects[i])
            {
                case Effect.None:
                    break;
                case Effect.Count:
                    Debug.Log("A card was assigned Count as red effect, please change");
                    break;
                case Effect.Happiness:
                    m_PlayerManager.DecreaseHappiness(m_RedEffectsSeverity[i]);
                    break;
                case Effect.Population:
                    m_PlayerManager.DecreasePopulation(m_RedEffectsSeverity[i]);
                    break;
            }
        }
    }

    public void SetDieRoll(int roll)
    {
        m_DieRoll = roll;
    }
}