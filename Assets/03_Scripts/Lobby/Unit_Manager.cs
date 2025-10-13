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
    public static Unit_Manager instance;

    public Dictionary<string, UnitData> units = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        units["������"] = new UnitData("������", 10, 2f, 2f);
        units["��ó"] = new UnitData("��ó", 50, 5f, 2.5f);
        units["��ũ"] = new UnitData("��ũ", 200, 3f, 1.8f);
        units["����"] = new UnitData("����", 500, 8f, 3f);
    }

    void Start()
    {
        if (!units["������"].purchased)
        {
            units["������"].purchased = true;
            units["������"].level = 1;
        }
    }

    public void BuyOrUpgradeUnit(string unitName)
    {
        if (!units.ContainsKey(unitName)) return;
        var unit = units[unitName];

        if (Mouse_Click.instance == null) return;

        if (!unit.purchased)
        {
            if (Mouse_Click.instance.gold >= unit.cost)
            {
                Mouse_Click.instance.gold -= unit.cost;
                unit.purchased = true;
                unit.level = 1;
                unit.cost = Mathf.RoundToInt(unit.cost * unit.upgradeMultiplier);
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
            }
        }

        Mouse_Click.instance.UpdateUI(); // UI�� Mouse_Click���� ó��
    }
}
