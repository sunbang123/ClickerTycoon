using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spawner : MonoBehaviour
{
    static public Monster_Spawner instance;

    [Header("Monster Settings")]
    public GameObject monsterPrefab;
    public Transform spawnPoints;
    public Transform[] setWaypoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnMonster", 2f, 5f);
    }

    public void SpawnMonster()
    {
        Instantiate(monsterPrefab, spawnPoints.position, Quaternion.identity);
    }
}
