using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Mouse_Click : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 점수를 표시할 UI
    private int gold = 0;         // Gold 변수
    private int clickPower = 1;    // 클릭당 점수 증가량

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))     //마우스 클릭
        {
            if (EventSystem.current.IsPointerOverGameObject())    //UI 위를 클릭했다면 점수 증가를 무시
                return;

            AddGold();
        }

        // 모바일 터치
/*        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // UI 위 터치 감지
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
