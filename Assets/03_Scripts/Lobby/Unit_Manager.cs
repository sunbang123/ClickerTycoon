using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable] //  인스펙터에서도 보이게 함
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

        units["워리어"] = new UnitData("워리어", 10, 2f, 2f);
        units["아처"] = new UnitData("아처", 50, 5f, 2.5f);
        units["몽크"] = new UnitData("몽크", 200, 3f, 1.8f);
        units["랜서"] = new UnitData("랜서", 500, 8f, 3f);
    }

    void Start()
    {
        if (!units["워리어"].purchased)
        {
            units["워리어"].purchased = true;
            units["워리어"].level = 1;
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

        Mouse_Click.instance.UpdateUI(); // UI는 Mouse_Click에서 처리
    }
}
