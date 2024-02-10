using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiTouchMgr : MonoBehaviour
{
    public bool IsTouching {  get; private set; }

    public float minZoomInch = 0.2f; //1��ġ�� 2.54cm
    public float maxZoomInch = 0.5f;

    public float minZoomPixel;
    public float maxZoomPixel;
    public float ZoomInch { get; private set; } //�þ�� �پ��� ���� ��ġ ������

    private List<int> fingerIdList = new List<int>(); //���� ��ȿ�� ��ġ���� ���̵� ������ ����Ʈ
    public int primaryFingerId;

    private void Awake()
    {
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        Debug.Log(Screen.dpi); //dot(=pixel) per inch

        minZoomPixel = minZoomInch * Screen.dpi;
        maxZoomPixel = maxZoomInch * Screen.dpi;
    }

    public void UpdatePinchToZoom()
    {
        if (fingerIdList.Count >= 2)
        {
            //[0] 1st Touch, [1] 2nd Touch
            Vector2[] prevTouchPos = new Vector2[2];
            Vector2[] currentTouchPos = new Vector2[2];


            for (int i = 0; i < 2; i++)
            {
                var touch = Array.Find(Input.touches,
                    x => x.fingerId == fingerIdList[i]);
                currentTouchPos[i] = touch.position;
                prevTouchPos[i] = touch.position - touch.deltaPosition;
            }

            // ���� �����ӿ����� �� ��ġ ������ �Ÿ�
            var prevFrameDist = Vector2.Distance(prevTouchPos[0], prevTouchPos[1]);

            // ���� �����ӿ����� �� ��ġ ������ �Ÿ�
            var currentFrameDist = Vector2.Distance(currentTouchPos[0], currentTouchPos[1]);

            //Debug.Log(currentFrameDist - prevFrameDist); //�־����� ��� ��������� ����
            //��ũ�� ��ǥ���� ��ǥ�̱� ������ �ػ󵵺��� �޶���. ��ġ�� ��ȯ �ʿ�

            var distancePixel = currentFrameDist - prevFrameDist;
            //var distanceInch = distancePixel / Screen.dpi; //�ȼ��� ��ġ�� ��ȯ

            //Debug.Log(distanceInch);

            ZoomInch = distancePixel / Screen.dpi;
        }
    }

    public void Update()
    {
        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if(fingerIdList.Count == 0 && primaryFingerId == int.MinValue) //�������� ���� ��ġ ����
                    {
                        primaryFingerId = touch.fingerId;
                    }
                    fingerIdList.Add(touch.fingerId);
                    break;
                case TouchPhase.Moved:
                //break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                //break;
                case TouchPhase.Canceled:
                    if(primaryFingerId == touch.fingerId) //�������� ���� ��ġ ����
                    {
                        primaryFingerId = int.MinValue;
                    }
                    fingerIdList.Remove(touch.fingerId);
                    break;
            }
        }

        if(primaryFingerId == int.MinValue && fingerIdList.Count > 0) //�������� ���� ��ġ ����
        {
           
        }

        UpdatePinchToZoom();
        //#if UNITY_EDITOR || UNITY_STANDALONE //pc ȯ�� �÷���

        //#elif UNITY_ANDROID || UNITY_IOS //����� ȯ�� �÷���

        //#endif
    }
}
