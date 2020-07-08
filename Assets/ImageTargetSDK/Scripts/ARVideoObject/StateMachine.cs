using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected AbstractVideoState currentVideoState;

    public void SetState(AbstractVideoState state)
    {
        currentVideoState = state;
        StartCoroutine(currentVideoState.Start());
    }

    

}
