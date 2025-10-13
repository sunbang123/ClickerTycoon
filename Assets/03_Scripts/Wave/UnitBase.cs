using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IMove
{
    float MoveSpeed { get; set; }
    Transform[] Waypoints { get; set; }

    void MoveTowardsWaypoint();
}

public interface ICombat
{
    Slider HealthSlider { get; set; }
    float KnockbackForce { get; set; }
    float KnockbackTime { get; set; }
    float DamageAmount { get; set; }

    void Damaged();
    void Knockbacked();
    void OnBattleStart(Collider2D other);
}

// �߻� ���̽� Ŭ����
public abstract class UnitBase : MonoBehaviour, IMove, ICombat
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public Transform[] waypoints;

    [Header("Combat Settings")]
    public Slider healthSlider;
    public float knockbackForce = 0.001f;
    public float knockbackTime = 2f;
    public float damageAmount = 2f;

    // �������̽� ������Ƽ ����
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Transform[] Waypoints { get => waypoints; set => waypoints = value; }
    public Slider HealthSlider { get => healthSlider; set => healthSlider = value; }
    public float KnockbackForce { get => knockbackForce; set => knockbackForce = value; }
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    public float DamageAmount { get => damageAmount; set => damageAmount = value; }

    protected int currentWaypointIndex = 0;
    protected Rigidbody2D rb;
    protected Vector2 movement;
    protected bool _battleFlag = false;

    protected virtual void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        MoveTowardsWaypoint();
    }

    // ������ ��������Ʈ�� ���� �̵��ϴ� �⺻ ����
    public virtual void MoveTowardsWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length) return;
        if (_battleFlag) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        // �̵������� ���� fixedDeltaTime�� ���� moveSpeed�ӵ��� �̵��ϴ� ����
        movement = direction * moveSpeed * Time.fixedDeltaTime;
        // �ڽ��� ��ġ + ���� = �̵�
        rb.MovePosition(rb.position + movement);
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            OnWaypointReached();
        }
    }

    protected virtual void OnWaypointReached(){}

    protected bool CanStartBattle()
    {
        return (_battleFlag == false);
    }

    protected void StartKnockback()
    {
        _battleFlag = true;
    }
    public virtual void Knockbacked()
    {
        _battleFlag = false;
    }

    public virtual void Damaged()
    {
        healthSlider.value -= damageAmount;

        if (healthSlider.value <= 0.1f)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }
    public abstract void OnBattleStart(Collider2D other);

    protected void ApplyKnockback()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(-movement.normalized * knockbackForce / 50f, ForceMode2D.Impulse);
    }
}