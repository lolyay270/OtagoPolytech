/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 18th April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The EnemySpawner class controls:
/// - Number of waves, and what's in them
/// - Spawning of each wave
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    #region variables/class as variable/accessor
    [System.Serializable] public class Wave
    {
        public Enemy enemyPrefab;
        public float spawnInterval = 2;
        public int maxEnemies = 20;
    }

    [SerializeField] private List<Transform> movePath;
    public List<Transform> MovePath { get { return movePath; } } //needed by EnemyShooter
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float waveDelay;
    [SerializeField] private List<Wave> waves;
    private int currentWaveIndex;
    public int CurrentWaveIndex { get { return currentWaveIndex; } } //needed by UI
    public static EnemySpawner Instance;
    #endregion

    #region event setup
    [HideInInspector] public UnityEvent OnNewWave = new();
    #endregion

    #region methods
    //utility method for setting up static instance
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    //method for spawning waves
    private IEnumerator Start()
    {
        for (currentWaveIndex = 0; currentWaveIndex < waves.Count; currentWaveIndex++)
        {
            OnNewWave?.Invoke();
            StartCoroutine(SpawnEnemies(waves[currentWaveIndex]));
            yield return new WaitForSeconds(waveDelay);
        }
    }

    //method for spawning enemies in each wave
    private IEnumerator SpawnEnemies(Wave wave)
    {
        for (int i = 0; i < wave.maxEnemies; i++)
        {
            Enemy enemy = Instantiate(wave.enemyPrefab, spawnLocation.position, spawnLocation.rotation);
            enemy.movePath = movePath;
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }
    #endregion
}