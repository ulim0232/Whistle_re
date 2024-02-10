using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class PlayerInput : MonoBehaviour
{
    public FindObjectInFov findObjectInFov;
    private Animator playerAnimator;
    public VirtualJoyStick virtualJoyStick;
    public Michsky.MUIP.ButtonManager interactButton;
    public Michsky.MUIP.ButtonManager buttonManager;
    public float move { get; private set; } // 감지된 움직임 입력값, 오토 프로퍼티
    public float rotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool interact { get ; private set; } //상호작용 키 입력
    //public ButtonClickedEvent interact { get; private set; }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        interactButton.onClick.AddListener(OnInteractButtonClick);
    }
    // Update is called once per frame
    void Update()
    {
        // move에 관한 입력 감지
        //move = Input.GetAxis("Vertical");
        // rotate에 관한 입력 감지
        //rotate = Input.GetAxis("Horizontal");
        //interact = Input.GetKeyDown(KeyCode.E);

        //fire = Input.GetButtonDown("Fire2");

        move = virtualJoyStick.GetAxis(VirtualJoyStick.Axis.Vertical);
        rotate = virtualJoyStick.GetAxis(VirtualJoyStick.Axis.Horizontal);
        fire = UIManager.instance.isAttacing;
    }

    private void OnInteractButtonClick()
    {
        fire = true;
    }
}
