using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState_DisplayProblem : GameState_Base
{
    [field: SerializeField]
    public Phone PhoneToPickUp
    { get; private set; }

    public enum DisplayStatus
    {
        Phone,
        DisplayScreen,
        ChangeState
    }

    public DisplayStatus DisplayState
    {  get; private set; }

    [field: SerializeField]
    public float TimeUntilRinging
    { get; private set; } = 5.0f;

    public float TimerCurrent
    { get; private set; }

    public override void EnterState(GameManager gameManager)
    {
        DisplayState = DisplayStatus.Phone;
        PhoneToPickUp.Status = Phone.PhoneStatus.Quiet;
        TimerCurrent = 0;
    }

    public override void UpdateState(GameManager gameManager)
    {
        if (DisplayState == DisplayStatus.Phone)
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
                    ((ISoundPlayer)PhoneToPickUp).PlayAudioTrack(PhoneToPickUp.PhonePickUp, false, 0.5f);
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
                    ((ISoundPlayer)PhoneToPickUp).PlayAudioTrack(PhoneToPickUp.PhoneTalking[randomIndex], false, 0.5f);
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
                DisplayState = DisplayStatus.DisplayScreen;
                PhoneToPickUp.Status = Phone.PhoneStatus.Quiet;
            }

        }
        else if (DisplayState == DisplayStatus.DisplayScreen)
        {
            Debug.LogWarning("Here there should be an animationn of the phone being picked up.");
        }
        else if(DisplayState == DisplayStatus.ChangeState)
        {
            gameManager.SwitchState(gameManager.AllGameStates.AwaitPlayerAnswer);
        }
    }

    public override void ExitState(GameManager gameManager)
    {
        TimerCurrent = 0;
    }
}
