using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mouse_Click : MonoBehaviour
{
    [Header("UI Texts")]
    public TextMeshProUGUI goldText;       // ��� ǥ�ÿ�
    public TextMeshProUGUI upgradeText;    // ���׷��̵� ��ư�� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI perGoldText;
    public TextMeshProUGUI autoGoldAText;
    public TextMeshProUGUI autoGoldBText;


    public Button autoGoldAButton;
    public Button autoGoldBButton;


    public GameObject shopUI; // �� ���� UI ������Ʈ (Ȱ��/��Ȱ�� Ȯ�ο�)

    [Header("Values")]
    public int gold = 0;         // Gold ����
    public int clickPower = 1;    // Ŭ���� ���
    private int upgradeCost = 10;       // ���׷��̵� ���

    // �ڵ�ä�� A
    private bool autoGoldA_Purchased = false;
    private float autoGoldA_Interval = 1.0f;
    private int autoGoldA_Amount = 1;
    private int autoGoldA_Cost = 100;
    private float autoGoldA_Timer = 0f;

    // �ڵ�ä�� B
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
        if (Input.GetMouseButtonDown(0))     //���콺 Ŭ��
        {
            if (EventSystem.current.IsPointerOverGameObject())    //UI ���� Ŭ���ߴٸ� ���� ������ ����
                return;
        if (shopUI != null && shopUI.activeSelf)            // 2. ������ ���� ���� ���� ����
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

    void BuyAutoGoldA()
    {
        if (autoGoldA_Purchased)
        {
            Debug.Log("�ڵ�ä���� A�� �̹� �����߽��ϴ�!");
            return;
        }

        if (gold >= autoGoldA_Cost)
        {
            gold -= autoGoldA_Cost;
            autoGoldA_Purchased = true;
            Debug.Log("�ڵ�ä���� A �۵� ����!");
            UpdateUI();
        }
        else
        {
            Debug.Log("��尡 �����մϴ�!");
        }
    }

    void BuyAutoGoldB()
    {
        if (autoGoldB_Purchased)
        {
            Debug.Log("�ڵ�ä���� B�� �̹� �����߽��ϴ�!");
            return;
        }

        if (gold >= autoGoldB_Cost)
        {
            gold -= autoGoldB_Cost;
            autoGoldB_Purchased = true;
            Debug.Log("�ڵ�ä���� B �۵� ����!");
            UpdateUI();
        }
        else
        {
            Debug.Log("��尡 �����մϴ�!");
        }
    }

    public void UpgradeClickPower()
    {
        if (gold >= upgradeCost)
        {
            gold -= upgradeCost;        // ��� ����
            clickPower *= 2;            // Ŭ���� ��� 2��� ����
            upgradeCost *= 2;           // ���� ���׷��̵� ��� 2�� ����
            UpdateUI();
        }
        else
        {
            Debug.Log("��尡 �����մϴ�!");
        }
    }

    public void UpgradeAutoGoldA()
    {
        if (gold >= upgradeCost && autoGoldA_Purchased)
        {
            gold -= autoGoldA_Cost;        // ��� ����
            autoGoldA_Amount *= 2;            // Ŭ���� ��� 2��� ����
            autoGoldA_Cost *= 2;           // ���� ���׷��̵� ��� 2�� ����
            UpdateUI();
        }
    }

    public void UpgradeAutoGoldB()
    {
        if (gold >= upgradeCost && autoGoldB_Purchased)
        {
            gold -= autoGoldB_Cost;        // ��� ����
            autoGoldB_Amount *= 2;            // Ŭ���� ��� 2��� ����
            autoGoldB_Cost *= 2;           // ���� ���׷��̵� ��� 2�� ����
            UpdateUI();
        }
    }



    void UpdateUI()
    {
        goldText.text = $"Gold: {gold:F0}";
        upgradeText.text = $"���׷��̵�: {upgradeCost:F0}";
        perGoldText.text = $"Ŭ���� ���: {clickPower:F0}";
        autoGoldAText.text = $"���׷��̵�: {autoGoldA_Cost:F0}";
        autoGoldBText.text = $"���׷��̵�: {autoGoldB_Cost:F0}";
    }

    void AddGold()
    {
        gold += clickPower;
        UpdateUI();
    }
}
