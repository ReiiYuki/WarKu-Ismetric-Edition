using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    #region state
    public enum State
    {
        NO_SELECTION = 0,
        UNIT_CREATION_SELECT,
        UNIT_MOVE_LISTEN,
        BUILD_LISTEN
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
    public bool IsListen()
    {
        return state == State.UNIT_MOVE_LISTEN;
    }
    public bool IsBuild()
    {
        return state == State.BUILD_LISTEN;
    }
    #endregion

    #region attribute
    State state;
    int selectUnitIndex;
    GameObject willMoveUnit,currentTile;
    #endregion

    void Start()
    {
        SetState(State.NO_SELECTION);
    }

    #region unit creation
    public void SelectUnit(int type)
    {
        selectUnitIndex = type;
        SetState(State.UNIT_CREATION_SELECT);
    }

    public int GetUnitCreationType()
    {
        return selectUnitIndex;
    }

    #endregion
    #region unitbuild
    public void ReadyToMove(GameObject willMoveUnit)
    {
        this.willMoveUnit = willMoveUnit;
        SetState(State.UNIT_MOVE_LISTEN);
    }

    public GameObject GetWillMoveUnit()
    {
        return willMoveUnit;
    }
    #endregion
    #region build
    public void ReadyToBuild(GameObject currentTile)
    {
        this.currentTile = currentTile;
        SetState(State.BUILD_LISTEN);
    }
    public GameObject GetCurrentTile()
    {
        return currentTile;
    }
    #endregion
    public void ResetState()
    {
        SetState(State.NO_SELECTION);
    }

    public void SetCurrentTile(GameObject currentTile)
    {
        this.currentTile = currentTile;
    }
}
