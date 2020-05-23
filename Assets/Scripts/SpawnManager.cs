using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        if (_enemyPrefab == null)
        {
            throw new System.Exception("Please assign an enemy prefab to the corresponding field in Unity Editor");
        }

        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(EnemyConst.xMin, EnemyConst.xMax), EnemyConst.ySpawn, 0);
            Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(5f);    
        }
    }
}
