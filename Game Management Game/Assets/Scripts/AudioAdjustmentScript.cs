using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAdjustmentScript : MonoBehaviour
{
    AudioSource _audioSource;
    public enum Type
    {
        BackgroundAudio,
        SoundEffect
    }
     
    public Type _type;
    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        switch( _type )
        {
            case Type.BackgroundAudio:
                _audioSource.volume = GameManager.bkgVolume;
                break;
            case Type.SoundEffect:
                _audioSource.volume = GameManager.sfxVolume;
                break;
        }
    }
    private void Update()
    {
        if(_type == Type.BackgroundAudio)
            _audioSource.volume = GameManager.bkgVolume;

    }
    // Update is called once per frame
    void OnEnable()
    {
        switch (_type)
        {
            case Type.BackgroundAudio:
                _audioSource.volume = GameManager.bkgVolume;
                break;
            case Type.SoundEffect:
                _audioSource.volume = GameManager.sfxVolume;
                break;
        }
    }
}
