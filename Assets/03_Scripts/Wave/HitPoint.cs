using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject Arrow;

    [Header("Attack Settings")]
    public float arrowForce = 10f;
    public float attackCooldown = 1f;

    private bool canAttack = true;

    public void OnBattleStart(Collider2D other)
    {
        if (!canAttack) return;

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
        Debug.Log("Unit: " + hit.collider);

        if (hit.collider == null) return;

        // Arrow 생성 및 발사
        ThrowArrow(hit.point, direction);

        StartCoroutine(BattleCoroutine());
    }

    private void ThrowArrow(Vector2 targetPoint, Vector2 direction)
    {
        if (Arrow == null)
        {
            Debug.LogWarning("Arrow prefab is not assigned!");
            return;
        }

        // 이 오브젝트의 중심 위치에서 Arrow 생성
        Vector2 center = (Vector2)transform.position + GetComponent<CircleCollider2D>().offset;
        GameObject arrowInstance = Instantiate(Arrow, center, Quaternion.identity);

        // Arrow의 회전 설정 (targetPoint를 향하도록)
        Vector2 arrowDirection = (targetPoint - center).normalized;
        float angle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;
        arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Rigidbody2D가 있다면 AddForce로 던지기
        Rigidbody2D arrowRb = arrowInstance.GetComponent<Rigidbody2D>();
        if (arrowRb != null)
        {
            arrowRb.AddForce(arrowDirection * arrowForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("Arrow prefab doesn't have Rigidbody2D component!");
        }

        // 1초 후에 화살 제거
        Destroy(arrowInstance, 1f);
    }

    private IEnumerator BattleCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBattleStart(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnBattleStart(other);
    }
}