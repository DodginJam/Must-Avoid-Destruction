using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameState_DisplayProblem;

[Serializable]
public class GameState_DisplayOutcome : GameState_Base
{
    [field: SerializeField]
    public Phone PhoneToPickUp
    { get; private set; }

    [field: SerializeField]
    public float TimeUntilRinging
    { get; private set; } = 5.0f;
    public float TimerCurrent
    { get; private set; }

    public int OldAlertValue
    { get; private set; }

    public int NewAlertValue
    { get; private set; }
    public enum OutcomeProcess
    {
        Phone,
        AlertChange,
        Reaction,
        ChangeState
    }

    public OutcomeProcess ProcessState
    { get; set; }

    [field: SerializeField]
    public Sound SoundPlayer
    { get; set; }

    public override void EnterState(GameManager gameManager)
    {
        ProcessState = OutcomeProcess.Phone;
        PhoneToPickUp.Status = Phone.PhoneStatus.Quiet;
        TimerCurrent = 0;
        OldAlertValue = 0;
        NewAlertValue = 0;
        SoundPlayer.StartedPlayingNewSound = false;
    }

    public override void UpdateState(GameManager gameManager)
    {
        if (ProcessState == OutcomeProcess.Phone)
        {
            if (PhoneToPickUp.Status == Phone.PhoneStatus.Quiet)
            {
                TimerCurrent += Time.deltaTime;

                if (TimerCurrent >= TimeUntilRinging)
                {
                    ((ISoundPlayer)PhoneToPickUp).PlayAudioTrack(PhoneToPickUp.PhoneRinging, true, 0);
                    PhoneToPickUp.IsInteractionEnabled = true;
                    PhoneToPickUp.Status = Phone.PhoneStatus.Ringing;
                }
            }
            else if (PhoneToPickUp.Status == Phone.PhoneStatus.Ringing)
            {
                // Wait for the phone to be picked up by player.
            }
            else if (PhoneToPickUp.Status == Phone.PhoneStatus.PickedUp)
            {
                if (PhoneToPickUp.StartedPlayingNewSound == false)
                {
                    ((ISoundPlayer)PhoneToPickUp).PlayAudioTrack(PhoneToPickUp.PhonePickUp, false, 1f);
                    PhoneToPickUp.StartedPlayingNewSound = true;
                }

                if (!PhoneToPickUp.AudioSource.isPlaying)
                {
                    PhoneToPickUp.Status = Phone.PhoneStatus.Talking;
                    PhoneToPickUp.StartedPlayingNewSound = false;
                }
            }
            else if (PhoneToPickUp.Status == Phone.PhoneStatus.Talking)
            {
                if (PhoneToPickUp.StartedPlayingNewSound == false)
                {
                    int randomIndex = UnityEngine.Random.Range(0, PhoneToPickUp.PhoneTalking.Length);
                    ((ISoundPlayer)PhoneToPickUp).PlayAudioTrack(PhoneToPickUp.PhoneTalking[randomIndex], false, 1f);
                    PhoneToPickUp.StartedPlayingNewSound = true;
                }

                if (!PhoneToPickUp.AudioSource.isPlaying)
                {
                    PhoneToPickUp.Status = Phone.PhoneStatus.Putdown;
                    PhoneToPickUp.StartedPlayingNewSound = false;
                }
            }
            else if (PhoneToPickUp.Status == Phone.PhoneStatus.Putdown)
            {
                PhoneToPickUp.AudioSource.PlayOneShot(PhoneToPickUp.PhonePutDown);
                ProcessState = OutcomeProcess.AlertChange;
                PhoneToPickUp.Status = Phone.PhoneStatus.Quiet;
            }
        }
        else if (ProcessState == OutcomeProcess.AlertChange)
        {
            OldAlertValue = gameManager.AlertLevels.CurrentAlertLevel;

            // The alert level is increased by one one failure to resolve, plus the value associated with the negotiators militant tendencies.
            if (!gameManager.CurrentCrisis.HasBeenResolved)
            {
                gameManager.ChangeAlertState(1 + (int)gameManager.CurrentCrisis.Negotiator.MilitantTendency);
            }

            NewAlertValue = gameManager.AlertLevels.CurrentAlertLevel;

            ProcessState = OutcomeProcess.Reaction;

        }
        else if (ProcessState == OutcomeProcess.Reaction)
        {
            if (SoundPlayer.StartedPlayingNewSound == false)
            {
                if (NewAlertValue > OldAlertValue)
                {
                    ((ISoundPlayer)SoundPlayer).PlayAudioTrack(SoundPlayer.Failure, false);
                }
                else
                {
                    ((ISoundPlayer)SoundPlayer).PlayAudioTrack(SoundPlayer.Success, false);
                }
                SoundPlayer.StartedPlayingNewSound = true;
            }


            if (!SoundPlayer.AudioSource.isPlaying)
            {
                ProcessState = OutcomeProcess.ChangeState;
                PhoneToPickUp.StartedPlayingNewSound = false;
            }
        }
        else if (ProcessState == OutcomeProcess.ChangeState)
        {
            if (gameManager.AlertLevels.CurrentAlertLevel >= 5)
            {
                gameManager.SwitchState(gameManager.AllGameStates.GameOver);
            }
            else
            {
                if (gameManager.SetNextCrisis())
                {
                    gameManager.SwitchState(gameManager.AllGameStates.DisplayProblem);
                }
                else
                {
                    gameManager.SwitchState(gameManager.AllGameStates.GameWin);
                }
            }

            Debug.Log($"NextState: {gameManager.CurrentGameState.ToString()}");
        }
    }

    public override void ExitState(GameManager gameManager)
    {
        ProcessState = OutcomeProcess.Phone;
        PhoneToPickUp.Status = Phone.PhoneStatus.Quiet;
        TimerCurrent = 0;
        OldAlertValue = 0;
        NewAlertValue = 0;
        SoundPlayer.StartedPlayingNewSound = false;
    }
}
