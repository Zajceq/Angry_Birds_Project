using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Pause,
    Exit
}
public class BallComponent : MonoBehaviour
{
    GameState State = GameState.Start;

    private void Start() 
    {
        Debug.Log("State: " + State);
        // int StateVal = (int)State;
        // ++StateVal;
        // State = (GameState)StateVal;
        State += 1;
        Debug.Log("New State: " + State);
    }
}
