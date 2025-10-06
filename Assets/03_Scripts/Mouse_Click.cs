using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mouse_Click : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 점수를 표시할 UI
    private int score = 0;         // 점수 변수
    private int clickPower = 1;    // 클릭당 점수 증가량

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        // 마우스 클릭 또는 터치 시작 시 점수 증가
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
