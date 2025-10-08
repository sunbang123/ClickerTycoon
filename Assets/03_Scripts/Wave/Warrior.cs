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
        base.Awake(); // 부모 클래스의 Awake 호출
        waypoints = Warrior_Spawner.instance.setWaypoints;
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
        if (!other.CompareTag("Monster")) return;
        if (!CanStartBattle()) return;
        StartCharging(); // charge와 battle 플래그 설정 true

        animator.SetBool("Combat", true);
        ApplyKnockback(); // 넉백 움직임
        Damaged();
        StartCoroutine(BattleAnimation());
        Invoke("Charge", chargeTime); // charge와 battle 플래그 설정 false
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
        base.Charge(); // 부모의 기본 동작 호출
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
        Debug.Log("Warrior defeated!");
        base.OnDeath(); // 오브젝트 파괴
    }
}