using System;
using UnityEngine;
using UnityEngine.Events;

public class pillSelection : MonoBehaviour
{

    //handles input for pillMenu

    public string state;
    [SerializeField] UnityEvent scrollUp, scrollDown;
    [SerializeField] pillMenu pillMenu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != "selection") return;

        scroll();
        confirm();

    }

    private void scroll()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            scrollUp.Invoke();
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            scrollDown.Invoke();
        }
    }

    private void confirm()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //get prefab
            GameObject pillObject = pillMenu.confirm();

            //switch state to "move"
            GetComponent<pillManager>().switchToMove(pillObject);
        }
    }
}
