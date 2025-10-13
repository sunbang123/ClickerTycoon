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
    public TextMeshProUGUI goldText;       // ��� ǥ�ÿ�
    public TextMeshProUGUI upgradeText;    // ���׷��̵� ��ư�� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI perGoldText;
    public TextMeshProUGUI autoGoldA_Text;
    public TextMeshProUGUI autoGoldB_Text;
    public TextMeshProUGUI autoGoldA_Bt_Text;    //��ư �ؽ�Ʈ
    public TextMeshProUGUI autoGoldB_Bt_Text;


    public Button autoGoldA_Button;
    public Button autoGoldB_Button;

    [Header("Gold Values")]
    public int gold = 0;         // Gold ����
    public int clickPower = 1;    // Ŭ���� ���
    private int click_Cost= 10;       // ���׷��̵� ���
    private int click_Level = 1;

    [Header("Auto Gold A")]
    private bool autoGoldA_Purchased = false;
    private float autoGoldA_Interval = 1.0f; //��Ÿ��
    private int autoGoldA_Amount = 1;
    private int autoGoldA_Cost = 100;
    private float autoGoldA_Timer = 0f;
    private int autoGoldA_Level = 0;

    [Header("Auto Gold B")]
    private bool autoGoldB_Purchased = false;
    private float autoGoldB_Interval = 3.0f; //��Ÿ��
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

    public GameObject normal_shopUI; // �� ���� UI ������Ʈ (Ȱ��/��Ȱ�� Ȯ�ο�)
    public GameObject battle_shopUI; // �� ���� UI ������Ʈ (Ȱ��/��Ȱ�� Ȯ�ο�)

    private void Awake()
    {
        instance = this;

        // PlayerData�κ��� �ҷ�����
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
        if (Input.GetMouseButtonDown(0))     //���콺 Ŭ��
        {
            if (EventSystem.current.IsPointerOverGameObject())    //UI ���� Ŭ���ߴٸ� ���� ������ ����
                return;
        if (normal_shopUI != null && normal_shopUI.activeSelf || battle_shopUI.activeSelf)            // 2. ������ ���� ���� ���� ����
            return;

            AddGold();
        }

        // ����� ��ġ
        /*        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    // UI �� ��ġ ����
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        return;

                    if (shopUI != null && shopUI.activeSelf)            // 2. ������ ���� ���� ���� ����
                        return;
                    AddGold();
                }*/

        // �ڵ�ä�� A
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

        // �ڵ�ä�� B
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
            gold -= click_Cost;        // ��� ����
            clickPower *= 2;            // Ŭ���� ��� 2��� ����
            click_Cost*= 2;           // ���� ���׷��̵� ��� 2�� ����
            click_Level++;

            UpdateUI();

            DataManager.instance.playerData.SetData(GameData.Gold, gold);
            DataManager.instance.playerData.SetData(GameData.ClickPower, clickPower);
            DataManager.instance.playerData.SetData(GameData.ClickCost, click_Cost);
            DataManager.instance.playerData.SetData(GameData.ClickLevel, click_Level);
        }
        else
        {
            Debug.Log("��尡 �����մϴ�!");
        }
    }

    // �ڵ�ä�� A
    void BuyOrUpgradeAutoGoldA()
    {
        // ���� ���� ���̶�� ���� ����
        if (!autoGoldA_Purchased)
        {
            if (gold >= autoGoldA_Cost)
            {
                gold -= autoGoldA_Cost;
                autoGoldA_Purchased = true;
                autoGoldA_Level = 1;

                Debug.Log("�ڵ�ä���� A �۵� ����!");
                autoGoldA_Cost *= 2;

                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Level, autoGoldA_Level);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Cost, autoGoldA_Cost);
            }
            else
            {
                Debug.Log("��尡 �����մϴ�! (A)");
            }
        }
        else
        {
            // �̹� �����ߴٸ� ���׷��̵� ����
            if (gold >= autoGoldA_Cost)
            {
                gold -= autoGoldA_Cost;
                autoGoldA_Amount *= 2;    // �ڵ�ä���� 2��
                autoGoldA_Cost *= 2;      // ���׷��̵� ��뵵 2�� ����
                autoGoldA_Level++;

                Debug.Log($"�ڵ�ä���� A ���׷��̵� �Ϸ�! ���� ä����: {autoGoldA_Amount}");
                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Amount, autoGoldA_Amount);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Cost, autoGoldA_Cost);
                DataManager.instance.playerData.SetData(GameData.AutoGoldA_Level, autoGoldA_Level);
            }
            else
            {
                Debug.Log("��尡 �����մϴ�! (A ���׷��̵�)");
            }
        }
    }

    // �ڵ�ä�� B
    void BuyOrUpgradeAutoGoldB()
    {
        // ���� ���� ���̶�� ���� ����
        if (!autoGoldB_Purchased)
        {
            if (gold >= autoGoldB_Cost)
            {
                gold -= autoGoldB_Cost;
                autoGoldB_Purchased = true;
                autoGoldB_Level = 1;

                Debug.Log("�ڵ�ä���� B �۵� ����!");
                autoGoldB_Cost *= 2;

                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Level, autoGoldB_Level);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Cost, autoGoldB_Cost);
            }
            else
            {
                Debug.Log("��尡 �����մϴ�! (B)");
            }
        }
        else
        {
            // �̹� �����ߴٸ� ���׷��̵� ����
            if (gold >= autoGoldB_Cost)
            {
                gold -= autoGoldB_Cost;
                autoGoldB_Amount *= 2;    // �ڵ�ä���� 2��
                autoGoldB_Cost *= 2;      // ���׷��̵� ��� 2�� ����
                autoGoldB_Level++;

                Debug.Log($"�ڵ�ä���� B ���׷��̵� �Ϸ�! ���� ä����: {autoGoldB_Amount}");
                UpdateUI();

                DataManager.instance.playerData.SetData(GameData.Gold, gold);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Amount, autoGoldB_Amount);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Cost, autoGoldB_Cost);
                DataManager.instance.playerData.SetData(GameData.AutoGoldB_Level, autoGoldB_Level);
            }
            else
            {
                Debug.Log("��尡 �����մϴ�! (B ���׷��̵�)");
            }
        }
    }
    public void UpdateUI()
    {
        // ���� ��� �� �ڵ�ä�� UI ����
        goldText.text = $"Gold: {gold:F0}";
        upgradeText.text = $"(Lv.{click_Level}) ����: {click_Cost:F0}G\nŬ����: {clickPower:F0}Gold";
        perGoldText.text = $"Ŭ����: {clickPower:F0}Gold";

        autoGoldA_Bt_Text.text = autoGoldA_Purchased ? "���׷��̵�" : "����";
        autoGoldA_Text.text = $"(Lv.{autoGoldA_Level}) ����: {autoGoldA_Cost:F0}G\n{autoGoldA_Amount:F0}Gold/1��";

        autoGoldB_Bt_Text.text = autoGoldB_Purchased ? "���׷��̵�" : "����";
        autoGoldB_Text.text = $"(Lv.{autoGoldB_Level}) ����: {autoGoldB_Cost:F0}G\n{autoGoldB_Amount:F0}Gold/3��";

        // ���� UI ����
        var units = Unit_Manager.instance.units;

        warriorBtnText.text = units["������"].purchased ? "���׷��̵�" : "����";
        archerBtnText.text = units["��ó"].purchased ? "���׷��̵�" : "����";
        monkBtnText.text = units["��ũ"].purchased ? "���׷��̵�" : "����";
        lancerBtnText.text = units["����"].purchased ? "���׷��̵�" : "����";

        warriorText.text = $"(Lv.{units["������"].level}) ����: {units["������"].cost}G\nAtk: {units["������"].attack:F0}";
        archerText.text = $"(Lv.{units["��ó"].level}) ����: {units["��ó"].cost}G\nAtk: {units["��ó"].attack:F0}";
        monkText.text = $"(Lv.{units["��ũ"].level}) ����: {units["��ũ"].cost}G\nAtk: {units["��ũ"].attack:F0}";
        lancerText.text = $"(Lv.{units["����"].level}) ����: {units["����"].cost}G\nAtk: {units["����"].attack:F0}";
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


