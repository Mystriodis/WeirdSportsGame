using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class pillManager : MonoBehaviour
{
    //acts as the game manager for each player
    //does so by managing and storing the "state" variable 
    //possible states: selection, move, syringe

    //these scripts mainly just pass through input to other scripts
    public int playerSide; //1 or 2
    
    [SerializeField] pillSelection selectionScript;   //just changed to reference new script
    [SerializeField] pillMove moveScript;
    [SerializeField] Transform gridCenter;
    [SerializeField] gridCheck checkScript;
    [SerializeField] Vector2 gridSize; //HALF SIZE OF GRID
    [SerializeField] Image caughtCounter;
    [SerializeField] float maxCaughtPercentage; //in decimal (0.65)
    [SerializeField] GameObject syringeMinigame;
    [SerializeField] GameObject gridBackground; //for setting minigame size
    [SerializeField] GameObject pillParent;


    public string state;
    public int pillAmount;
    private float currentCaughtPercentage;
    private Syringe activeMinigame;

    // Start is called before the first frame update
    void Start()
    {
        if (playerSide == 1)
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player 1", Keyboard.current);
        } else if (playerSide == 2)
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player 2", Keyboard.current);
        }
        
        passVariables();
        switchState("selection");
        updateCaught(); 
    }

    private void passVariables()
    {
        moveScript.gridSize = gridSize;
        checkScript.gridSize = gridSize;
        checkScript.playerSide = playerSide;
        checkScript.pillParent = pillParent;
        
    }

    public void switchState(string newState)
    {
        state = newState;
        selectionScript.state = newState;
        moveScript.state = newState;
    }

    public void switchToMove(GameObject pillPrefab)
    {
        GameObject newPill = Instantiate(pillPrefab, gridCenter.position, Quaternion.identity);
        newPill.transform.parent = pillParent.transform;
        moveScript.currentPill = newPill;
        
        moveScript.newPill();
        switchState("move");
    }

    public void switchToSelection()
    {
        switchState("selection");
    }

    public void switchToSyringe()
    {

        switchState("syringe");
        GameObject minigameObject = Instantiate(syringeMinigame);
        minigameObject.transform.position = gridBackground.transform.position;
        minigameObject.transform.localScale = gridBackground.transform.localScale;
        minigameManager minigameScript = minigameObject.GetComponent<minigameManager>();
        minigameScript.manager = this;
        minigameScript.playerSide = playerSide;
        activeMinigame = minigameObject.transform.Find("syringe").GetComponent<Syringe>();
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

    public void useSyringe(InputAction.CallbackContext context)
    {
        if (state != "syringe") return;

        if (context.started)
        {
            activeMinigame.Inject(); 
        }
    }
}
