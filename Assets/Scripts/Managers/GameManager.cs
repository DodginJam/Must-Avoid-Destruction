using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Crises_SO currentCrisis;

    public Crises_SO CurrentCrisis
    {
        get {return currentCrisis; }
        set 
        {
            currentCrisis = value;
            currentCrisis.Negotiator = AllNegotiators[NegotiatorsTracker];
            NegotiatorsTracker++;
        }
        
     }

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

    [field: SerializeField]
    public NegotiatorDisplay NegotiatorDisplay
    { get; private set; }

    [field: SerializeField, Range(1, 5)]
    public int StartingAlertLevel
    { get; private set; } = 2;

    [field: SerializeField]
    public Negotiator_SO[] AllNegotiators
    { get; private set; }

    public int NegotiatorsTracker
    { get; private set; } = 0;


    // Start is called before the first frame update
    void Start()
    {
        Shuffle(AllNegotiators);

        InitialiseCrisis();

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
        public GameState_AwaitPlayerAnswer AwaitPlayerAnswer { get; private set; }
        [field: SerializeField]
        public GameState_DisplayProblem DisplayProblem { get; private set; }
    }

    /// <summary>
    /// Initialise the first crisis on game load, performing null check.
    /// </summary>
    public void InitialiseCrisis()
    {
        if (AllCrisis != null)
        {
            if (AllCrisis[0] != null)
            {
                CurrentCrisis = AllCrisis[0];
            }
            else
            {
                Debug.LogError("The first element of the crisis array is null, unable to continue.");
            }
        }
        else
        {
            Debug.LogError("No crisis have been assigned to the crises array. Unable to continue.");
        }
    }

    /// <summary>
    /// Set's the next crisis, and returns true if it is possible. Else, if no more crisis exist further into the array, return false.
    /// </summary>
    /// <returns></returns>
    public bool SetNextCrisis()
    {
        int currentCrisisIndex = Array.IndexOf(AllCrisis, CurrentCrisis);

        Debug.Log($"currentCrisisIndex: {currentCrisisIndex}");

        // If the current index is not the end point, move through array, else bring game to end.
        if (currentCrisisIndex < AllCrisis.Length - 1)
        {
            CurrentCrisis = AllCrisis[currentCrisisIndex + 1];
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Thanks to ChatGPT.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    public static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i);; // Get a random index from 0 to i
            (array[i], array[j]) = (array[j], array[i]); // Swap elements
        }
    }
}
