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
    // 싱글톤 추가
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

    [Header("유닛 데이터")]
    public UnitData warrior = new UnitData("워리어", 10, 2f, 2f);
    public UnitData archer = new UnitData("아처", 50, 5f, 2.5f);
    public UnitData monk = new UnitData("몽크", 200, 3f, 1.8f);
    public UnitData lancer = new UnitData("랜서", 500, 8f, 3f);

    private void Awake()
    {
        // 싱글톤 유지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
    }

    void Start()
    {
        // 워리어는 기본 구매 상태
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
        // 새 씬에서 UI 다시 찾기 (이름이 같다는 전제)
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
            Debug.LogWarning("Mouse_Click 인스턴스가 없습니다!");
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
        if (warriorBtnText == null) return;

        warriorBtnText.text = warrior.purchased ? "업그레이드" : "구매";
        archerBtnText.text = archer.purchased ? "업그레이드" : "구매";
        monkBtnText.text = monk.purchased ? "업그레이드" : "구매";
        lancerBtnText.text = lancer.purchased ? "업그레이드" : "구매";

        warriorText.text = $"(Lv.{warrior.level}) 가격: {warrior.cost}G\nAtk: {warrior.attack:F0}";
        archerText.text = $"(Lv.{archer.level}) 가격: {archer.cost}G\nAtk: {archer.attack:F0}";
        monkText.text = $"(Lv.{monk.level}) 가격: {monk.cost}G\nAtk: {monk.attack:F0}";
        lancerText.text = $"(Lv.{lancer.level}) 가격: {lancer.cost}G\nAtk: {lancer.attack:F0}";
    }
}
