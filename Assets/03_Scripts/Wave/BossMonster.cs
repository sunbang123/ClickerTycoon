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
        base.Damaged(); // 부모의 기본 데미지 처리
    }

    protected override void OnDeath()
    {
        Debug.Log("Monster defeated!");
        base.OnDeath(); // 오브젝트 파괴
        Game_Manager.instance.WinGame(); // 게임 승리 처리
    }
}
