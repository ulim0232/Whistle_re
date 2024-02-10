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
    public float move { get; private set; } // ������ ������ �Է°�, ���� ������Ƽ
    public float rotate { get; private set; } // ������ ȸ�� �Է°�
    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool interact { get ; private set; } //��ȣ�ۿ� Ű �Է�
    //public ButtonClickedEvent interact { get; private set; }
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        interactButton.onClick.AddListener(OnInteractButtonClick);
    }
    // Update is called once per frame
    void Update()
    {
        // move�� ���� �Է� ����
        //move = Input.GetAxis("Vertical");
        // rotate�� ���� �Է� ����
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
