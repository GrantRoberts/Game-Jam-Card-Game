using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
     Ray m_PointerRay = new Ray();
    RaycastHit m_Hit = new RaycastHit();
    public Camera m_GameCamera;

    PhysicsDie m_PhysicsDie;

    int m_DieValue;

    public float m_DragHeight = 1.0f;

    private void Awake()
    {
        m_PhysicsDie = GetComponent<PhysicsDie>();
    }

    public void OnMouseDrag()
    {
        // This is so scuffed.
        // Make the raycast coming up go through this die.
        gameObject.layer = 2;

        m_DieValue = m_PhysicsDie.GetResult();
        m_PointerRay = m_GameCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(m_PointerRay, out m_Hit, float.MaxValue);
        transform.position = m_Hit.point + (Vector3.up * m_DragHeight);
        
        // This is dumb.
        // Have to reset for function to even be called again.
        gameObject.layer = 0;
    }

    public void OnCollisionStay(Collision collision)
    {
        JamesCard cardHit = collision.gameObject.GetComponent<JamesCard>();
        if (cardHit != null)
        {
            cardHit.SetDie(m_PhysicsDie);
        }
    }
}