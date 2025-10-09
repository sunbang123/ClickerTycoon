using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit_Manager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI warrior_text;
    public TextMeshProUGUI archer_text;
    public TextMeshProUGUI monk_text;
    public TextMeshProUGUI lancer_text;

    public TextMeshProUGUI warrior_bt_text;
    public TextMeshProUGUI archer_bt_text;
    public TextMeshProUGUI monk_bt_text;
    public TextMeshProUGUI lancer_bt_text;

    [Header("warrior")]
    private bool warrior_Purchased = true;
    private int warrior_Level = 1;
    private int warrior_Cost = 10;

    [Header("archer")]
    private bool archer_Purchased = false;
    private int archer_Level = 0;
    private int archer_Cost = 50;

    [Header("monk")]
    private bool monk_Purchased = false;
    private int monk_Level = 0;
    private int monk_Cost = 200;

    [Header("lancer")]
    private bool lancer_Purchased = false;
    private int lancer_Level = 0;
    private int lancer_Cost = 500;


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        string aState = warrior_Purchased ? "���׷��̵�" : "����";
        warrior_bt_text.text = $"{aState}";

        warrior_text.text = $"(Lv.{warrior_Level}) ����: {warrior_Cost:F0}G\n//warrior����";

        string bState = archer_Purchased ? "���׷��̵�" : "����";
        archer_bt_text.text = $"{bState}";

        archer_text.text = $"(Lv.{archer_Level}) ����: {archer_Cost:F0}G\n//archer����";

        string cState = monk_Purchased ? "���׷��̵�" : "����";
        monk_bt_text.text = $"{cState}";

        monk_text.text = $"(Lv.{monk_Level}) ����: {monk_Cost:F0}G\n//monk����";

        string dState = lancer_Purchased ? "���׷��̵�" : "����";
        lancer_bt_text.text = $"{dState}";

        lancer_text.text = $"(Lv.{lancer_Level}) ����: {lancer_Cost:F0}G\n//lancer����";
    }
}
