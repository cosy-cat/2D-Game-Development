using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float _speed = 3f;
    private Vector3 _direction = Vector3.down;
    public enum PowerupId   // To be set in inspector for each powerup prefab
    {
        TrippleShot,
        Speed,
        Shield
    }
    [SerializeField] private PowerupId _powerupId;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Unable to find game object audio manager");
        }
    }

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
            {
                // player.PowerupActive(_powerupId);
                switch (_powerupId)
                {
                    case PowerupId.TrippleShot:
                        player.TrippleShotActive();
                        break;
                    case PowerupId.Speed:
                        player.BoostSpeedActive();
                        break;
                    case PowerupId.Shield:
                        player.ShieldActive();
                        break;
                }
            }
            _audioManager.Play(AudioManager.ObjectAudio.PowerUp);
            Destroy(this.gameObject);
        }
    }
}
