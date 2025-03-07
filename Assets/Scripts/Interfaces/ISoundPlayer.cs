using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public interface ISoundPlayer
{
    public AudioSource AudioSource
    { get; set; }

    public bool StartedPlayingNewSound
    { get; set; }

    public void PlayAudioTrack(AudioClip clip, bool allowLooping, float delay = 0, bool manuallyStopPrevious = false, bool isOneShot = false)
    {
        if (manuallyStopPrevious)
        {
            AudioSource.Stop();
        }

        if (!isOneShot)
        {
            AudioSource.clip  = clip;
            AudioSource.loop = allowLooping;
            AudioSource.PlayDelayed(delay);
        }
        else
        {
            AudioSource.PlayOneShot(clip);
        }
    }
}
