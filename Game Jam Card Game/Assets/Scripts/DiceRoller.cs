using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public int m_AmountOfDice = 0;
    
    private int[] m_DiceRolls = null;

    private List<Transform> m_DiceObjects = new List<Transform>();

    private PlayerManager m_PM = null;

    private void Awake()
    {
        m_DiceRolls = new int[m_AmountOfDice];
        for (int i = 0; i < transform.childCount; ++i)
        {
            m_DiceObjects.Add(transform.GetChild(i));
        }
        m_PM = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void RollDice()
    {
        for (int i = 0; i < m_PM.GetDiceToRoll(); ++i)
        {
            m_DiceRolls[i] = Random.Range(1, 10);
            m_DiceObjects[i].GetComponentInChildren<Text>().text = m_DiceRolls[i].ToString();
        }
    }
}
