using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mouse_Click : MonoBehaviour
{
    public static Mouse_Click instance;

    [Header("UI")]
    public TextMeshProUGUI goldText;       // 골드 표시용
    public TextMeshProUGUI upgradeText;    // 업그레이드 버튼에 표시할 텍스트
    public TextMeshProUGUI perGoldText;
    public TextMeshProUGUI autoGoldA_Text;
    public TextMeshProUGUI autoGoldB_Text;
    public TextMeshProUGUI autoGoldA_Bt_Text;    //버튼 텍스트
    public TextMeshProUGUI autoGoldB_Bt_Text;


    public Button autoGoldA_Button;
    public Button autoGoldB_Button;

    [Header("Gold Values")]
    public int gold = 0;         // Gold 변수
    public int clickPower = 1;    // 클릭당 골드
    private int click_Cost= 10;       // 업그레이드 비용
    private int click_Level = 1;

    [Header("Auto Gold A")]
    private bool autoGoldA_Purchased = false;
    private float autoGoldA_Interval = 1.0f; //쿨타임
    private int autoGoldA_Amount = 1;
    private int autoGoldA_Cost = 100;
    private float autoGoldA_Timer = 0f;
    private int autoGoldA_Level = 0;

    [Header("Auto Gold B")]
    private bool autoGoldB_Purchased = false;
    private float autoGoldB_Interval = 3.0f; //쿨타임
    private int autoGoldB_Amount = 10;
    private int autoGoldB_Cost = 500;
    private float autoGoldB_Timer = 0f;
    private int autoGoldB_Level = 0;

    [Header("Unit UI")]
    public TextMeshProUGUI warriorText;
    public TextMeshProUGUI archerText;
    public TextMeshProUGUI monkText;
    public TextMeshProUGUI lancerText;
    public TextMeshProUGUI warriorBtnText;
    public TextMeshProUGUI archerBtnText;
    public TextMeshProUGUI monkBtnText;
    public TextMeshProUGUI lancerBtnText;

    public GameObject normal_shopUI; // ← 상점 UI 오브젝트 (활성/비활성 확인용)
    public GameObject battle_shopUI; // ← 상점 UI 오브젝트 (활성/비활성 확인용)

    private void Awake()
    {
        instance = this;

        // PlayerData로부터 불러오기
        var data = DataManager.instance.playerData;
        gold = data.GetData(GameData.Gold);
        clickPower = data.GetData(GameData.ClickPower);
        click_Cost = data.GetData(GameData.ClickCost);
        click_Level = data.GetData(GameData.ClickLevel);
        autoGoldA_Level = data.GetData(GameData.AutoGoldA_Level);
        autoGoldA_Cost = data.GetData(GameData.AutoGoldA_Cost);
        autoGoldA_Amount = data.GetData(GameData.AutoGoldA_Amount);
        autoGoldB_Level = data.GetData(GameData.AutoGoldB_Level);
        autoGoldB_Cost = data.GetData(GameData.AutoGoldB_Cost);
        autoGoldB_Amount = data.GetData(GameData.AutoGoldB_Amount);

        autoGoldA_Purchased = autoGoldA_Level > 0;
        autoGoldB_Purchased = autoGoldB_Level > 0;
    }

    void Start()
    {
        UpdateUI();

        autoGoldA_Button.onClick.AddListener(BuyOrUpgradeAutoGoldA);
        autoGoldB_Button.onClick.AddListener(BuyOrUpgradeAutoGoldB);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))     //마우스 클릭
        {
            if (EventSystem.current.IsPointerOverGameObject())    //UI 위를 클릭했다면 점수 증가를 무시
                return;
        if (normal_shopUI != null && normal_shopUI.activeSelf || battle_shopUI.activeSelf)            // 2. 상점이 열려 있을 때도 무시
            return;

            AddGold();
        }

        // 모바일 터치
        /*        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    // UI 위 터치 감지
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        return;

                    if (shopUI != null && shopUI.activeSelf)            // 2. 상점이 열려 있을 때도 무시
                        return;
                    AddGold();
                }*/

        // 자동채굴 A
        if (autoGoldA_Purchased)
        {
            autoGoldA_Timer += Time.deltaTime;
            if (autoGoldA_Timer >= autoGoldA_Interval)
            {
                gold += autoGoldA_Amount;
                autoGoldA_Timer = 0f;
                UpdateUI();
            }
        }

        // 자동채굴 B
        if (autoGoldB_Purchased)
        {
            autoGoldB_Timer += Time.deltaTime;
            if (autoGoldB_Timer >= autoGoldB_Interval)
            {
                gold += autoGoldB_Amount;
                autoGoldB_Timer = 0f;
                UpdateUI();
            }
        }
    }

    public void UpgradeClickPower()
    {
        if (gold >= click_Cost)
        {
            gold -= click_Cost;        // 비용 지불
            clickPower *= 2;            // 클릭당 골드 2배로 증가
            click_Cost*= 2;           // 다음 업그레이드 비용 2배 증가
            click_Level++;

            UpdateUI();

            DataManager.instance.playerData.SetData(GameData.Gold, gold);
            DataManager.instance.playerData.SetData(GameData.ClickPower, clickPower);
            DataManager.instance.playerData.SetData(GameData.ClickCost, click_Cost);
            DataManager.instance.playerData.SetData(GameData.ClickLevel, click_Level);
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
        }
    }

    // 자동채굴 A
    void BuyOrUpgradeAutoGoldA()
    {
        // 아직 구매 전이라면 구매 로직
        if (!autoGoldA_Purchased)
        {
            if (gold >= autoGoldA_Cost)
            {
                gold -= autoGoldA_Cost;
                autoGoldA_Purchased = true;
                autoGoldA_Level = 1;

                Debug.Log("자동채굴기 A 작동 시작!");
                autoGoldA_Cost *= 2;

                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Level, autoGoldA_Level);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Cost, autoGoldA_Cost);
            }
            else
            {
                Debug.Log("골드가 부족합니다! (A)");
            }
        }
        else
        {
            // 이미 구매했다면 업그레이드 로직
            if (gold >= autoGoldA_Cost)
            {
                gold -= autoGoldA_Cost;
                autoGoldA_Amount *= 2;    // 자동채굴량 2배
                autoGoldA_Cost *= 2;      // 업그레이드 비용도 2배 증가
                autoGoldA_Level++;

                Debug.Log($"자동채굴기 A 업그레이드 완료! 현재 채굴량: {autoGoldA_Amount}");
                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Amount, autoGoldA_Amount);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Cost, autoGoldA_Cost);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Level, autoGoldA_Level);
            }
            else
            {
                Debug.Log("골드가 부족합니다! (A 업그레이드)");
            }
        }
    }

    // 자동채굴 B
    void BuyOrUpgradeAutoGoldB()
    {
        // 아직 구매 전이라면 구매 로직
        if (!autoGoldB_Purchased)
        {
            if (gold >= autoGoldB_Cost)
            {
                gold -= autoGoldB_Cost;
                autoGoldB_Purchased = true;
                autoGoldB_Level = 1;

                Debug.Log("자동채굴기 B 작동 시작!");
                autoGoldB_Cost *= 2;

                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Level, autoGoldB_Level);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Cost, autoGoldB_Cost);
            }
            else
            {
                Debug.Log("골드가 부족합니다! (B)");
            }
        }
        else
        {
            // 이미 구매했다면 업그레이드 로직
            if (gold >= autoGoldB_Cost)
            {
                gold -= autoGoldB_Cost;
                autoGoldB_Amount *= 2;    // 자동채굴량 2배
                autoGoldB_Cost *= 2;      // 업그레이드 비용 2배 증가
                autoGoldB_Level++;

                Debug.Log($"자동채굴기 B 업그레이드 완료! 현재 채굴량: {autoGoldB_Amount}");
                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Amount, autoGoldB_Amount);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Cost, autoGoldB_Cost);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Level, autoGoldB_Level);
            }
            else
            {
                Debug.Log("골드가 부족합니다! (B 업그레이드)");
            }
        }
    }
    public void UpdateUI()
    {
        // 기존 골드 및 자동채굴 UI 갱신
        goldText.text = $"Gold: {gold:F0}";
        upgradeText.text = $"(Lv.{click_Level}) 가격: {click_Cost:F0}G\n클릭당: {clickPower:F0}Gold";
        perGoldText.text = $"클릭당: {clickPower:F0}Gold";

        autoGoldA_Bt_Text.text = autoGoldA_Purchased ? "업그레이드" : "구매";
        autoGoldA_Text.text = $"(Lv.{autoGoldA_Level}) 가격: {autoGoldA_Cost:F0}G\n{autoGoldA_Amount:F0}Gold/1초";

        autoGoldB_Bt_Text.text = autoGoldB_Purchased ? "업그레이드" : "구매";
        autoGoldB_Text.text = $"(Lv.{autoGoldB_Level}) 가격: {autoGoldB_Cost:F0}G\n{autoGoldB_Amount:F0}Gold/3초";

        // 유닛 UI 갱신
        var units = Unit_Manager.instance.units;

        warriorBtnText.text = units["워리어"].purchased ? "업그레이드" : "구매";
        archerBtnText.text = units["아처"].purchased ? "업그레이드" : "구매";
        monkBtnText.text = units["몽크"].purchased ? "업그레이드" : "구매";
        lancerBtnText.text = units["랜서"].purchased ? "업그레이드" : "구매";

        warriorText.text = $"(Lv.{units["워리어"].level}) 가격: {units["워리어"].cost}G\nAtk: {units["워리어"].attack:F0}";
        archerText.text = $"(Lv.{units["아처"].level}) 가격: {units["아처"].cost}G\nAtk: {units["아처"].attack:F0}";
        monkText.text = $"(Lv.{units["몽크"].level}) 가격: {units["몽크"].cost}G\nAtk: {units["몽크"].attack:F0}";
        lancerText.text = $"(Lv.{units["랜서"].level}) 가격: {units["랜서"].cost}G\nAtk: {units["랜서"].attack:F0}";
    }

    void GoldData_Update(int gold)
    {
        DataManager.instance.playerData.SetData(GameData.Gold, gold);
    }
    
    void AddGold()
    {
        gold += clickPower;
        UpdateUI();
        GoldData_Update(gold);
    }
}


