using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[SerializeField]
public class Card : MonoBehaviour
{
    public int m_RollDC = 0;

    public CardEffect[] m_SucceedEffects;

    public CardEffect[] m_FailEffects;

    private int m_DieScore = 0;

    private PlayerManager m_PM = null;

    private DiceManager m_DM = null;

    public TextMeshProUGUI m_CardSucceedText = null;

    public TextMeshProUGUI m_CardFailText = null;

    public TextMeshProUGUI m_DCScore = null;

    public TextMeshProUGUI m_DSScore = null;

    private void Awake()
    {
        m_PM = GameObject.FindObjectOfType<PlayerManager>();
        m_DM = GameObject.FindObjectOfType<DiceManager>();

        // Update card text to reflect it's DC and effects.
        m_DCScore.text = m_RollDC.ToString();

        CardEffect currentEffect = null;
        for(int i = 0; i < m_SucceedEffects.Length; ++i)
        {
            currentEffect = m_SucceedEffects[i];

            m_CardSucceedText.text += currentEffect.m_Effect.ToString();

            if (currentEffect.m_Positive)
                m_CardSucceedText.text += " +";
            else
                m_CardSucceedText.text += " -";

            m_CardSucceedText.text += currentEffect.m_Severity.ToString() + "\n";
        }

        for(int i = 0; i < m_FailEffects.Length; ++i)
        {
            currentEffect = m_FailEffects[i];

            m_CardFailText.text += currentEffect.m_Effect.ToString();

            if (currentEffect.m_Positive)
                m_CardFailText.text += " +";
            else
                m_CardFailText.text += " -";

            m_CardFailText.text += currentEffect.m_Severity.ToString() + "\n";
        }
    }

    public void ApplyEffects()
    {
        if (m_DieScore >= m_RollDC)
        {
            // Check succeed effects.
            for (int i = 0; i < m_SucceedEffects.Length; ++i)
            {
                switch(m_SucceedEffects[i].m_Effect)
                {
                    // Happiness.
                    case Effect.Happiness:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreaseHappiness(m_SucceedEffects[i].m_Severity);
                        else                            
                            m_PM.DecreaseHappiness(m_SucceedEffects[i].m_Severity);
                        break;

                    // Population.
                    case Effect.Population:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreasePopulation(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.DecreasePopulation(m_SucceedEffects[i].m_Severity);
                        break;

                    // Dice Scores.
                    case Effect.DiceScore:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.AddDiceScoreIncrease(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.AddDiceScoreDecrease(m_SucceedEffects[i].m_Severity);
                        break;

                    // Dice to roll.
                    case Effect.DiceToRoll:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreaseDiceToRoll(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.DecreaseDiceToRoll(m_SucceedEffects[i].m_Severity);
                        break;

                    // Cards to Draw.
                    case Effect.CardsToDraw:
                        if (m_SucceedEffects[i].m_Positive)
                            m_PM.IncreaseCardsToDraw(m_SucceedEffects[i].m_Severity);
                        else
                            m_PM.DecreaseCardsToDraw(m_SucceedEffects[i].m_Severity);
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
                    // Happiness.
                    case Effect.Happiness:                    
                        if (m_FailEffects[i].m_Positive)
                            m_PM.IncreaseHappiness(m_FailEffects[i].m_Severity);
                        else
                            m_PM.DecreaseHappiness(m_FailEffects[i].m_Severity);
                        break;
                    
                    // Population.
                    case Effect.Population:
                        if (m_FailEffects[i].m_Positive)
                            m_PM.IncreasePopulation(m_FailEffects[i].m_Severity);
                        else
                            m_PM.DecreasePopulation(m_FailEffects[i].m_Severity);
                        break;

                    // Dice Scores.
                    case Effect.DiceScore:
                        if (m_FailEffects[i].m_Positive)
                            m_PM.AddDiceScoreIncrease(m_FailEffects[i].m_Severity);
                        else
                            m_PM.AddDiceScoreDecrease(m_FailEffects[i].m_Severity);
                        break;

                    // Dice to roll.
                    case Effect.DiceToRoll:
                        if (m_FailEffects[i].m_Positive)
                            m_PM.IncreaseDiceToRoll(m_FailEffects[i].m_Severity);
                        else
                            m_PM.DecreaseDiceToRoll(m_FailEffects[i].m_Severity);
                        break;

                    // Cards to Draw.
                    case Effect.CardsToDraw:
                        if (m_FailEffects[i].m_Positive)
                            m_PM.IncreaseCardsToDraw(m_FailEffects[i].m_Severity);
                        else
                            m_PM.DecreaseCardsToDraw(m_FailEffects[i].m_Severity);
                        break;
                }
            }
        }
    }

    public void SetDieRoll(int roll)
    {
        m_DieScore = roll;
        m_DSScore.text = m_DieScore.ToString();
    }
}