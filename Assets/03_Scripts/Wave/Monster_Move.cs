using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Move : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public Transform[] waypoints;

    [Header("Combat Settings")]
    public Slider healthSlider;
    public float knockbackForce = 1f;
    public float chargeTime = 2f;
    public float damageAmount = 2f;

    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool _battleFlag = false;

    private void Awake()
    {
        waypoints = Monster_Spawner.instance.setWaypoints;
        healthSlider = GetComponentInChildren<Slider>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length) return;
        if (_battleFlag) return;
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        movement = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = currentWaypointIndex + 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Monster collided with Player");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Monster collided with Player");
            _battleFlag = true;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            // �˹� ȿ�� - ���� �ӵ��� �ʱ�ȭ�ϰ� AddForce
            rb.velocity = Vector2.zero;  // �ٽ�: ���� �ӵ��� 0���� ���� ���� ����
            rb.AddForce(-movement.normalized * knockbackForce);
            Damaged();
            Invoke("Charge", chargeTime);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("�÷��̾ ȸ���ߴ�.");
    }

    private void Charge()
    {
        _battleFlag = false;
    }
    private void Damaged()
    {
        // �ڽ��� �ڽ��� Canvas�� Slider�� Value�� damageAmount�� �پ���
        healthSlider.value -= damageAmount;

        // ���� Slider�� Value�� 0.1f���� ���ų� �۴ٸ�
            // ������Ʈ�� �ı��ȴ�
        if (healthSlider.value <= 0.1f)
            Destroy(gameObject);
    }
}
