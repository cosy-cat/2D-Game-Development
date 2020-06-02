using System.Collections;
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
                Player player = other.GetComponent<Player>();
                if (player != null)
                    player.Damage();
                Destroy(this.gameObject);
                break;
            case "Laser":
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }

}
