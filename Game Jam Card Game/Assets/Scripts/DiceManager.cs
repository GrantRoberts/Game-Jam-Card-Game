 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public GameObject m_DiceObject = null;

    public int m_AmountOfDice = 0;
    
    private int[] m_DiceRolls = null;

    private List<Dice> m_DiceObjects = new List<Dice>();
    private List<Text> m_DiceText = new List<Text>();

    public List<Transform> m_DiceSpawnPoints = new List<Transform>();

    private PlayerManager m_PM = null;

    private void Awake()
    {
        m_DiceRolls = new int[m_AmountOfDice];

        // Create the dice for the manager to roll.
        for (int i = 0; i < m_AmountOfDice; ++i)
        {
            m_DiceObjects.Add(Instantiate(m_DiceObject).GetComponent<Dice>());
            m_DiceObjects[i].SetDiceRollerIndex(i);
            m_DiceText.Add(m_DiceObjects[i].GetComponentInChildren<Text>());
        }
        m_PM = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void RollDice()
    {
        // Roll dice.
        for (int i = 0; i < m_PM.GetDiceToRoll(); ++i)
        {
            m_DiceRolls[i] = Random.Range(1, 10);
            m_DiceObjects[i].gameObject.SetActive(true);
            m_DiceObjects[i].gameObject.transform.position = m_DiceSpawnPoints[i].position;
            m_DiceText[i].text = m_DiceRolls[i].ToString();
        }

        // Apply modifiers to dice rolls.
        int[] rollIncreases = m_PM.GetDiceScoreIncreases();
        int[] rollDecreases = m_PM.GetDiceScoreDecreases();
        int diceRollsCount = m_DiceRolls.Length;

        // Apply modifiers.
        for (int i = 0; i < rollIncreases.Length; ++i)
        {
            m_DiceRolls[Random.Range(0, diceRollsCount - 1)] += rollIncreases[i];
        }
        for (int i = 0; i < rollDecreases.Length; ++i)
        {
            m_DiceRolls[Random.Range(0, diceRollsCount - 1)] -= rollDecreases[i];
        }
    }

    public int GetDiceRoll(int index)
    {
        return m_DiceRolls[index];
    }
}
