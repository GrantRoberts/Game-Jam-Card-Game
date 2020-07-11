using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> m_Cards = new List<Card>();

    private Stack<Card> m_DrawDeck = new Stack<Card>();

    private List<Card> m_CardsInPlay = new List<Card>();

    private PlayerManager m_PM = null;

    private void Awake()
    {
        // Shuffle the cards and put them in the draw deck.
        ShuffleCardsIntoDeck();

        m_PM = GameObject.FindObjectOfType<PlayerManager>();
    }

    public void DrawCards()
    {
        // Check there are enough cards in draw deck to draw from.
        // If there aren't enough, shuffle the deck.
        int cardsToDraw = m_PM.GetCardsToDraw();
        if (m_DrawDeck.Count < cardsToDraw)
        {
            ShuffleCardsIntoDeck();
        }

        for (int i = 0; i < cardsToDraw; ++i)
        {
            m_CardsInPlay.Add(m_DrawDeck.Pop());
        }
    }

    public void ShuffleCardsIntoDeck()
    {
        int n = m_Cards.Count;  
        while (n > 1)
        {
            n--;
            int k = Random.Range(1, n);  
            Card value = m_Cards[k];  
            m_Cards[k] = m_Cards[n];  
            m_Cards[n] = value;
        }

        for (int i = 0; i < m_Cards.Count; ++i)
        {
            m_DrawDeck.Push(m_Cards[i]);
        }
    }
}