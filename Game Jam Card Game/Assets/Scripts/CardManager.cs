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

    [HideInInspector]
    public Transform m_OffScreenPosition = null;

    public AudioClip endDay;

    public int safelyOffscreen = 0;

    bool canEndDay;

    private void Awake()
    {
        instance = this;

        m_CardsInDeck = Resources.LoadAll<CardDataContainer>("Cards");

        m_AudioSource = FindObjectOfType<AudioSource>();
        if (!m_AudioSource)
        {
            Debug.LogError("Missing AudioSource!");
        }
        m_OffScreenPosition = transform;
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
        canEndDay = m_CardsInPlay[m_CardsInPlay.Length-1].isUnflipped;
    }

    public void DrawCards()
    {
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            JamesCard card = m_CardsInPlay[i];
            card.SetCardData(m_CardsInDeck[Random.Range(0, m_CardsInDeck.Length)]);
            StartCoroutine(card.MoveToPoint(card.cachedPosition, card.GetAnimator().SetBool, "Flipped", false));
        }
        PlayerManager.instance.RollDice();
    }

    public void ApplyCardEffects()
    {
        if (!canEndDay) return;

        PlayerManager.instance.IncrementDaysSurvived();
        m_AudioSource.PlayOneShot(endDay);
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            JamesCard card = m_CardsInPlay[i];
            card.isUnflipped = false;
            card.CheckResult();
            card.GetAnimator().SetBool("Flipped", true);
            PhysicsDie cardDie = card.GetDie();
            if (cardDie != null)
            {
                cardDie.gameObject.SetActive(false);
            }
        }
    }
}
