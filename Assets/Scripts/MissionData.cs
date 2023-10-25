using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData : MonoBehaviour
{
    public float gauge { get; private set; } //���� ���൵
    public float duration = 40f;

    private float startTime;
    public bool isCapturing; //���� ������ Ȯ��. �̰��� Ȱ��ȭ�Ǹ� ���൵�� ���� ����
    public bool isCaptured; //���� �Ϸ� �Ǿ����� Ȯ��
    public Outline outline;
    public bool isPaused;
    private float pauseTime;
    public Transform player;
    private float outlineDis = 3f;
    public bool hasCompletedCapture = false;

    GameManager.MissionType type { get; } = GameManager.MissionType.Data;

    private void Start()
    {
        isCapturing = false;
        isCaptured = false;
        gauge = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hasCompletedCapture = false;
    }

    public void Update()
    {
        if (!isCaptured)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            //Debug.Log(distanceToPlayer);
            if (distanceToPlayer > outlineDis)
            {
                outline.enabled = false;
            }
            else
            {
                outline.enabled = true;
            }
        }
        if (!isCapturing)
            return;
        Capturing();
    }
    public void Capturing()
    {
        float elapsed = Time.time - startTime; //��� �ð�
        if (elapsed >= duration)
        {
            if (!hasCompletedCapture)
                CompleteCapture();
        }
        else
        {
            gauge = Mathf.Lerp(0f, 100f, elapsed / duration);
            UIManager.instance.SetDataProgress(gauge);
        }
        //Debug.Log("Current Gauge Value: " + gauge);
    }
    public void StartCapture() //Ȱ��ȭ
    {
        startTime = Time.time;
        isCapturing = true;
        UIManager.instance.SetActivePorgressUI(true);
    }

    public void CompleteCapture()
    {
        gauge = 100;
        isCapturing = false;
        isCaptured = true;
        if (outline != null)
        {
            outline.enabled = false;
        }
        UIManager.instance.SetActivePorgressUI(false);
        GameManager.instance.AddScore(1);
        GameManager.instance.UpdateMissionList(type);
        hasCompletedCapture = true;
        player.GetComponent<PlayerInteract>().PlayCompleteSound();
    }

    public void PauseCapture()
    {
        Debug.Log("pause");
        if (isCapturing && !isPaused)
        {
            isPaused = true;
            isCapturing = false;
            pauseTime = Time.time;
            UIManager.instance.SetActivePorgressUI(false);
        }
    }

    public void ResumeCapture()
    {
        if (!isCapturing && isPaused)
        {
            isPaused = false;
            isCapturing = true;
            startTime += Time.time - pauseTime; // �Ͻ������� �ð��� ���� �ٽ� ����
            UIManager.instance.SetActivePorgressUI(true);
        }
    }
}
