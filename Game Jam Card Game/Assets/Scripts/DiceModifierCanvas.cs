using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceModifierCanvas : MonoBehaviour
{
    Transform m_GameCamera = null;

    private void Awake()
    {
        m_GameCamera = transform.parent.GetComponent<Dice>().m_GameCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_GameCamera);
    }
}
