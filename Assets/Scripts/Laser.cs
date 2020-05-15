using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Vector3 _direcion = Vector3.up;

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private float _yLimit = 7f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direcion * _speed * Time.deltaTime);

        if (transform.position.y > _yLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
