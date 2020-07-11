using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public int m_AmountOfDice = 0;
    
    private int[] m_DiceRolls = null;

    private List<Dice> m_DiceObjects = new List<Dice>();
    private List<Text> m_DiceText = new List<Text>();

    private PlayerManager m_PM = null;

    private void Awake()
    {
        m_DiceRolls = new int[m_AmountOfDice];
        for (int i = 0; i < transform.childCount; ++i)
        {
            m_DiceObjects.Add(transform.GetChild(i).GetComponent<Dice>());
            m_DiceObjects[i].SetDiceRollerIndex(i);
            m_DiceText.Add(transform.GetChild(i).GetComponentInChildren<Text>());
        }
        m_PM = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void RollDice()
    {
        for (int i = 0; i < m_PM.GetDiceToRoll(); ++i)
        {
            m_DiceRolls[i] = Random.Range(1, 10);
            transform.GetChild(i).gameObject.SetActive(true);
            m_DiceText[i].text = m_DiceRolls[i].ToString();
        }
    }

    public int GetDiceRoll(int index)
    {
        return m_DiceRolls[index];
    }
}
