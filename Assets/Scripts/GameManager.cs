using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// ������ ���� ���� ���θ� �����ϴ� ���� �Ŵ���
public class GameManager : MonoBehaviour
{
    public enum MissionType
    {
        Data,
        Book
    }
    // �̱��� ���ٿ� ������Ƽ
    public static GameManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; // �̱����� �Ҵ�� static ����

    public int score { get; private set; } // ���� ���� ����
    public bool isGameover { get; private set; } // ���� ���� ����
    public float gameOverTime { get; private set; }
    public float timer = 0;
    public GameObject menuList;
    public int CompleteBookCount = 0;
    public int CompleteDataCount = 0;

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
        score = 0;
        //Cursor.visible = false;
        gameOverTime = 600f;
        timer = gameOverTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
        }

        if(!isGameover)
        {
            timer -= Time.deltaTime;
            UIManager.instance.SetTimerTxt(timer);

            if(timer < 0)
            {
                EndGame();
            }
        }
    }

    // ������ �߰��ϰ� UI ����
    public void AddScore(int newScore)
    {
        // ���� ������ �ƴ� ���¿����� ���� ���� ����
        if (!isGameover)
        {
            // ���� �߰�
            score += newScore;
        }
        Debug.Log(score);
    }

    // ���� ���� ó��
    public void ClearGame()
    {
        isGameover = true;
        Cursor.visible = true;
        SceneManager.LoadScene("Happy");
        //UIManager.instance.SetActiveGameclearUI(true);
    }

    public void EndGame()
    {
        isGameover = true;
        Cursor.visible = true;
        SceneManager.LoadScene("Sad");
        //UIManager.instance.SetAcitveGameOverUI(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void SetMenuList()
    //{
    //    if (menuList.activeSelf)
    //    {
    //        UIManager.instance.SetActiveMenuList(false);
    //    }
    //    else
    //    {
    //        UIManager.instance.SetActiveMenuList(true);
    //    }
    //}

    public void UpdateMissionList(MissionType type)
    {
        if(type == MissionType.Data)
        {
            UIManager.instance.UpdateMissionList(CompleteDataCount + 3, true);
            CompleteDataCount++;
        }
        else if(type == MissionType.Book)
        {
            UIManager.instance.UpdateMissionList(CompleteBookCount, true);
            CompleteBookCount++;
        }
    }
}