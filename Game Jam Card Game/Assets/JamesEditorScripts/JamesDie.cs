using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JamesDie : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Ray pointerRay = new Ray();
    RaycastHit hit = new RaycastHit();
    Camera mainCamera;

    PhysicsDie die;

    int cachedValue;

    private void Awake()
    {
        mainCamera = Camera.main;
        die = GetComponent<PhysicsDie>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        cachedValue = die.GetResult();
        pointerRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(pointerRay, out hit, float.MaxValue);
        transform.position = hit.point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CardText cardHit = hit.collider.gameObject.GetComponent<CardText>();

        if (cardHit != null)
        {
            cardHit.SetDieRoll(cachedValue);
            transform.position = cardHit.diePosition.position;
            gameObject.SetActive(false);
        }
    }
}
