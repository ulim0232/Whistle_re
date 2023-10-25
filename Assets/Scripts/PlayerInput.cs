using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public FindObjectInFov findObjectInFov;
    private Animator playerAnimator;
    public float move { get; private set; } // ������ ������ �Է°�, ���� ������Ƽ
    public float rotate { get; private set; } // ������ ȸ�� �Է°�
    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool interact { get ; private set; } //��ȣ�ۿ� Ű �Է�

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //{
        //    move = Input.GetAxis("Vertical");
        //    // rotate�� ���� �Է� ����
        //    rotate = Input.GetAxis("Horizontal");
        //}
        // move�� ���� �Է� ����
        move = Input.GetAxis("Vertical");
        // rotate�� ���� �Է� ����
        rotate = Input.GetAxis("Horizontal");

        interact = Input.GetKeyDown(KeyCode.E);

        fire = Input.GetButtonDown("Fire2");
    }
}
