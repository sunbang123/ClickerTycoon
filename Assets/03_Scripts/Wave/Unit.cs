using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : UnitBase
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake(); // �θ� Ŭ������ Awake ȣ��
        waypoints = Unit_Spawner.instance.setWaypoints;
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
        if (!CanStartBattle()) return;

        // ��� ���� ���
        Vector2 direction = (other.transform.position - transform.position).normalized;

        // CircleCollider2D �������� �ٱ� ��ġ ���
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        float radius = col.radius * transform.localScale.x; // ������ �ݿ�
        Vector2 center = (Vector2)transform.position + col.offset;
        Vector2 startPoint = center + direction * radius;

        // Raycast �Ÿ� ����
        float rayLength = 0.5f;

        // ����� �� �׸���
        Debug.DrawRay(startPoint, direction * rayLength, Color.red, 1f);

        int layerMask = LayerMask.GetMask("Enemy");

        // Raycast �߻�
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, rayLength, layerMask);
        Debug.Log("Unit: " + hit);


        if (hit.collider == null) return;

        animator.SetBool("Combat", true);
        ApplyKnockback(); // �˹� ������
        StartCoroutine(BattleCoroutine());
        Invoke("Knockbacked", knockbackTime); // charge�� battle �÷��� ���� false
        StartKnockback(); // charge�� battle �÷��� ���� true
        hit.collider.gameObject.GetComponent<Monster>().Damaged();
    }

    private IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Knockback", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBattleStart(other);
    }

    public override void Knockbacked()
    {
        base.Knockbacked(); // �θ��� �⺻ ���� ȣ��
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
        Debug.Log("Monster defeated!");
        base.OnDeath(); // ������Ʈ �ı�
    }
}