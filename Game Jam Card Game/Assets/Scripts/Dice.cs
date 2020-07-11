using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IDragHandler, IEndDragHandler
{
    DiceManager m_DM = null;

    private int m_DiceRollerIndex = 0;

    private Camera m_MainCamera = null;

    private Ray m_MousePosRay = new Ray();

    private RaycastHit m_RayHitInfo = new RaycastHit();

    private Vector3 m_StartingPosition = Vector3.zero;

    private void Awake()
    {
        m_MainCamera = Camera.main;
        m_StartingPosition = transform.position;
    }

    public void SetDiceRollerIndex(int index)
    {
        m_DiceRollerIndex = index;
        m_DM = GameObject.FindObjectOfType<DiceManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_MousePosRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(m_MousePosRay, out m_RayHitInfo, float.MaxValue);
        transform.position = m_RayHitInfo.point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Card cardHit = m_RayHitInfo.collider.gameObject.GetComponent<Card>();

        if (cardHit != null)
        {
            cardHit.SetDieRoll(m_DM.GetDiceRoll(m_DiceRollerIndex));
            transform.position = m_StartingPosition;
            gameObject.SetActive(false);
        }
    }
}