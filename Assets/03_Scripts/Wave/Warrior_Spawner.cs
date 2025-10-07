using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Spawner : MonoBehaviour
{
    static public Warrior_Spawner instance;

    [Header("Warrior Settings")]
    public GameObject[] warriorPrefabs;
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
        InvokeRepeating("SpawnWarrior", 2f, 5f);
    }

    public void SpawnWarrior()
    {
        int i = Random.Range(0, warriorPrefabs.Length);
        Instantiate(warriorPrefabs[i], spawnPoints.position, Quaternion.identity);
    }
}
