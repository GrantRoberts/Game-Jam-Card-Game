using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardDataContainer[] m_CardsInDeck = null;

    public JamesCard[] m_CardsInPlay = null;    

    private void Awake()
    {       
    }

    private void Start()
    {
        DrawCards();
    }

    public void DrawCards()
    {
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            m_CardsInPlay[i].SetCardData(m_CardsInDeck[Random.Range(0, m_CardsInDeck.Length)]);
            m_CardsInPlay[i].GetComponent<Animator>().Play("CardUnflip");
        }

        JamesManager.instance.RollDice();
    }

    public void ApplyCardEffects()
    {
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            m_CardsInPlay[i].CheckResult();
            m_CardsInPlay[i].GetComponent<Animator>().Play("CardFlip");
        }

        DrawCards();
    }
}
