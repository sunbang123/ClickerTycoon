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

        Debug.Log("<color=green>UnitDataManager �ʱ�ȭ �Ϸ�</color>");
        PrintAllUnitInfo();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Unit_Manager.instance != null)
        {
            unitManager = Unit_Manager.instance;
            Debug.Log($"���ο� ��({scene.name})���� Unit_Manager �ٽ� �����");

            if (Mouse_Click.instance != null)
                Mouse_Click.instance.UpdateUI(); // UI ������ Mouse_Click����
        }
    }

    public void UpgradeArcherFromAnotherScene()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager�� ���� �غ���� �ʾҽ��ϴ�.");
            return;
        }

        unitManager.BuyOrUpgradeUnit("��ó");

        if (Mouse_Click.instance != null)
            Mouse_Click.instance.UpdateUI();

        Debug.Log("�ٸ� ������ ��ó ���׷��̵� �����");
    }

    public void PrintAllUnitInfo()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager�� ���� ������� �ʾҽ��ϴ�.");
            return;
        }

        foreach (var pair in unitManager.units)
        {
            string name = pair.Key;
            UnitData unit = pair.Value;
            Debug.Log($"{name}: Lv.{unit.level} / ���ݷ� {unit.attack}");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
