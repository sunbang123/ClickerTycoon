using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mouse_Click : MonoBehaviour
{
    [Header("UI Texts")]
    public TextMeshProUGUI goldText;       // 골드 표시용
    public TextMeshProUGUI upgradeText;    // 업그레이드 버튼에 표시할 텍스트
    public TextMeshProUGUI perGoldText;
    public TextMeshProUGUI autoGoldAText;
    public TextMeshProUGUI autoGoldBText;


    public Button autoGoldAButton;
    public Button autoGoldBButton;


    public GameObject shopUI; // ← 상점 UI 오브젝트 (활성/비활성 확인용)

    [Header("Values")]
    public int gold = 0;         // Gold 변수
    public int clickPower = 1;    // 클릭당 골드
    private int upgradeCost = 10;       // 업그레이드 비용

    // 자동채굴 A
    private bool autoGoldA_Purchased = false;
    private float autoGoldA_Interval = 1.0f;
    private int autoGoldA_Amount = 1;
    private int autoGoldA_Cost = 100;
    private float autoGoldA_Timer = 0f;

    // 자동채굴 B
    private bool autoGoldB_Purchased = false;
    private float autoGoldB_Interval = 3.0f;
    private int autoGoldB_Amount = 10;
    private int autoGoldB_Cost = 500;
    private float autoGoldB_Timer = 0f;


    void Start()
    {
        UpdateUI();

        autoGoldAButton.onClick.AddListener(BuyAutoGoldA);
        autoGoldBButton.onClick.AddListener(BuyAutoGoldB);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))     //마우스 클릭
        {
            if (EventSystem.current.IsPointerOverGameObject())    //UI 위를 클릭했다면 점수 증가를 무시
                return;
        if (shopUI != null && shopUI.activeSelf)            // 2. 상점이 열려 있을 때도 무시
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

    void BuyAutoGoldA()
    {
        if (autoGoldA_Purchased)
        {
            Debug.Log("자동채굴기 A는 이미 구매했습니다!");
            return;
        }

        if (gold >= autoGoldA_Cost)
        {
            gold -= autoGoldA_Cost;
            autoGoldA_Purchased = true;
            Debug.Log("자동채굴기 A 작동 시작!");
            UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
        }
    }

    void BuyAutoGoldB()
    {
        if (autoGoldB_Purchased)
        {
            Debug.Log("자동채굴기 B는 이미 구매했습니다!");
            return;
        }

        if (gold >= autoGoldB_Cost)
        {
            gold -= autoGoldB_Cost;
            autoGoldB_Purchased = true;
            Debug.Log("자동채굴기 B 작동 시작!");
            UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
        }
    }

    public void UpgradeClickPower()
    {
        if (gold >= upgradeCost)
        {
            gold -= upgradeCost;        // 비용 지불
            clickPower *= 2;            // 클릭당 골드 2배로 증가
            upgradeCost *= 2;           // 다음 업그레이드 비용 2배 증가
            UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
        }
    }

    public void UpgradeAutoGoldA()
    {
        if (gold >= upgradeCost && autoGoldA_Purchased)
        {
            gold -= autoGoldA_Cost;        // 비용 지불
            autoGoldA_Amount *= 2;            // 클릭당 골드 2배로 증가
            autoGoldA_Cost *= 2;           // 다음 업그레이드 비용 2배 증가
            UpdateUI();
        }
    }

    public void UpgradeAutoGoldB()
    {
        if (gold >= upgradeCost && autoGoldB_Purchased)
        {
            gold -= autoGoldB_Cost;        // 비용 지불
            autoGoldB_Amount *= 2;            // 클릭당 골드 2배로 증가
            autoGoldB_Cost *= 2;           // 다음 업그레이드 비용 2배 증가
            UpdateUI();
        }
    }



    void UpdateUI()
    {
        goldText.text = $"Gold: {gold:F0}";
        upgradeText.text = $"업그레이드: {upgradeCost:F0}";
        perGoldText.text = $"클릭당 골드: {clickPower:F0}";
        autoGoldAText.text = $"업그레이드: {autoGoldA_Cost:F0}";
        autoGoldBText.text = $"업그레이드: {autoGoldB_Cost:F0}";
    }

    void AddGold()
    {
        gold += clickPower;
        UpdateUI();
    }
}
