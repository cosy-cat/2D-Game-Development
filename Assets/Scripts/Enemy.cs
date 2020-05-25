﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Vector3 _direction = Vector3.down;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    
        if (transform.position.y < EnemyConst.yMin)
        {
            transform.position = new Vector3(Random.Range(EnemyConst.xMin, EnemyConst.xMax), EnemyConst.ySpawn, 0);
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                Player player = other.GetComponent<Player>();
                if (player != null)
                    player.Damage();
                break;
            case "Laser":
                Destroy(other.gameObject);
                break;
        }
        Destroy(this.gameObject);
    }

}