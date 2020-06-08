using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Vector3 _direction = Vector3.down;
    private Player _player = null;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    
        if (transform.position.y < SpawnObjConst.yMin)
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
                Destroy(this.gameObject);
                break;
            case "Laser":
                Destroy(other.gameObject);
                if (_player != null)
                    _player.AddScore(10);
                Destroy(this.gameObject);
                break;
        }
    }

}
