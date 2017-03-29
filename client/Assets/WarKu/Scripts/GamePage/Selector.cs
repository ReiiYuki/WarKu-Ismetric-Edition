using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    #region state
    public enum State
    {
        NO_SELECTION = 0,
        UNIT_CREATION_SELECT
    }
    
    public void SetState(State state)
    {
        this.state = state;
    }

    public bool IsSelected()
    {
        return state != State.NO_SELECTION;
    }

    public bool IsCreation()
    {
        return state == State.UNIT_CREATION_SELECT;
    }
    #endregion

    #region attribute
    State state;
    #endregion

    void Start()
    {
        SetState(State.NO_SELECTION);
    }

}
