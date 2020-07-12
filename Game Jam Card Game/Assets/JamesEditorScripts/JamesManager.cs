using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JamesManager : MonoBehaviour
{
    public static JamesManager instance;
    Camera mainCamera;

    [Header("Status Bars")]
    public JamesStatusBar happiness;
    public JamesStatusBar population;

    public List<int> m_DiceScoreModifiers = new List<int>();

    [Header("Dice")]
    int diceToRoll = 4;
    public PhysicsDie[] dice;

    [Header("Dragging")]
    Ray pointerRay = new Ray();
    RaycastHit hit = new RaycastHit();
    PhysicsDie heldDie;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    #region Dragging - does not currently work

    public void OnDrag(PointerEventData eventData)
    {
        heldDie.GetRigidbody().freezeRotation = true;
        pointerRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(pointerRay, out hit, float.MaxValue);
        transform.position = hit.point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        JamesCard cardHit = hit.collider.gameObject.GetComponent<JamesCard>();

        if (cardHit != null)
        {
            cardHit.SetDie(heldDie);
            transform.position = cardHit.diePosition.position;
        }
    }

    #endregion

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
                    m_DiceScoreModifiers.Add(effect.m_Severity);
                    break;
                case Effect.DiceToRoll:
                    diceToRoll = Mathf.Max(0, diceToRoll += effect.m_Severity);
                    break;
                case Effect.CardsToDraw:
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
            die.GetRenderer().material.color = Color.white;
            die.modifier = 0;
            die.gameObject.SetActive(false);
        }

        // Apply modifiers - modifiers can stack!
        Queue<int> modifierQueue = new Queue<int>(m_DiceScoreModifiers);
        print(modifierQueue.Count);
        while (modifierQueue.Count > 0)
        {
            PhysicsDie dieToAffect = dice[Random.Range(0, diceToRoll)];
            dieToAffect.modifier += modifierQueue.Dequeue();
            dieToAffect.GetRenderer().material.color = Color.Lerp(Color.white, dieToAffect.modifier > 0 ? Color.yellow : Color.cyan, Mathf.Abs(dieToAffect.modifier) / (float)5);
        }
        //m_DiceScoreModifiers.Clear();
        // Roll dice!
        for (int i = 0; i < diceToRoll; i++)
        {
            dice[i].gameObject.SetActive(true);
            dice[i].RollDie();
        }
    }
}
