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
            //message += "\tPhase" + touch.phase; //��ġ�� ����, ������move, ������ ����stationary, ����� ��
            //message += "Position: " + touch.position;
            //message += "Delta Pos:  " + touch.deltaPosition; //���� �����ǰ� ���� �������� ����
            //message += "Delta Time:  " + touch.deltaTime + "\n";

            //�⺻���� ��ġ �м� ����
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
