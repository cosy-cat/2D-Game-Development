using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSourceExplosion;
    
    void Start()
    {
        _audioSourceExplosion = this.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<AudioSource>();
        if (_audioSourceExplosion == null)
        {
            Debug.LogError("Unable to find explosion audio source in child objects.");
        }
    }


    public void PlayExplosion()
    {
        _audioSourceExplosion.Play();
    }
    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
