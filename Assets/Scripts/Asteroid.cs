using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 _scale;
    private float _speed;
    private Vector3 _direction = Vector3.down;
    private float _rotationSpeed;
    private Vector3 _rotationAxis = Vector3.forward;
    [SerializeField] GameObject _explosionPrefab;
    private Player _player = null;
    
    void Start()
    {
        _scale = Vector3.one * UnityEngine.Random.Range(.2f, 1f);
        transform.localScale = _scale;
        _speed = UnityEngine.Random.Range(1f, 3f);
        _rotationSpeed = UnityEngine.Random.Range(-40f, 40f);
        if (_explosionPrefab == null)
        {
            Debug.LogError("Explostion Prefab has not be assigned via Unity Editor");
        }
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        transform.Rotate(_rotationAxis * _rotationSpeed * Time.deltaTime, Space.Self);
        if (transform.position.y < SpawnObjConst.yMin)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Laser":
                Destroy(other.gameObject);
                DestroyAsteroid();
                break;
            case "Player":
                if (_player != null)
                    _player.Damage();
                DestroyAsteroid();
                break;
        }
    }

    private void DestroyAsteroid()
    {
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = _scale;
        Destroy(this.gameObject, 0.2f);
    }
}
