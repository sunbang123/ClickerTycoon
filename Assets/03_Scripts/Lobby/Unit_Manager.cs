using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable] //  �ν����Ϳ����� ���̰� ��
public class UnitData
{
    public string name;
    public int level = 0;
    public int cost;
    public float attack;
    public float upgradeMultiplier = 2f;
    public bool purchased = false;

    public UnitData(string name, int cost, float attack, float upgradeMultiplier)
    {
        this.name = name;
        this.cost = cost;
        this.attack = attack;
        this.upgradeMultiplier = upgradeMultiplier;
    }
}

public class Unit_Manager : MonoBehaviour
{
    // �̱��� �߰�
    public static Unit_Manager instance;

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

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� ����
            return;
        }
    }

    void Start()
    {
        // ������� �⺻ ���� ����
        if (!warrior.purchased)
        {
            warrior.purchased = true;
            warrior.level = 1;
        }

        UpdateUI();
    }

    private void Update()
    {
        if (goldText != null && Mouse_Click.instance != null)
        {
            goldText.text = $"Gold: {Mouse_Click.instance.gold:F0}";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ������ UI �ٽ� ã�� (�̸��� ���ٴ� ����)
        goldText = GameObject.Find("Gold_txt")?.GetComponent<TextMeshProUGUI>();
        warriorText = GameObject.Find("warrior_txt")?.GetComponent<TextMeshProUGUI>();
        archerText = GameObject.Find("archer_txt")?.GetComponent<TextMeshProUGUI>();
        monkText = GameObject.Find("monk_txt")?.GetComponent<TextMeshProUGUI>();
        lancerText = GameObject.Find("lancer_txt")?.GetComponent<TextMeshProUGUI>();

        warriorBtnText = GameObject.Find("warrior_bt_txt")?.GetComponent<TextMeshProUGUI>();
        archerBtnText = GameObject.Find("archer_bt_txt")?.GetComponent<TextMeshProUGUI>();
        monkBtnText = GameObject.Find("monk_bt_txt")?.GetComponent<TextMeshProUGUI>();
        lancerBtnText = GameObject.Find("lancer_bt_txt")?.GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }

    public void BuyOrUpgradeWarrior() => BuyOrUpgradeUnit(warrior);
    public void BuyOrUpgradeArcher() => BuyOrUpgradeUnit(archer);
    public void BuyOrUpgradeMonk() => BuyOrUpgradeUnit(monk);
    public void BuyOrUpgradeLancer() => BuyOrUpgradeUnit(lancer);

    public void BuyOrUpgradeUnit(UnitData unit)
    {
        if (Mouse_Click.instance == null)
        {
            Debug.LogWarning("Mouse_Click �ν��Ͻ��� �����ϴ�!");
            return;
        }

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
        if (warriorBtnText == null) return;

        warriorBtnText.text = warrior.purchased ? "���׷��̵�" : "����";
        archerBtnText.text = archer.purchased ? "���׷��̵�" : "����";
        monkBtnText.text = monk.purchased ? "���׷��̵�" : "����";
        lancerBtnText.text = lancer.purchased ? "���׷��̵�" : "����";

        warriorText.text = $"(Lv.{warrior.level}) ����: {warrior.cost}G\nAtk: {warrior.attack:F0}";
        archerText.text = $"(Lv.{archer.level}) ����: {archer.cost}G\nAtk: {archer.attack:F0}";
        monkText.text = $"(Lv.{monk.level}) ����: {monk.cost}G\nAtk: {monk.attack:F0}";
        lancerText.text = $"(Lv.{lancer.level}) ����: {lancer.cost}G\nAtk: {lancer.attack:F0}";
    }
}
