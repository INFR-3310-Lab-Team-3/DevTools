using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SpawnEnemy(Enemy Prefab, Vector3 position)
    {
        objectPooler.SpawnFromPool("Zombie", transform.position, Quaternion.identity);
        objectPooler.SpawnFromPool("Ghost", transform.position, Quaternion.identity);
        //Enemy enemy = Instantiate(Prefab);
        //enemy.transform.position = position;
        //manager.Instance().enemies.Add(enemy);
    }
}
