using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems; //using �ʿ�
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public enum Axis
    {
        Horizontal,
        Vertical
    }

    public Image stick; //������ ��ƽ. ���� �׸�
    private float radius = 50f; //�̵� �ݰ�

    private Vector3 originalPoint; //�巡�װ� ������ ���ư� ���� ��ġ�Ǿ��ִ� ��ġ
    private RectTransform rectTr;

    private Vector2 value;

    private int pointerId;
    private bool isDragging = false; //�巡�������� �ƴ��� Ȯ���� �÷��� ����

    public CanvasScaler scaler;

    private void Start()
    {
        rectTr = GetComponent<RectTransform>();

        originalPoint = stick.rectTransform.position; //anchoredPosition�� ���� ��Ŀ ������ ���� �����ǰ�
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
        //Debug.Log(eventData.position); //��ũ�� ��ǥ���� �����ǰ�
        //eventData.pointerDrag

        //Vector3 eventWorldPos; //���� ��ǥ��� ��ȯ�ؼ� ������ ����
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
        pointerId = eventData.pointerId; //��ġ ���̵�� ����ȭ �Ǵ� ��
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
