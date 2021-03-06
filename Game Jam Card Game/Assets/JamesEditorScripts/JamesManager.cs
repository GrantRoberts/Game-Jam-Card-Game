﻿using System.Collections.Generic;
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
    int diceToRoll = 6;
    int diceToRollModifier;
    public PhysicsDie[] dice;

    public Canvas m_EndScreen = null;

    private bool m_EndScreenUp = false;

    private int m_DaysSurvived = 0;

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
        // Reset all dice
        foreach (PhysicsDie die in dice)
        {
            die.SetModifier(0);
            die.gameObject.SetActive(false);
            die.GetRenderer().material.color = Color.white;
        }

        // Apply modifiers - modifiers can stack!
        while (m_DiceScoreModifiers.Count > 0)
        {
            PhysicsDie dieToAffect = dice[Random.Range(0, diceToRoll + diceToRollModifier)];
            dieToAffect.AddModifier(m_DiceScoreModifiers.Dequeue());
            dieToAffect.GetRenderer().material.color = Colorx.Slerp(Color.white, dieToAffect.m_Modifier < 0 ? new Color(0.28f, 0.69f, 0.96f) : new Color(0.93f, 0.74f, 0.2f), Mathf.Clamp(Mathf.Abs(dieToAffect.m_Modifier) / (float)5, 0, 1));
        }
        
        DiceManager.instance.SpawnDice(diceToRoll + diceToRollModifier);

        diceToRollModifier = 0;
    }

    public void IncrementDaysSurvived()
    {
        m_DaysSurvived++;
    }

    public int GetDaysSurvived()
    {
        return m_DaysSurvived;
    }

    //public void EndGame(string barTag)
    //{
    //    if (m_EndScreenUp == false)
    //    {
    //        if (barTag == "Happiness")
    //            m_EndScreen.transform.GetChild(1).gameObject.SetActive(true);
    //        else if (barTag == "Population")
    //            m_EndScreen.transform.GetChild(2).gameObject.SetActive(true);
    //        else if (barTag == "Money")
    //            m_EndScreen.transform.GetChild(3).gameObject.SetActive(true);

    //        m_EndScreen.gameObject.SetActive(true);
    //        m_EndScreenUp = true;
    //        Time.timeScale = 0.0f;
    //    }
    //}
}
