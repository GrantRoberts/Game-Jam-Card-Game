using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int m_RollDC = 0;

    public Effect m_GreenEffect = Effect.None;
    public int m_GreenEffectSeverity = 0;

    public Effect m_RedEffect = Effect.None;
    public int m_RedEffectSeverity = 0;

    private int m_DieRoll = 0;

    private PlayerManager m_PlayerManager = null;

    private void Awake()
    {
        m_PlayerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void ApplyEffect()
    {
        // Check green effect.
        switch(m_GreenEffect)
        {
            case Effect.None:
                break;
            case Effect.Count:
                Debug.Log("A card was assigned Count as green effect, please change");
                break;
            case Effect.Happiness:
                m_PlayerManager.IncreaseHappiness(m_GreenEffectSeverity);
                break;
            case Effect.Population:
                m_PlayerManager.IncreasePopulation(m_GreenEffectSeverity);
                break;
        }

        // Check red effect.
        switch (m_RedEffect)
        {
            case Effect.None:
                break;
            case Effect.Count:
                Debug.Log("A card was assigned Count as red effect, please change");
                break;
            case Effect.Happiness:
                m_PlayerManager.DecreaseHappiness(m_RedEffectSeverity);
                break;
            case Effect.Population:
                m_PlayerManager.DecreasePopulation(m_RedEffectSeverity);
                break;
        }
    }

    public void SetDieRoll(int roll)
    {
        m_DieRoll = roll;
    }
}