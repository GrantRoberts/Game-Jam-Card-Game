using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int m_RollDC = 0;

    public GreenEffect.Effect m_GreenEffect = GreenEffect.Effect.None;
    public int m_GreenEffectSeverity = 0;

    public RedEffect.Effect m_RedEffect = RedEffect.Effect.None;
    public int m_RedEffectSeverity = 0;

    private Vector3 m_TargetPosition = Vector3.zero;

    private bool m_ReachedTarget = false;

    public float m_Speed = 3.0f;

    private int m_DieRoll = 0;

    public void ApplyEffect()
    {
        
    }

    public void SetTargetPosition(Vector3 pos)
    {
        m_TargetPosition = pos;
        m_ReachedTarget = false;
    }

    public void SetDieRoll(int roll)
    {
        m_DieRoll = roll;
    }
}