using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Crises_SO CurrentCrisis
    { get; private set; }

    [field: SerializeField]
    public Crises_SO[] AllCrisis
    { get; private set; }

    [field: SerializeField]
    public AlertLevels AlertLevels 
    { get; private set; }

    [field: SerializeField]
    public AlertLevelDisplay AlertDisplay
    {  get; private set; }

    [field: SerializeField]
    public AvailableGameStates AllGameStates
    { get; private set; }

    public GameState_Base CurrentGameState
    { get; private set; }

    [field: SerializeField]
    public DisplayOptions ScreenDisplays
    { get; private set; }

    [field: SerializeField, Range(1, 5)]
    public int StartingAlertLevel
    { get; private set; } = 2;


    // Start is called before the first frame update
    void Start()
    {
        SwitchState(AllGameStates.StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGameState != null)
        {
            CurrentGameState.UpdateState(this);
        }
        else
        {
            Debug.LogWarning("CurrentGameState is null - only acceptable in game start.");
        }
    }

    public void SwitchState(GameState_Base newState)
    {
        if (CurrentGameState == null)
        {
            Debug.LogWarning("CurrentGameState is null - only acceptable in game start.");
        }
        else
        {
            CurrentGameState.ExitState(this);
        }

        CurrentGameState = newState;
        CurrentGameState.EnterState(this);
    }

    public void SetAlertStatus(int newAlertLevel)
    {
        AlertLevels.SetAlertLevel(newAlertLevel);
        AlertLevels.SetAlertLevelColour(newAlertLevel);
        AlertDisplay.DisplayText(AlertLevels);
        AlertDisplay.ChangeDisplayEmissionColour(AlertLevels.CurrentColorDisplay);
    }

    public void ChangeAlertState(int incrementAmount)
    {
        AlertLevels.ModifyAlertLevel(incrementAmount);
        AlertLevels.SetAlertLevelColour(AlertLevels.CurrentAlertLevel);
        AlertDisplay.DisplayText(AlertLevels);
        AlertDisplay.ChangeDisplayEmissionColour(AlertLevels.CurrentColorDisplay);
    }

    [Serializable]
    public class AvailableGameStates
    {
        [field: SerializeField]
        public GameState_StartGame StartGame { get; private set; }
        [field: SerializeField]
        public GameState_GameOver GameOver { get; private set; }
        [field: SerializeField]
        public GameState_GameWin GameWin { get; private set; }
        [field: SerializeField]
        public GameState_DisplayOutcome DisplayOutcome { get; private set; }
        [field: SerializeField]
        public GameState_ProcessAnswer ProcessAnswer { get; private set; }
        [field: SerializeField]
        public GameState_AwaitPlayerAnswer AwaitPlayerAnswer { get; private set; }
        [field: SerializeField]
        public GameState_DisplayProblem DisplayProblem { get; private set; }
    }
}
