using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float _speed = 3f;
    private Vector3 _direction = Vector3.down;

    private enum PowerupId
    {
        TrippleShot,
        Speed,
        Shield
    }

    [SerializeField] private PowerupId _powerupId;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.y < SpawnObjConst.yMin)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                switch (_powerupId)
                {
                    case PowerupId.TrippleShot:
                        player.TrippleShotActive();
                        // player.ActivatePowerUp(_powerupId);
                        break;
                    case PowerupId.Speed:
                        // player.SpeedActive();
                        break;
                    case PowerupId.Shield:
                        // player.Shield();
                        break;
                }

            Destroy(this.gameObject);
        }
    }
}
