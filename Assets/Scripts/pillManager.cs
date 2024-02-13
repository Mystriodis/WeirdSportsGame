using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillManager : MonoBehaviour
{
    //acts as the game manager for each player
    //does so by managing and storing the "state" variable 
    //possible states: selection, move, syringe

    //these scripts mainly just pass through input to other scripts
    [SerializeField] private pillSelection selectionScript;   //just changed to reference new script
    [SerializeField] private pillMove moveScript;
    [SerializeField] private Transform gridCenter;
    [SerializeField] private Vector2 gridSize; //HALF SIZE OF GRID

    public string state;

    // Start is called before the first frame update
    void Start()
    {
        passVariables();
        switchState("selection");
    }

    private void passVariables()
    {
        moveScript.gridSize = gridSize;

    }

    public void switchState(string newState)
    {
        state = newState;
        selectionScript.state = newState;
        moveScript.state = newState;
    }

    public void switchToMove(GameObject pillPrefab)
    {
        moveScript.currentPill = Instantiate(pillPrefab, gridCenter.position, Quaternion.identity);
        moveScript.newPill();
        switchState("move");
    }

    public void switchToSelection()
    {
        switchState("selection");
    }
}
