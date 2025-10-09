using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // new�� ����! ���� ������Ʈ�� �����Ǵ� Ŭ������ �ƴ϶�.
            playerData = new PlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}