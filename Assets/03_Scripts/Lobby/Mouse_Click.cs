using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Mouse_Click : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // ������ ǥ���� UI
    private int gold = 0;         // Gold ����
    private int clickPower = 1;    // Ŭ���� ���� ������

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))     //���콺 Ŭ��
        {
            if (EventSystem.current.IsPointerOverGameObject())    //UI ���� Ŭ���ߴٸ� ���� ������ ����
                return;

            AddGold();
        }

        // ����� ��ġ
/*        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // UI �� ��ġ ����
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            AddGold();
        }*/
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Gold: {gold:F0}";
    }

    void AddGold()
    {
        gold += clickPower;
        UpdateScoreText();
    }
}
