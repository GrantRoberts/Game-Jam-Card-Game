using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JamesManager : MonoBehaviour
{
    public static JamesManager instance;
    Camera mainCamera;

    public CanvasGroup UIGroup;

    [Header("Status Bars")]
    public JamesStatusBar happiness;
    public JamesStatusBar population;
    public JamesStatusBar money;

    public Queue<int> m_DiceScoreModifiers = new Queue<int>();

    [Header("Dice")]
    int diceToRollDefault = 6;
    int diceToRollModifier;
    public PhysicsDie[] dice;

    public Canvas m_EndScreen = null;

    private int m_DaysSurvived = 0;

    public Gradient m_DiceBonusGradient;

    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;
        Time.timeScale = 1.0f;
        StartCoroutine(Fade.FadeElement(UIGroup, 1, 0, 1));
    }

    public void DoEffects(CardEffect[] effects)
    {
        foreach (CardEffect effect in effects)
        {
            switch (effect.m_Effect)
            {
                case Effect.Happiness:
                    happiness.ModifyValue(effect.m_Severity);
                    break;
                case Effect.Population:
                    population.ModifyValue(effect.m_Severity);
                    break;
                case Effect.DiceScore:
                    m_DiceScoreModifiers.Enqueue(effect.m_Severity);
                    break;
                case Effect.DiceToRoll:
                    diceToRollModifier += effect.m_Severity;
                    break;
                case Effect.CardsToDraw:
                    break;
                case Effect.Money:
                    money.ModifyValue(effect.m_Severity);
                    break;
                default:
                    break;
            }
        }
    }

    [ContextMenu("Roll dice")]
    public void RollDice()
    {
        int totalDiceToRoll = diceToRollDefault + diceToRollModifier;

        // Reset all dice
        foreach (PhysicsDie die in dice)
        {
            die.Reset();
        }

        // Apply modifiers - modifiers can stack!
        while (m_DiceScoreModifiers.Count > 0)
        {
            // Select a random die and modify the values
            dice[Random.Range(0, totalDiceToRoll)].AddModifier(m_DiceScoreModifiers.Dequeue());
        }
        
        DiceManager.instance.SpawnDice(totalDiceToRoll);

        diceToRollModifier = 0;
    }

    public void IncrementDaysSurvived()
    {
        m_DaysSurvived++;
    }

    public int GetDaysSurvived() => m_DaysSurvived;
}
