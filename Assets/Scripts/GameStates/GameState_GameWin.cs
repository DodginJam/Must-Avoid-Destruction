using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState_GameWin : GameState_Base
{
    [field: SerializeField]
    public GameObject VictorUI
    {  get; private set; }

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
        VictorUI.SetActive(true);
        CanvasGroupUI.alpha = 0;
        Counter = 0;

        ((ISoundPlayer)SoundPlayer).PlayAudioTrack(SoundPlayer.GameWin, true, 0, true);
        SoundPlayer.AudioSource.spatialBlend = 0;
        SoundPlayer.AudioSource.volume = 0.20f;
    }

    public override void UpdateState(GameManager gameManager)
    {
        if (CanvasGroupUI.alpha != 1)
        {
            CanvasGroupUI.alpha += Time.deltaTime * Counter;
        }
    }

    public override void ExitState(GameManager gameManager)
    {

    }
}
