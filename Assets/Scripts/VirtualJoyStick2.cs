using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoyStick2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 Value { get; private set; }
    private int pointerId;
    private bool isDragging = false;

    private void Update()
    {
        //Debug.Log(Value);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging)
        {
            return;
        }
        pointerId = eventData.pointerId;
        isDragging = true;
        Value = eventData.delta / Screen.dpi;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;
        isDragging = false;
        //Value = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;
        Value = eventData.delta / Screen.dpi;
    }

    
}
