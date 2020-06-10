using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioManager _audioManager;
    void Start()
    {
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Unable to find game object audio manager");
        }
        _audioManager.PlayExplosion();

        Destroy(this.gameObject, 2.8f);
    }
}
