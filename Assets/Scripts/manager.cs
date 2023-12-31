using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : Singleton_template<manager>
{
    public int maxZombie, maxGhost;
    [SerializeField] private Enemy ZombiePrefab;
    [SerializeField] private Enemy GhostPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public List<Enemy> enemies;
    private int currentZombie = 0;
    private int currentGhost = 0;

    private void Update()
    {
        if (currentGhost < maxGhost) { Spawner.Instance.SpawnEnemy(GhostPrefab, spawnPoints[(int)Random.Range(0f, spawnPoints.Length - 1)].position); currentGhost++; }
        if (currentZombie < maxZombie) { Spawner.Instance.SpawnEnemy(ZombiePrefab, spawnPoints[(int)Random.Range(0f, spawnPoints.Length - 1)].position); currentZombie++; }
    }
}
