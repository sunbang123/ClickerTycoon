using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitDataManager : MonoBehaviour
{
    private Unit_Manager unitManager;

    private void Awake()
    {
        // �� ��ũ��Ʈ�� ������ ���� ���� �� �����Ƿ� �ڵ� ���� X
        // ��� ���� �ε�� ������ Unit_Manager�� �翬��
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        // Unit_Manager�� ������ ������ ���
        yield return new WaitUntil(() => Unit_Manager.instance != null);
        unitManager = Unit_Manager.instance;

        Debug.Log("<color=green> UnitDataManager �ʱ�ȭ �Ϸ�</color>");
        PrintAllUnitInfo();
    }

    // �� ��ȯ �� �ٽ� ����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Unit_Manager.instance != null)
        {
            unitManager = Unit_Manager.instance;
            Debug.Log($"���ο� ��({scene.name})���� Unit_Manager �ٽ� �����");
        }
    }

    public void UpgradeArcherFromAnotherScene()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager�� ���� �غ���� �ʾҽ��ϴ�.");
            return;
        }

        unitManager.BuyOrUpgradeArcher();
        Debug.Log(" �ٸ� ������ ��ó ���׷��̵� �����");
    }

    public void PrintAllUnitInfo()
    {
        if (unitManager == null)
        {
            Debug.LogWarning("Unit_Manager�� ���� ������� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log($"������: Lv.{unitManager.warrior.level} / ���ݷ� {unitManager.warrior.attack}");
        Debug.Log($"��ó: Lv.{unitManager.archer.level} / ���ݷ� {unitManager.archer.attack}");
        Debug.Log($"��ũ: Lv.{unitManager.monk.level} / ���ݷ� {unitManager.monk.attack}");
        Debug.Log($"����: Lv.{unitManager.lancer.level} / ���ݷ� {unitManager.lancer.attack}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}