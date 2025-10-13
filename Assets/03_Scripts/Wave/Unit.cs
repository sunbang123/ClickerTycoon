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
        base.Awake(); // 부모 클래스의 Awake 호출
        waypoints = Unit_Spawner.instance.setWaypoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void MoveTowardsWaypoint()
    {
        base.MoveTowardsWaypoint(); // 부모의 기본 이동 로직 사용
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

        // 상대 방향 계산
        Vector2 direction = (other.transform.position - transform.position).normalized;

        // CircleCollider2D 기준으로 바깥 위치 계산
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        float radius = col.radius * transform.localScale.x; // 스케일 반영
        Vector2 center = (Vector2)transform.position + col.offset;
        Vector2 startPoint = center + direction * radius;

        // Raycast 거리 제한
        float rayLength = 0.5f;

        // 디버그 선 그리기
        Debug.DrawRay(startPoint, direction * rayLength, Color.red, 1f);

        int layerMask = LayerMask.GetMask("Enemy");

        // Raycast 발사
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, rayLength, layerMask);
        Debug.Log("Unit: " + hit);


        if (hit.collider == null) return;

        animator.SetBool("Combat", true);
        ApplyKnockback(); // 넉백 움직임
        StartCoroutine(BattleCoroutine());
        Invoke("Knockbacked", knockbackTime); // charge와 battle 플래그 설정 false
        StartKnockback(); // charge와 battle 플래그 설정 true
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
        base.Knockbacked(); // 부모의 기본 동작 호출
        Debug.Log("Warrior charging!");
        animator.SetBool("Knockback", false);
        animator.SetBool("Combat", false);
    }

    public override void Damaged()
    {
        base.Damaged(); // 부모의 기본 데미지 처리
    }

    protected override void OnDeath()
    {
        Debug.Log("Monster defeated!");
        base.OnDeath(); // 오브젝트 파괴
    }
}