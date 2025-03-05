using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] AlertLevels
    { get; private set; } = new int[] { 1, 2, 3, 4, 5 };

    public int CurrentAlertLevel
    { get; private set; }

    public Crises_SO CurrentCrisis
    { get; private set; }

    [field: SerializeField]
    public Crises_SO[] AllCrisis
    { get; private set; }

    public AvailableGameStates AllGameStates
    { get; private set; } = new AvailableGameStates();

    public GameState_Base CurrentGameState
    { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = AllGameStates.StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentGameState.UpdateState(this);
    }

    public class AvailableGameStates
    {
        public GameState_StartGame StartGame { get; private set; } = new GameState_StartGame();
        public GameState_GameOver GameOver { get; private set; } = new GameState_GameOver();
        public GameState_GameWin GameWin { get; private set; } = new GameState_GameWin();
        public GameState_DisplayOutcome DisplayOutcome { get; private set; } = new GameState_DisplayOutcome();
        public GameState_ProcessAnswer ProcessAnswer { get; private set; } = new GameState_ProcessAnswer();
        public GameState_AwaitPlayerAnswer AwaitPlayerAnswer { get; private set; } = new GameState_AwaitPlayerAnswer();
        public GameState_DisplayProblem DisplayProblem { get; private set; } = new GameState_DisplayProblem();
    }
}
