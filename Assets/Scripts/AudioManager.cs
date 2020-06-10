using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<AudioSource> _objectAudioSrc = new List<AudioSource>();
    public enum ObjectAudio
    {
        Explosion,
        PowerUp
    }
    void Start()
    {
        Transform objectAudio = transform.GetChild(1);
        if (objectAudio == null)
        {
            Debug.LogError("Unable to find ObjectAudio gameObject");
        }
        for (int i = 0; i < objectAudio.childCount; i++)
        {
            _objectAudioSrc.Add(objectAudio.GetChild(i).GetComponent<AudioSource>());
        }
    }


    public void Play(ObjectAudio objectAudio)
    {
        switch (objectAudio)
        {
            case ObjectAudio.Explosion:
                _objectAudioSrc[0].Play();
                break;
            case ObjectAudio.PowerUp:
                _objectAudioSrc[1].Play();
                break;
        }
    }
}
