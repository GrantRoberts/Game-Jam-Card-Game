using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JamesManager : MonoBehaviour
{
    public static JamesManager instance;
    Camera mainCamera;

    [Header("Status Bars")]
    public JamesStatusBar happiness;
    public JamesStatusBar population;
    public JamesStatusBar money;

    public Queue<int> m_DiceScoreModifiers = new Queue<int>();

    [Header("Dice")]
    int diceToRoll = 4;
    public PhysicsDie[] dice;

    [Header("Dragging")]
    Ray pointerRay = new Ray();
    RaycastHit hit = new RaycastHit();
    PhysicsDie heldDie;

    public Canvas m_EndScreen = null;

    private bool m_EndScreenUp = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;
        Time.timeScale = 1.0f;
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
                    diceToRoll = Mathf.Max(0, diceToRoll += effect.m_Severity);
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
        DiceManager.instance.SpawnDice();

        // Reset all dice
        foreach (PhysicsDie die in dice)
        {
            die.SetModifier(0);
        }

        // Apply modifiers - modifiers can stack!
        while (m_DiceScoreModifiers.Count > 0)
        {
            PhysicsDie dieToAffect = dice[Random.Range(0, diceToRoll)];
            dieToAffect.AddModifier(m_DiceScoreModifiers.Dequeue());
        }
        
        // Roll dice!
        for (int i = 0; i < diceToRoll; i++)
        {
            dice[i].gameObject.SetActive(true);
            dice[i].RollDie();
        }
    }

    public void EndGame(string barTag)
    {
        if (m_EndScreenUp == false)
        {
            if (barTag == "Happiness")
                m_EndScreen.transform.GetChild(1).gameObject.SetActive(true);
            else if (barTag == "Population")
                m_EndScreen.transform.GetChild(2).gameObject.SetActive(true);
            else if (barTag == "Money")
                m_EndScreen.transform.GetChild(3).gameObject.SetActive(true);

            m_EndScreen.gameObject.SetActive(true);
            m_EndScreenUp = true;
            Time.timeScale = 0.0f;
        }
    }
}
