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

            // new로 생성! 게임 오브젝트에 부착되는 클래스가 아니라서.
            playerData = new PlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}