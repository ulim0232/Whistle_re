using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        var message = string.Empty;

        foreach(var touch in Input.touches)
        {
            //message += "Touch ID: " + touch.fingerId;
            //message += "\tPhase" + touch.phase; //터치의 상태, 움직임move, 가만히 있음stationary, 사라짐 등
            //message += "Position: " + touch.position;
            //message += "Delta Pos:  " + touch.deltaPosition; //이전 포지션과 현재 포지션의 차이
            //message += "Delta Time:  " + touch.deltaTime + "\n";

            //기본적인 터치 분석 형태
            switch(touch.phase)
            {

                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled: 
                    break;
                default:
                    break;
            }
        }
        message += "\n";

        text.text = message;
    }
}
