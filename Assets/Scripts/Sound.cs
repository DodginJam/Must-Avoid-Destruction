using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour, ISoundPlayer
{
    [field: SerializeField]
    public AudioSource AudioSource
    {  get; set; }

    [field: SerializeField]
    public AudioClip Success
    { get; private set; }

    [field: SerializeField]
    public AudioClip Failure
    { get; private set; }

    [field: SerializeField]
    public AudioClip GameOver
    { get; private set; }

    [field: SerializeField]
    public AudioClip GameWin
    { get; private set; }

    public bool StartedPlayingNewSound
    { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
