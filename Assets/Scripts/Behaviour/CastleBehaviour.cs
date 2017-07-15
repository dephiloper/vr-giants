using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Filters;
using UnityEngine;

public class CastleBehaviour : MonoBehaviour
{
    public GameObject CameraRig;

    private void OnDestroy()
    {
        if (!CameraRig)
            return;
        
        var gameScoreBehaviour = CameraRig.GetComponent<GameScoreBehaviour>();
        if (gameScoreBehaviour)
        {
            gameScoreBehaviour.CurrentGameState = GameState.Lost;
        }
    }
}