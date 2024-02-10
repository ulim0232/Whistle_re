using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private CinemachineFreeLook cineCam;
    public VirtualJoyStick panel;
    public float rotateSpeed = 0.5f;
    public float rotateYSpeed = 0.5f;

    private float yAxisSpeed = 0.07f;
    private float xAxisSpeed = 5.0f;

    private void Start()
    {
        cineCam = GetComponent<CinemachineFreeLook>();
        cineCam.m_YAxis.Value = 0.5f;
    }

    private void Update()
    {
        
        cineCam.m_XAxis.Value += panel.GetAxis(VirtualJoyStick.Axis.Horizontal) * 180f * Time.deltaTime * rotateSpeed;
        cineCam.m_YAxis.Value -= panel.GetAxis(VirtualJoyStick.Axis.Vertical) * Time.deltaTime * rotateYSpeed;
        Mathf.Clamp(cineCam.m_YAxis.Value, 0, 1);
        //cineCam.m_YAxis.Value = Mathf.Clamp(panel.GetAxis(VirtualJoyStick.Axis.Vertical), 0, 1);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            cineCam.m_XAxis.m_MaxSpeed = 0;
            cineCam.m_YAxis.m_MaxSpeed = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            cineCam.m_XAxis.m_MaxSpeed = xAxisSpeed;
            cineCam.m_YAxis.m_MaxSpeed = yAxisSpeed;
        }
    }
}
