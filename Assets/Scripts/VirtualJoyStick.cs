using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems; //using 필요
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public enum Axis
    {
        Horizontal,
        Vertical
    }

    public Image stick; //움직일 스틱. 작은 네모
    private float radius = 50f; //이동 반경

    private Vector3 originalPoint; //드래그가 끝나면 돌아갈 원래 배치되어있던 위치
    private RectTransform rectTr;

    private Vector2 value;

    private int pointerId;
    private bool isDragging = false; //드래그중인지 아닌지 확인할 플래그 변수

    public CanvasScaler scaler;

    private void Start()
    {
        rectTr = GetComponent<RectTransform>();

        originalPoint = stick.rectTransform.position; //anchoredPosition은 현재 앵커 설정에 따른 포지션값
        radius = (rectTr.rect.width * 0.5f * Screen.height) / scaler.referenceResolution.y;
        //Debug.Log(radius);
    }

    public float GetAxis(Axis axis)
    {
        switch(axis)
        {
            case Axis.Horizontal:
                return value.x;
            case Axis.Vertical:
                return value.y;
        }
        return 0f;
    }

    private void Update()
    {
        //Debug.Log($"{GetAxis(Axis.Horizontal)} / {GetAxis(Axis.Vertical)}");
    }
    public void UpdateStickPos(Vector3 screenPos)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTr, screenPos, null, out Vector3 newPoint);
        var delta = Vector3.ClampMagnitude(newPoint - originalPoint, radius);
        value = delta / radius;

        stick.rectTransform.position = originalPoint + delta;

        //Debug.Log("d"+value);
        //Debug.Log("n"+delta.normalized);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.position); //스크린 좌표계의 포지션값
        //eventData.pointerDrag

        //Vector3 eventWorldPos; //월드 좌표계로 변환해서 저장할 변수
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTr, eventData.position, null, out eventWorldPos);
        //float distance = Vector3.Distance(originalPoint, eventWorldPos);
        //Vector3 dir = eventWorldPos - originalPoint;
        //dir.Normalize();
        //distance = Mathf.Clamp(distance, 0f, radius);
        //Vector3 point = originalPoint + dir * distance;

        //stick.rectTransform.position = point;

        if (pointerId != eventData.pointerId)
            return;
        UpdateStickPos(eventData.position);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isDragging)
        {
            return;
        }

        isDragging = true;
        pointerId = eventData.pointerId; //터치 아이디와 동기화 되는 값
        UpdateStickPos(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;
        isDragging = false;
        stick.rectTransform.position = originalPoint;
        value = Vector2.zero;
    }
}
