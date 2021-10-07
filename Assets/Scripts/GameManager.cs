using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        Prepare,
        MainGame,
        NextLevel,
        FinishGame,
        GameOver
    }
    [SerializeField] private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:
                    break;
                case GameState.NextLevel:
                    break;
                case GameState.FinishGame:
                    break;
                case GameState.GameOver:
                    break;
            }
            _currentGameState = value;
        }
    }
}
