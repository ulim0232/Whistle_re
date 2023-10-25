using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // Unity-UI�� ����ϱ� ���� ������ ���ӽ����̽�
using UnityEngine.Events;           // UnityEvent ���� API(���ø����̼����α׷����������̽�, �̸��������� �����Լ�)�� ����ϱ� ���� ������ ���ӽ����̽�
using UnityEngine.SceneManagement;

public class MainTitleUIManager : MonoBehaviour
{
    // ��ư�� ������ ����
    public Button startButton;
    public Button optionButton;
    public Button exitButton;
    public Button exitnoButton;
    public Button exityesButton;
    public Button optionbackButton;
    public Button stage1Button;



    public GameObject Main;
    public GameObject Stage;
    public GameObject Option;
    public GameObject Exit;
    private UnityAction action;

    private void Start()
    {
        /*
        Acion ������ �Լ��� �����ϴ� ���ٽ��� ����

        �Ű�����(����, �μ�, Argument)�� ���� �� 
        ��������ƮŸ�� ������ = (�Ű�����1, �Ű�����2 ...) => ��;
        ��������ƮŸ�� ������ = (�Ű�����1, �Ű�����2 ...) => {����1;, ����2; ...};

        �Ű�����(����, �μ�, Argument)�� ���� �� 
        ��������ƮŸ�� ������ = () => ��;
        ��������ƮŸ�� ������ = () => {����1;, ����2; ...};
        */

        // UnityAction�� ����� �̺�Ʈ ���� ���
        //action = () => OnStartClick();
        //startButton.onClick.AddListener(action);

        // ����޼��带 Ȱ���� �̺�Ʈ ���� ���
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });

        // ���ٽ��� Ȱ���� �̺�Ʈ ���� ���
        startButton.onClick.AddListener(() => OnButtonClick(startButton.name));

        exitButton.onClick.AddListener(() => OnButtonClick(exitButton.name));

        exitnoButton.onClick.AddListener(() => OnButtonClick(exitnoButton.name));

        exityesButton.onClick.AddListener(() => OnButtonClick(exityesButton.name));

        exitButton.onClick.AddListener(() => OnButtonClick(exitButton.name));

        optionbackButton.onClick.AddListener(() => OnButtonClick(optionbackButton.name));

        stage1Button.onClick.AddListener(() => OnButtonClick(stage1Button.name));
    }

    public void OnButtonClick(string msg)
    {
        Debug.Log($"��ư ���� : {msg}");
    }

    public void OnStartClick()
    {
        Stage.SetActive(true);
        Exit.SetActive(false);
        Option.SetActive(false);
    }

    public void OnOptionClick()
    {
        //Stage.SetActive(false);
        //Exit.SetActive(false);
        //Option.SetActive(true);
        //Main.SetActive(false);
    }

    public void OnExitClick()
    {
        Exit.SetActive(true);
        Stage.SetActive(false);
        Option.SetActive(false);
    }



    public void OnExitnoClick()
    {
        Exit.SetActive(false);
    }

    public void OnExityesClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
				        Application.Quit();
        #endif
    }

    public void OnOptionbackClick()
    {
        Main.SetActive(true);
        Option.SetActive(false);
    }

    public void OnStage1Click()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Develop 1");
    }
}
