using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pillSelection : MonoBehaviour
{

    //handles input for pillMenu

    public string state;
    [SerializeField] UnityEvent scrollUp, scrollDown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != "selection") return;

        scroll();
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
            
        }
    }
}
