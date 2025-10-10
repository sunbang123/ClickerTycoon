using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitData
{
    public string name;
    public int level = 0;
    public int cost;
    public float attack;
    public float upgradeMultiplier = 2f;
    public bool purchased = false;

    // 생성자 (선택사항)
    public UnitData(string name, int cost, float attack, float upgradeMultiplier)
    {
        this.name = name;
        this.cost = cost;
        this.attack = attack;
        this.upgradeMultiplier = upgradeMultiplier; //가격 배수
    }
}

public class Unit_Manager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI warriorText;
    public TextMeshProUGUI archerText;
    public TextMeshProUGUI monkText;
    public TextMeshProUGUI lancerText;
    public TextMeshProUGUI warriorBtnText;
    public TextMeshProUGUI archerBtnText;
    public TextMeshProUGUI monkBtnText;
    public TextMeshProUGUI lancerBtnText;

    [Header("유닛 데이터")]
    public UnitData warrior = new UnitData("워리어", 10, 2f, 2f);
    public UnitData archer = new UnitData("아처", 50, 5f, 2.5f);
    public UnitData monk = new UnitData("몽크", 200, 3f, 1.8f);
    public UnitData lancer = new UnitData("랜서", 500, 8f, 3f);

    void Start()
    {
        // 워리어는 기본으로 구매되어 있다고 가정
        warrior.purchased = true;
        warrior.level = 1;
        UpdateUI();
    }
    private void Update()
    {
        {
            // 항상 Mouse_Click의 골드 표시 갱신
            goldText.text = $"Gold: {Mouse_Click.instance.gold:F0}";
        }
    }
    public void BuyOrUpgradeWarrior()
    {
        BuyOrUpgradeUnit(warrior);
    }
    public void BuyOrUpgradeArcher()
    {
        BuyOrUpgradeUnit(archer);
    }

    public void BuyOrUpgradeMonk()
    {
        BuyOrUpgradeUnit(monk);
    }

    public void BuyOrUpgradeLancer()
    {
        BuyOrUpgradeUnit(lancer);
    }

    public void BuyOrUpgradeUnit(UnitData unit)
    {
        if (!unit.purchased)
        {
            if (Mouse_Click.instance.gold >= unit.cost)
            {
                Mouse_Click.instance.gold -= unit.cost;
                unit.purchased = true;
                unit.level = 1;
                unit.cost = Mathf.RoundToInt(unit.cost * unit.upgradeMultiplier);
                Debug.Log($"{unit.name} 구매 완료! 공격력 {unit.attack}");
            }
            else
            {
                Debug.Log("골드가 부족합니다!");
            }
        }
        else
        {
            if (Mouse_Click.instance.gold >= unit.cost)
            {
                Mouse_Click.instance.gold -= unit.cost;
                unit.level++;
                unit.attack *= unit.upgradeMultiplier;
                unit.cost = Mathf.RoundToInt(unit.cost * unit.upgradeMultiplier);

                Debug.Log($"{unit.name} 업그레이드 완료! Lv.{unit.level} / 공격력 {unit.attack}");
            }
            else
            {
                Debug.Log("골드가 부족합니다!");
            }
        }

        Mouse_Click.instance.UpdateUI();
        UpdateUI();

    }

    void UpdateUI()
    {
        warriorBtnText.text = warrior.purchased ? "업그레이드" : "구매";
        archerBtnText.text = archer.purchased ? "업그레이드" : "구매";
        monkBtnText.text = monk.purchased ? "업그레이드" : "구매";
        lancerBtnText.text = lancer.purchased ? "업그레이드" : "구매";

        warriorText.text = $"(Lv.{warrior.level}) 가격: {warrior.cost}G\nAtk: {warrior.attack.ToString("F1")}";
        archerText.text = $"(Lv.{archer.level}) 가격: {archer.cost}G\nAtk: {archer.attack.ToString("F1")}";
        monkText.text = $"(Lv.{monk.level}) 가격: {monk.cost}G\nAtk: {monk.attack.ToString("F1")}";
        lancerText.text = $"(Lv.{lancer.level}) 가격: {lancer.cost}G\nAtk: {lancer.attack.ToString("F1")}";
    }
}
