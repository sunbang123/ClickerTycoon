using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Spawner : MonoBehaviour
{
    static public Unit_Spawner instance;

    [Header("Unit Settings")]
    public GameObject[] unitPrefabs;
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
        InvokeRepeating("SpawnUnit", 2f, 5f);
    }

    public void SpawnUnit()
    {
        int i = Random.Range(0, unitPrefabs.Length);
        Instantiate(unitPrefabs[i], spawnPoints.position, Quaternion.identity);
    }
}
