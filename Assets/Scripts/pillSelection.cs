using System;
using UnityEngine;
using UnityEngine.Events;

public class pillSelection : MonoBehaviour
{

    //handles input for pillMenu

    public string state;
    [SerializeField] UnityEvent scrollUp, scrollDown;
    [SerializeField] pillMenu pillMenu;
    [SerializeField] RandomPills pillList;
    [SerializeField] pillMenu whichPill;


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
            GameObject pillObject = pillMenu.confirm(); //CHANGE

            //change sorting layer
            for (int i = 0; i < pillObject.transform.childCount; i++)
            {
                pillObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 1;
            }

            //switches pill when selected - sends info on which pill is selected
            pillList.newPill(whichPill.currentIndex);

            //switch state to "move"
            GetComponent<pillManager>().switchToMove(pillObject);
        }
    }
}
