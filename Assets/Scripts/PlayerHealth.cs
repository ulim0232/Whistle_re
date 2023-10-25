using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    private PlayerMovement playerMovement; // �÷��̾� ������ ������Ʈ
    public float playerStartingHealth = 30f;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void OnEnable()
    {
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();

        //���� �� ������ϸ� false �� �� �ֱ� ������ true�� ��������
        playerMovement.enabled = true;
        health = playerStartingHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OnDamage(100);
        }
    }

    //// ü�� ȸ��
    //public override void RestoreHealth(float newHealth)
    //{
    //    // LivingEntity�� RestoreHealth() ���� (ü�� ����)
    //    base.RestoreHealth(newHealth);

    //    healthSlider.value = health;
    //}

    // ������ ó��
    public override void OnDamage(float damage)
    {
        // LivingEntity�� OnDamage() ����(������ ����)
        base.OnDamage(damage);
        Debug.Log(health);
        UIManager.instance.SetHeathBar(health);
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();
        playerAnimator.SetTrigger("Die");
        playerMovement.enabled = false;
        playerAnimator.SetTrigger("Die");

    }
}
