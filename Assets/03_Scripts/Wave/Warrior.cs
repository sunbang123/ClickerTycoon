using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : UnitBase
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake(); // �θ� Ŭ������ Awake ȣ��
        waypoints = Warrior_Spawner.instance.setWaypoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void MoveTowardsWaypoint()
    {
        base.MoveTowardsWaypoint(); // �θ��� �⺻ �̵� ���� ���
    }

    protected override void OnWaypointReached()
    {
        if (currentWaypointIndex is 1 or 3 or 5 or 7)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public override void OnBattleStart(Collider2D other)
    {
        if (!other.CompareTag("Monster")) return;
        if (!CanStartBattle()) return;
        StartCharging(); // charge�� battle �÷��� ���� true

        animator.SetBool("Combat", true);
        ApplyKnockback(); // �˹� ������
        Damaged();
        StartCoroutine(BattleAnimation());
        Invoke("Charge", chargeTime); // charge�� battle �÷��� ���� false
    }
    private IEnumerator BattleAnimation()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("Knockback", true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnBattleStart(other);
    }

    public override void Charge()
    {
        base.Charge(); // �θ��� �⺻ ���� ȣ��
        Debug.Log("Warrior charging!");
        animator.SetBool("Knockback", false);
        animator.SetBool("Combat", false);
    }

    public override void Damaged()
    {
        base.Damaged(); // �θ��� �⺻ ������ ó��
    }

    protected override void OnDeath()
    {
        Debug.Log("Warrior defeated!");
        base.OnDeath(); // ������Ʈ �ı�
    }
}