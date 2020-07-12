 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    private PhysicsDie[] m_PhysicsDice = null;

    public Transform m_DiceSpawnPoint = null;

    private float m_SpawnPointExtentsX = 0.0f;

    private float m_SpawnPointExtentsZ = 0.0f;

    private void Awake()
    {
        // Get all the physics dice in the scene.
        m_PhysicsDice = FindObjectsOfType<PhysicsDie>();

        m_SpawnPointExtentsX = transform.position.x + (transform.localScale.x / 2);
        m_SpawnPointExtentsZ = transform.position.z + (transform.localScale.z / 2);
    }

    private void Start()
    {
        RollDice();
    }

    public void RollDice()
    {
        for (int i = 0; i < m_PhysicsDice.Length; ++i)
        {
            PhysicsDie currentDie = m_PhysicsDice[i];

            // Spawn dice in random area in the spawn point.
            currentDie.transform.position = new Vector3
            (
                Random.Range(-m_SpawnPointExtentsX, m_SpawnPointExtentsX),
                0,
                Random.Range(-m_SpawnPointExtentsZ, m_SpawnPointExtentsZ)
            ) + m_DiceSpawnPoint.position;

            currentDie.RollDie();
        }
    }
}