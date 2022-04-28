using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    private AudioMixer audioMix;    

    private AudioSource source;

   void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        audioMix = source.outputAudioMixerGroup.audioMixer;
    }

    //changes volume when slider value changes
    public void volumeChange(float newValue)
    {
        
        audioMix.SetFloat("audioVolume", newValue);
    }

    private void Update()
    {
        
    }


}
