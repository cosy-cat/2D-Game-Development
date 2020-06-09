using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed;
    private float _rotationSpeed;
    private Vector3 _rotationAxis = Vector3.forward;
    [SerializeField] GameObject _explosionPrefab;
    
    void Start()
    {
        _rotationSpeed = UnityEngine.Random.Range(-40f, 40f);
        if (_explosionPrefab == null)
        {
            Debug.LogError("Explostion Prefab has not be assigned via Unity Editor");
        }
    }

    void Update()
    {
        transform.Rotate(_rotationAxis * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.2f);
        }
    }
}
