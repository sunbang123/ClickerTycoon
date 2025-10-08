using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : UnitBase
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake(); // �θ� Ŭ������ Awake ȣ��
        waypoints = Monster_Spawner.instance.setWaypoints;
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
        if (!other.CompareTag("Player")) return;
        if (!CanStartBattle()) return;

        StartCharging();
        ApplyKnockback();
        Damaged();
        Invoke("Charge", chargeTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnBattleStart(other);
    }

    public override void Charge()
    {
        base.Charge(); // �θ��� �⺻ ���� ȣ��
        Debug.Log("Monster charging!");
    }

    public override void Damaged()
    {
        base.Damaged(); // �θ��� �⺻ ������ ó��
    }

    protected override void OnDeath()
    {
        Debug.Log("Monster defeated!");
        base.OnDeath(); // ������Ʈ �ı�
    }
}