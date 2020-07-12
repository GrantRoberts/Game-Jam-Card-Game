using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour
{
     Ray m_PointerRay = new Ray();
    RaycastHit m_Hit = new RaycastHit();
    Camera m_MainCamera;

    PhysicsDie m_PhysicsDie;

    int m_DieValue;

    public float m_DragHeight = 1.0f;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        m_PhysicsDie = GetComponent<PhysicsDie>();
    }

    public void OnMouseDrag()
    {
        // This is so scuffed.
        // Make the raycast coming up go through this die.
        gameObject.layer = 2;

        m_DieValue = m_PhysicsDie.GetResult();
        m_PointerRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(m_PointerRay, out m_Hit, float.MaxValue);
        transform.position = m_Hit.point + (Vector3.up * m_DragHeight);

        // This is dumb.
        // Have to reset for function to even be called.
        gameObject.layer = 0;
    }

    public void OnCollisionStay(Collision collision)
    {
        CardText cardHit = collision.gameObject.GetComponent<CardText>();
        if (cardHit != null)
        {
            cardHit.SetDieRoll(m_DieValue);
        }
    }
}