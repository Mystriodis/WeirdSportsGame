using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideManagerV2 : MonoBehaviour
{
    //acts as the game manager for each player
    //does so by managing and storing the "state" variable 
    //possible states: selection, move, syringe

    //these scripts mainly just pass through input to other scripts
    [SerializeField] private pillSelectionV2 selectionScript;   //just changed to reference new script
    [SerializeField] private pillMove moveScript;

    public string state = "";

    // Start is called before the first frame update
    void Start()
    {
        switchState("selection");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void switchState(string newState)
    {
        state = newState;
        selectionScript.state = newState;
        moveScript.state = newState;
    }
}
