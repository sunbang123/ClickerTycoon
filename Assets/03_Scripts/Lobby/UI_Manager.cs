using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("UI Info")]
    public TextMeshProUGUI levelText;

    [Header("Player Info")]
    public int Level;

    void Start()
    {
        int Level = DataManager.instance.playerData.GetData(GameData.Player_Level);
        levelText.text = $"Level: {Level}";
    }
}