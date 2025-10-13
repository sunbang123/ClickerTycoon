using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitDataManager : MonoBehaviour
{
    private Unit_Manager unitManager;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitUntil(() => Unit_Manager.instance != null);
        unitManager = Unit_Manager.instance;

        Debug.Log("<color=green>UnitDataManager 초기화 완료</color>");
        PrintAllUnitInfo();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Unit_Manager.instance != null)
        {
            unitManager = Unit_Manager.instance;
            Debug.Log($"새로운 씬({scene.name})에서 Unit_Manager 다시 연결됨");

            if (Mouse_Click.instance != null)
                Mouse_Click.instance.UpdateUI(); // UI 갱신은 Mouse_Click에서
        }
    }

    public void UpgradeArcherFromAnotherScene()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager가 아직 준비되지 않았습니다.");
            return;
        }

        unitManager.BuyOrUpgradeUnit("아처");

        if (Mouse_Click.instance != null)
            Mouse_Click.instance.UpdateUI();

        Debug.Log("다른 씬에서 아처 업그레이드 실행됨");
    }

    public void PrintAllUnitInfo()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager가 아직 연결되지 않았습니다.");
            return;
        }

        foreach (var pair in unitManager.units)
        {
            string name = pair.Key;
            UnitData unit = pair.Value;
            Debug.Log($"{name}: Lv.{unit.level} / 공격력 {unit.attack}");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
