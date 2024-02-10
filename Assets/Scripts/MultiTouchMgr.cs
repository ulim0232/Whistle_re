using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiTouchMgr : MonoBehaviour
{
    public bool IsTouching {  get; private set; }

    public float minZoomInch = 0.2f; //1인치는 2.54cm
    public float maxZoomInch = 0.5f;

    public float minZoomPixel;
    public float maxZoomPixel;
    public float ZoomInch { get; private set; } //늘어나고 줄어드는 값을 인치 단위로

    private List<int> fingerIdList = new List<int>(); //현재 유효한 터치들의 아이디를 저장할 리스트
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

            // 이전 프레임에서의 두 터치 사이의 거리
            var prevFrameDist = Vector2.Distance(prevTouchPos[0], prevTouchPos[1]);

            // 현재 프레임에서의 두 터치 사이의 거리
            var currentFrameDist = Vector2.Distance(currentTouchPos[0], currentTouchPos[1]);

            //Debug.Log(currentFrameDist - prevFrameDist); //멀어지면 양수 가까워지면 음수
            //스크린 좌표계의 좌표이기 때문에 해상도별로 달라짐. 인치로 변환 필요

            var distancePixel = currentFrameDist - prevFrameDist;
            //var distanceInch = distancePixel / Screen.dpi; //픽셀을 인치로 변환

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
                    if(fingerIdList.Count == 0 && primaryFingerId == int.MinValue) //여러개의 동시 터치 막기
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
                    if(primaryFingerId == touch.fingerId) //여러개의 동시 터치 막기
                    {
                        primaryFingerId = int.MinValue;
                    }
                    fingerIdList.Remove(touch.fingerId);
                    break;
            }
        }

        if(primaryFingerId == int.MinValue && fingerIdList.Count > 0) //여러개의 동시 터치 막기
        {
           
        }

        UpdatePinchToZoom();
        //#if UNITY_EDITOR || UNITY_STANDALONE //pc 환경 플레이

        //#elif UNITY_ANDROID || UNITY_IOS //모바일 환경 플레이

        //#endif
    }
}
