using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private GameObject _enemyContainer = null;
    private Player _player = null;
    private IEnumerator coroutine;
    
    void Start()
    {
        if (_enemyPrefab == null)
        {
            throw new System.Exception("Please assign an enemy prefab to the corresponding field in Unity Editor");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player != null)
        {
            _player.OnDeath += StopSpawningEnemies;
        }
        else
        {
            throw new System.Exception("Unable to Find Player Component of Player GameObject");
        }

        coroutine = SpawnEnemies();

        StartCoroutine(coroutine);
    }

    private void StopSpawningEnemies(object sender, EventArgs e)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);    
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (_enemyPrefab != null && _enemyContainer != null)
            {
                Vector3 spawnLocation = new Vector3(UnityEngine.Random.Range(EnemyConst.xMin, EnemyConst.xMax), EnemyConst.ySpawn, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(5f);
            }
        }
    }

}
