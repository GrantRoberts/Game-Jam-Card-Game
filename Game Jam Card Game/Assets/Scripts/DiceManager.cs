 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance = null;

    private PhysicsDie[] m_PhysicsDice = null;

    public Transform m_DiceSpawnPoint = null;

    private float m_SpawnPointExtentsX = 0.0f;

    private float m_SpawnPointExtentsZ = 0.0f;

    private void Awake()
    {
        // Get all the physics dice in the scene.
        m_PhysicsDice = FindObjectsOfType<PhysicsDie>();

        BoxCollider collider = m_DiceSpawnPoint.GetComponent<BoxCollider>();

        m_SpawnPointExtentsX = collider.size.x / 2;
        m_SpawnPointExtentsZ = collider.size.y / 2;

        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SpawnDice(int diceToSpawn)
    {
        for (int i = 0; i < diceToSpawn; ++i)
        {
            PhysicsDie currentDie = m_PhysicsDice[i];

            currentDie.gameObject.SetActive(true);

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