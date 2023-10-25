using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
                //FindObjectOfType �Լ��� ������� ����.
            }

            return m_instance;
        }
    }
    private static UIManager m_instance; // �̱����� �Ҵ�� ����

    public TextMeshProUGUI interactTxt;
    //public Slider dataProgress;
    public Slider healthBar;
    public GameObject gameClearUI; // ���� ������ Ȱ��ȭ�� UI 
    public GameObject controlUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI TimeTxt;
    public Michsky.MUIP.ProgressBar dataBar; // ImgsFD;
    public Michsky.MUIP.NotificationManager notification;
    public Michsky.MUIP.NotificationManager npcNotification;
    public Michsky.MUIP.NotificationManager HomeNotification;
    public Michsky.MUIP.NotificationManager RestartNotification;
    public Michsky.MUIP.NotificationManager ExitNotification;
    public Michsky.MUIP.DropdownMultiSelect missionList;
    public GameObject menuList;
    public GameObject key;
    //public Michsky.MUIP.RangeSlider healthBar;


    //private void Start()
    //{
    //    SetActivePorgressUI(false);
    //}

    private void Start()
    {
        SetActivePorgressUI(false);
    }

    private void Update()
    {

    }

    public void SetActiveInteractUI(bool active)
    {
        interactTxt.gameObject.SetActive(active);
    }

    public void SetActivePorgressUI(bool active)
    {
        //dataProgress.gameObject.SetActive(active);
        dataBar.gameObject.SetActive(active);
    }

    public void SetDataProgress(float value)
    {
        //dataProgress.value = value;
        dataBar.ChangeValue(value);
        //dataBar.SetValue(value/100, true);
    }

    public void SetHeathBar(float value)
    {
        healthBar.value = value;
    }

    public void SetActiveGameclearUI(bool active)
    {
        gameClearUI.SetActive(active);
    }

    public void SetActiveControlUI(bool active)
    {
        controlUI.SetActive(active);
    }

    public void SetAcitveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }

    public void SetTimerTxt(float text)
    {
        int minutes = Mathf.FloorToInt(text / 60);
        int seconds = Mathf.FloorToInt(text % 60);

        // �ð��� �ؽ�Ʈ�� ǥ�� (��:��)
        TimeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TestONClick()
    {
        Debug.Log("click");
    }

    public void AcitveNeedKey()
    {
        notification.Open();
    }

    public void SetActiveNPC()
    {
        npcNotification.Open();
    }

    public void SetActiveHomePop(bool isActive)
    {
        if(isActive)
        {
            HomeNotification.Open();
        }
        else
        {
            HomeNotification.Close();
        }
        
    }

    public void SetActiveRestartPop(bool isActive)
    {
        if (isActive)
        {
            RestartNotification.Open();
        }
        else
        {
            RestartNotification.Close();
        }
        //RestartNotification.Open();
    }

    public void SetActiveExitPop(bool isActive)
    {
        if (isActive)
        {
            ExitNotification.Open();
        }
        else
        {
            ExitNotification.Close();
        }
        //ExitNotification.Open();
    }

    public void LoadTitle()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MainTitleUI 1");
    }

    public void SetActiveMenuList()
    {
        if(menuList.activeSelf)
        {
            menuList.SetActive(false);
        }
        else
        {
            menuList.SetActive(true);
        }
    }

    public void OnExityesClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
				                Application.Quit();
        #endif
    }

    public void UpdateMissionList(int index, bool isActive)
    {
        missionList.items[index].isOn = isActive;
        missionList.SetupDropdown();

    }

    public void SetActiveKey(bool value)
    {
        key.SetActive(value);
    }
}
