using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInput playerinput;

    private FindObjectInFov findObjectInFov;

    public MissionData interactObj;
    public GameObject bookObj;
    public GameObject keyObj;
    public GameObject doorObj;

    private bool hasKey = false;

    public AudioSource playerAudio;
    public AudioClip playerCompleteSound;

    private void Start()
    {
        playerinput = GetComponent<PlayerInput>();
        findObjectInFov = GetComponent<FindObjectInFov>();
        playerAudio = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (playerinput.move != 0 || playerinput.rotate != 0) //�̵��ϸ� �Ͻ� ����
        {
            if (interactObj != null && interactObj.isCapturing)
            {
                interactObj.PauseCapture();
            }
        }
        if (playerinput.interact)
        {
            if (findObjectInFov.hitObject != null)
            {
                interactObj = findObjectInFov.hitObject.GetComponent<MissionData>();
                CollectionData();
            }
            else if (findObjectInFov.bookObject != null)
            {
                bookObj = findObjectInFov.bookObject;
                GetBookData();
            }
            else if(findObjectInFov.keyObject != null)
            {
                keyObj = findObjectInFov.keyObject;
                GetKey();
            }
            else if(findObjectInFov.doorObject != null)
            {
                doorObj = findObjectInFov.doorObject;
                if(hasKey)
                {
                    DoorOpen();
                }
                else
                {
                    UIManager.instance.AcitveNeedKey();
                }

            }
            else if(findObjectInFov.NPCObject != null)
            {
                UIManager.instance.SetActiveNPC();
            }
            else
            {
                Debug.Log("hitObject null");
                return;
            }

        }
    

        if(interactObj != null)
        {
            if (interactObj.gauge == 100)
            {
                interactObj = null;
            }
        }
       
    }

    public void CollectionData()
    {
        if (interactObj.isCapturing || interactObj.isCaptured) //ĸ�� ���� ������Ʈ�� ��ȣ�ۿ� ��õ� or �̹� ���� �Ϸ�Ȼ��� -> �ƹ��ϵ� ���� ����
        {
            return;
        }
        if (interactObj != null)
        {
            if (interactObj.isPaused)
            {
                interactObj.ResumeCapture();
            }
            else
            {
                interactObj.StartCapture();
            }
        }
        else
        {
            Debug.Log("don't exist obj");
        }
    }

    public void GetBookData()
    {
        GameManager.instance.AddScore(1);
        GameManager.instance.UpdateMissionList(GameManager.MissionType.Book);
        bookObj.SetActive(false);
        PlayCompleteSound();
    }

    public void GetKey()
    {
        keyObj.SetActive(false);
        hasKey = true;
        UIManager.instance.SetActiveKey(hasKey);
        PlayCompleteSound();
    }

    public void DoorOpen()
    {
        doorObj.GetComponent<Door>().ActionDoor();
    }

    public void PlayCompleteSound()
    {
        playerAudio.PlayOneShot(playerCompleteSound);
    }
}
