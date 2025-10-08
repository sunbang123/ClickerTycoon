using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : UnitBase
{
    public override void OnBattleStart(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Damaged();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnBattleStart(other);
    }

    public override void Damaged()
    {
        base.Damaged(); // �θ��� �⺻ ������ ó��
    }

    protected override void OnDeath()
    {
        Debug.Log("Monster defeated!");
        base.OnDeath(); // ������Ʈ �ı�
        Game_Manager.instance.WinGame(); // ���� �¸� ó��
    }
}
