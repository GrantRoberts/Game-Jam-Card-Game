using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private int m_DiceRollerIndex = 0;

    private Camera m_MainCamera = null;

    private Ray m_MousePosRay = new Ray();

    private RaycastHit m_RayHitInfo = new RaycastHit();

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_MousePosRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(m_MousePosRay, out m_RayHitInfo, float.MaxValue);
        transform.position = m_RayHitInfo.point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}