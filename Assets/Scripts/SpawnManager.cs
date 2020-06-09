using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private GameObject _enemyContainer = null;
    [SerializeField] private GameObject[] _powerupPrefabs = null;
    [SerializeField] private GameObject _powerupContainer = null;
    [SerializeField] private GameObject _asteroidPrefab = null;
    [SerializeField] private float _spawnMinDelay = 4f;
    [SerializeField] private float _spawnMaxDelay = 8f;

    private bool _stopSpawning = false;
    
    void Start()
    {
        if (_enemyPrefab == null || _enemyContainer == null || _powerupPrefabs.Length == 0 || _powerupContainer == null || _asteroidPrefab == null)
        {
            throw new System.Exception("Please assign prefabs and corresponding container into the corresponding field in Unity Editor");
        }

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpwanPowerups());
        StartCoroutine(SpwanAsteroids());
    }
    // Update is called once per frame
    // void Update()
    // {

    // }

    public IEnumerator SpawnEnemies()
    {
        while (!_stopSpawning)
        {
            if (_enemyPrefab != null && _enemyContainer != null)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, GetSpawnObjectLocation(), Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(5f);
            }
        }
    }

    public IEnumerator SpwanPowerups()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 5f));
        while (!_stopSpawning)
        {
            if (_powerupPrefabs.Length > 0 && _powerupContainer != null)       
            {
                int randomPowerup = UnityEngine.Random.Range(0, _powerupPrefabs.Length);
                GameObject newPowerup = Instantiate(_powerupPrefabs[randomPowerup], GetSpawnObjectLocation(), Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
                yield return new WaitForSeconds(UnityEngine.Random.Range(_spawnMinDelay, _spawnMaxDelay));
            }
        }
    }

    public IEnumerator SpwanAsteroids()
    {
        while(!_stopSpawning)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_spawnMinDelay, _spawnMaxDelay));
            for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
            {
                Instantiate(_asteroidPrefab, GetSpawnObjectLocation(), Quaternion.identity);
                yield return new WaitForSeconds(UnityEngine.Random.Range(.2f, 1f));
            }
        }
    }

    private Vector3 GetSpawnObjectLocation()
    {
        return new Vector3(UnityEngine.Random.Range(SpawnObjConst.xMin, SpawnObjConst.xMax), SpawnObjConst.ySpawn, 0);
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
