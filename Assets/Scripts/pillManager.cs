using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pillManager : MonoBehaviour
{
    //acts as the game manager for each player
    //does so by managing and storing the "state" variable 
    //possible states: selection, move, syringe

    //these scripts mainly just pass through input to other scripts
    [SerializeField] pillSelection selectionScript;   //just changed to reference new script
    [SerializeField] pillMove moveScript;
    [SerializeField] Transform gridCenter;
    [SerializeField] gridCheck checkScript;
    [SerializeField] Vector2 gridSize; //HALF SIZE OF GRID
    [SerializeField] Image caughtCounter;
    [SerializeField] float maxCaughtPercentage; //in decimal (0.65)


    public string state;
    public int pillAmount;
    private float currentCaughtPercentage;

    // Start is called before the first frame update
    void Start()
    {
        passVariables();
        switchState("selection");
        updateCaught();
    }

    private void passVariables()
    {
        moveScript.gridSize = gridSize;
        checkScript.gridSize = gridSize;
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

    private void updateUI()
    {
        //caught counter
        caughtCounter.fillAmount = currentCaughtPercentage;
    }

    public void updateCaught()
    {
        int maxSafeAmount = Mathf.CeilToInt((gridSize.x * 2 + 1) * (gridSize.y * 2 + 1)*maxCaughtPercentage);
        currentCaughtPercentage = pillAmount / (float)maxSafeAmount;

        updateUI();
    }
}
