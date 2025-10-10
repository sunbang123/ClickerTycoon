using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitDataManager : MonoBehaviour
{
    private Unit_Manager unitManager;

    private void Awake()
    {
        // 이 스크립트는 씬마다 따로 생길 수 있으므로 자동 제거 X
        // 대신 씬이 로드될 때마다 Unit_Manager를 재연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        // Unit_Manager가 생성될 때까지 대기
        yield return new WaitUntil(() => Unit_Manager.instance != null);
        unitManager = Unit_Manager.instance;

        Debug.Log("<color=green> UnitDataManager 초기화 완료</color>");
        PrintAllUnitInfo();
    }

    // 씬 전환 시 다시 연결
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Unit_Manager.instance != null)
        {
            unitManager = Unit_Manager.instance;
            Debug.Log($"새로운 씬({scene.name})에서 Unit_Manager 다시 연결됨");
        }
    }

    public void UpgradeArcherFromAnotherScene()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager가 아직 준비되지 않았습니다.");
            return;
        }

        unitManager.BuyOrUpgradeArcher();
        Debug.Log(" 다른 씬에서 아처 업그레이드 실행됨");
    }

    public void PrintAllUnitInfo()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager가 아직 연결되지 않았습니다.");
            return;
        }

        Debug.Log($"워리어: Lv.{unitManager.warrior.level} / 공격력 {unitManager.warrior.attack}");
        Debug.Log($"아처: Lv.{unitManager.archer.level} / 공격력 {unitManager.archer.attack}");
        Debug.Log($"몽크: Lv.{unitManager.monk.level} / 공격력 {unitManager.monk.attack}");
        Debug.Log($"랜서: Lv.{unitManager.lancer.level} / 공격력 {unitManager.lancer.attack}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}