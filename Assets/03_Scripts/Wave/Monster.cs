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
        Debug.DrawRay(startPoint, direction * rayLength, Color.green, 1f);
        
        int layerMask = LayerMask.GetMask("User");


        // Raycast �߻�
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, rayLength, layerMask);
        Debug.Log("Monster: " + hit);

        if (hit.collider == null) return;

        ApplyKnockback();
        StartCoroutine(BattleCoroutine());
        Invoke("Knockbacked", knockbackTime);
        StartKnockback();
        hit.collider.gameObject.GetComponent<Unit>().Damaged();
    }
    private IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBattleStart(other);
    }

    public override void Knockbacked()
    {
        base.Knockbacked(); // �θ��� �⺻ ���� ȣ��
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