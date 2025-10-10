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

    // ������ (���û���)
    public UnitData(string name, int cost, float attack, float upgradeMultiplier)
    {
        this.name = name;
        this.cost = cost;
        this.attack = attack;
        this.upgradeMultiplier = upgradeMultiplier; //���� ���
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

    [Header("���� ������")]
    public UnitData warrior = new UnitData("������", 10, 2f, 2f);
    public UnitData archer = new UnitData("��ó", 50, 5f, 2.5f);
    public UnitData monk = new UnitData("��ũ", 200, 3f, 1.8f);
    public UnitData lancer = new UnitData("����", 500, 8f, 3f);

    void Start()
    {
        // ������� �⺻���� ���ŵǾ� �ִٰ� ����
        warrior.purchased = true;
        warrior.level = 1;
        UpdateUI();
    }
    private void Update()
    {
        {
            // �׻� Mouse_Click�� ��� ǥ�� ����
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
                Debug.Log($"{unit.name} ���� �Ϸ�! ���ݷ� {unit.attack}");
            }
            else
            {
                Debug.Log("��尡 �����մϴ�!");
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

                Debug.Log($"{unit.name} ���׷��̵� �Ϸ�! Lv.{unit.level} / ���ݷ� {unit.attack}");
            }
            else
            {
                Debug.Log("��尡 �����մϴ�!");
            }
        }

        Mouse_Click.instance.UpdateUI();
        UpdateUI();

    }

    void UpdateUI()
    {
        warriorBtnText.text = warrior.purchased ? "���׷��̵�" : "����";
        archerBtnText.text = archer.purchased ? "���׷��̵�" : "����";
        monkBtnText.text = monk.purchased ? "���׷��̵�" : "����";
        lancerBtnText.text = lancer.purchased ? "���׷��̵�" : "����";

        warriorText.text = $"(Lv.{warrior.level}) ����: {warrior.cost}G\nAtk: {warrior.attack.ToString("F1")}";
        archerText.text = $"(Lv.{archer.level}) ����: {archer.cost}G\nAtk: {archer.attack.ToString("F1")}";
        monkText.text = $"(Lv.{monk.level}) ����: {monk.cost}G\nAtk: {monk.attack.ToString("F1")}";
        lancerText.text = $"(Lv.{lancer.level}) ����: {lancer.cost}G\nAtk: {lancer.attack.ToString("F1")}";
    }
}
