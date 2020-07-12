using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public JamesCard[] m_CardsInPlay = null;

    public void ApplyCardEffects()
    {
        for (int i = 0; i < m_CardsInPlay.Length; ++i)
        {
            m_CardsInPlay[i].CheckResult();
        }
    }
}
