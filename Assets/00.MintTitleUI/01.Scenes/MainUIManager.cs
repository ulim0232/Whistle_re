using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // Unity-UI�� ����ϱ� ���� ������ ���ӽ����̽�
using UnityEngine.Events;           // UnityEvent ���� API(���ø����̼����α׷����������̽�, �̸��������� �����Լ�)�� ����ϱ� ���� ������ ���ӽ����̽�
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    // ��ư�� ������ ����
    public Button startButton;
    public Button optionButton;
    public Button exitButton;
    public Button backButton;
    public Button stage1Button;


    public GameObject Main;
    public GameObject Stage;
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
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);

        // ����޼��带 Ȱ���� �̺�Ʈ ���� ���
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); });

        // ���ٽ��� Ȱ���� �̺�Ʈ ���� ���
        exitButton.onClick.AddListener(() => OnButtonClick(exitButton.name));

        backButton.onClick.AddListener(() => OnButtonClick(backButton.name));

        stage1Button.onClick.AddListener(() => OnButtonClick(stage1Button.name));
    }

    public void OnButtonClick(string msg)
    {
        Debug.Log($"��ư ���� : {msg}");
    }

    public void OnStartClick()
    {
        Main.SetActive(false);
        Stage.SetActive(true);
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
    }

    public void OnBackClick()
    {
        Main.SetActive(true);
        Stage.SetActive(false);
    }

    public void OnStage1Click()
    {
        SceneManager.LoadScene("Develop");
    }
}
