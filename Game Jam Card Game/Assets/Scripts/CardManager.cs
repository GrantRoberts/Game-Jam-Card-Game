using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance = null;

    public CardDataContainer[] m_CardsInDeck = null;

    public JamesCard[] m_CardsInPlay = null;

    AudioSource m_AudioSource;

    public Transform[] m_CardPositions = new Transform[0];

    public Transform m_OffScreenPosition = null;

    public AudioClip endDay;

    private int m_CardsOffScreenIterator = 0;

    private bool m_AllCardsOffScreen = false;

    public int safelyOffscreen = 0;


    private void Awake()
    {
        instance = this;

        m_CardsInDeck = Resources.LoadAll<CardDataContainer>("Cards");

        m_AudioSource = FindObjectOfType<AudioSource>();
        if (!m_AudioSource)
        {
            Debug.LogError("Missing AudioSource!");
        }
    }

    private void Start()
    {
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            m_CardsInPlay[i].transform.position = m_OffScreenPosition.position;
            m_CardsInPlay[i].index = i;
        }

        DrawCards();
    }

    private void Update()
    {
        if (safelyOffscreen == 0b_0011_1111)
        {
            DrawCards();
            safelyOffscreen = 0;
        }
    }

    public void DrawCards()
    {
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            JamesCard card = m_CardsInPlay[i];
            card.SetCardData(m_CardsInDeck[Random.Range(0, m_CardsInDeck.Length)]);
            StartCoroutine(card.MoveToPoint(card.cachedPosition, card.GetAnimator().SetBool, "Flipped", false));
            //m_CardsInPlay[i].SetTargetPosition(m_CardPositions[i].position, true);
        }

        JamesManager.instance.RollDice();
    }

    public void ApplyCardEffects()
    {
        m_AudioSource.PlayOneShot(endDay);
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            JamesCard card = m_CardsInPlay[i];
            card.CheckResult();
            card.GetAnimator().SetBool("Flipped", true);
            PhysicsDie cardDie = card.GetDie();
            if (cardDie != null)
            {
                cardDie.gameObject.SetActive(false);
            }
        }
    }

    //public void UpdateCardsOffScreen()
    //{
    //    m_CardsOffScreenIterator++;

    //    if (m_CardsOffScreenIterator == m_CardsInPlay.Length)
    //    {
    //        m_AllCardsOffScreen = true;
    //        m_CardsOffScreenIterator = 0;
    //    }
    //}
}
