using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Vector3 _direction = Vector3.down;
    private Player _player = null;
    private Animator _animator;
    private bool _isBeingDestroyed = false;
    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)    
        {
            Debug.LogError("Player is null");
        }   
        
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component not found");
        }

        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("Collider component not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    
        if (!_isBeingDestroyed && transform.position.y < SpawnObjConst.yMin)
        {
            transform.position = new Vector3(Random.Range(SpawnObjConst.xMin, SpawnObjConst.xMax), SpawnObjConst.ySpawn, 0);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                if (_player != null)
                    _player.Damage();
                DestroyEnemy();
                break;
            case "Laser":
                Destroy(other.gameObject, 2.8f);
                if (_player != null)
                    _player.AddScore(10);
                DestroyEnemy();
                break;
        }
    }

    private void DestroyEnemy()
    {
        _isBeingDestroyed = true;
        _collider.enabled = false;
        _animator.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject, 2.8f);
    }
}
