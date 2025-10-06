using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mouse_Click : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // ������ ǥ���� UI
    private int score = 0;         // ���� ����
    private int clickPower = 1;    // Ŭ���� ���� ������

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        // ���콺 Ŭ�� �Ǵ� ��ġ ���� �� ���� ����
        if (Input.GetMouseButtonDown(0) ||
           (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            score += clickPower;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Gold: {score:F0}";
    }
}
