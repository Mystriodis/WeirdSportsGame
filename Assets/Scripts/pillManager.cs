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
    [SerializeField] cursor cursorScript;
    [SerializeField] pillMenu menuScript;


    [HideInInspector] public string state;
    [HideInInspector] public int pillAmount;
    [HideInInspector] public int extraCaughtValue;
    private float currentCaughtPercentage;
    private Syringe activeMinigame;
    private float bugCounter; //last ditch effort save to fix useSyringe() being called in the same frame as when syringe is built 

    // Start is called before the first frame update
    void Start()
    {
        var gamepads = Gamepad.all;
        if (playerSide == 1)
        {
            if (gamepads.Count >= 2)
            {
                GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player 1", Keyboard.current, gamepads[0]);
            } else
            {
                GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player 1", Keyboard.current);
            }
           
        } else if (playerSide == 2)
        {
            

            if (gamepads.Count >= 2)
            {
                GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player 2", Keyboard.current, gamepads[1]);
            } else
            {
                GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player 2", Keyboard.current);
            }
        }
        
        passVariables();
        switchState("selection");
        updateCaught(); 
    }

    private void assignInput()
    {

    }

    private void Update()
    {
        bugCounter += Time.deltaTime;
    }
    private void passVariables()
    {
        moveScript.gridSize = gridSize;
        checkScript.gridSize = gridSize;
        checkScript.playerSide = playerSide;
        checkScript.pillParent = pillParent;
        cursorScript.playerSide = playerSide;
        menuScript.playerSide = playerSide;
        
        
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

        SpriteRenderer pillRenderer = newPill.AddComponent<SpriteRenderer>(); //for the cursor to get the center of bounds
        moveScript.updateCursorPosition();
        
        moveScript.newPill();
        switchState("move");

        bugCounter = 0;
    }

    public void switchToSelection()
    {
        switchState("selection");
        menuScript.updateDisplay();

        bugCounter = 0;
    }

    public void switchToSyringe()
    {
        GameObject minigameObject = Instantiate(syringeMinigame);
        minigameObject.transform.position = gridBackground.transform.position;
        minigameObject.transform.localScale = gridBackground.transform.localScale;
        minigameManager minigameScript = minigameObject.GetComponent<minigameManager>();
        minigameScript.manager = this;
        minigameScript.playerSide = playerSide;
        activeMinigame = minigameObject.transform.Find("syringe").GetComponent<Syringe>();
        switchState("syringe");

        bugCounter = 0;

    }

    private void updateUI()
    {
        //caught counter
        caughtCounter.fillAmount = currentCaughtPercentage;
    }

    public void updateCaught()
    {
        int maxSafeAmount = Mathf.CeilToInt((gridSize.x * 2 + 1) * (gridSize.y * 2 + 1)*maxCaughtPercentage);
        currentCaughtPercentage = (pillAmount + extraCaughtValue) / (float)maxSafeAmount;


        updateUI();

        if (currentCaughtPercentage >= 1)
        {
            Actions.playerCaught(playerSide);
        }
    }

    public void useSyringe(InputAction.CallbackContext context)
    {
        if (state != "syringe") return;
        if (bugCounter == 0) return;

        if (context.started)
        {
            activeMinigame.Inject(); 
        }
    }
}
