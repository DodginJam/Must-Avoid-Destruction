using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState_GameOver : GameState_Base
{
    [field: SerializeField]
    public GameObject LoseUI
    { get; private set; }

    [field: SerializeField]
    public CanvasGroup CanvasGroupUI
    { get; private set; }

    public int Counter
    { get; private set; } = 0;

    [field: SerializeField]
    public Sound SoundPlayer
    { get; private set; }
    public override void EnterState(GameManager gameManager)
    {
        LoseUI.SetActive(true);
        CanvasGroupUI.alpha = 0;
        Counter = 0;

        ((ISoundPlayer)SoundPlayer).PlayAudioTrack(SoundPlayer.GameOver, true, 0, true);
        SoundPlayer.AudioSource.spatialBlend = 0;
        SoundPlayer.AudioSource.volume = 0.75f;
    }

    public override void UpdateState(GameManager gameManager)
    {
        if (CanvasGroupUI.alpha != 1)
        {
            CanvasGroupUI.alpha += Time.deltaTime;
        }
    }

    public override void ExitState(GameManager gameManager)
    {

    }
}
