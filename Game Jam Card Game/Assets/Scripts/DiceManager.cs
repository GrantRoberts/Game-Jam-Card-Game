 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    private PhysicsDie[] m_PhysicsDice;

    private void Awake()
    {
        // Get all the physics dice in the scene.
        m_PhysicsDice = FindObjectsOfType<PhysicsDie>();
    }

    public void RollDice()
    {
        for (int i = 0; i < m_PhysicsDice.Length; ++i)
        {
            m_PhysicsDice[i].RollDie();
        }
    }
}