using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class pillSelection : MonoBehaviour
{

    //handles input for pillMenu
    public string state;
    [SerializeField] UnityEvent scrollUp, scrollDown;
    [SerializeField] pillMenu pillMenu;
    [SerializeField] RandomPills pillList;
    [SerializeField] pillMenu whichPill;

    public void confirm(InputAction.CallbackContext context)
    {
        if (state != "selection") return;

        if (context.started)
        {
            //get prefab
            Pills pillScriptableObject = pillMenu.confirm();

            GameObject pillObject = pillScriptableObject.prefab;

            //game object setup: switch sorting layer and passing through prefab
            for (int i = 0; i < pillObject.transform.childCount; i++)
            {
                if (pillObject.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    pillObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
            }

            pillObject.GetComponent<pillStats>().stats = pillScriptableObject;

            //switches pill when selected - sends info on which pill is selected
            pillList.newPill(whichPill.currentIndex);

            //switch state to "move"
            StartCoroutine(switchToMove(pillObject));
        }

    }

    public void scroll(InputAction.CallbackContext context)
    {
        if (state != "selection") return;

        if (context.started)
        {
            Vector2 inputDirection = context.ReadValue<Vector2>();

            if (inputDirection.y > 0.1)
            {
                scrollUp.Invoke();
            }
            else if (inputDirection.y < -0.1)
            {
                scrollDown.Invoke();
            }
        }
    }

    IEnumerator switchToMove(GameObject pillObject)
    {
        //start on next frame so input for the pill placement and pill selection doesn't overlap
        yield return null;
        GetComponent<pillManager>().switchToMove(pillObject);
    }
}
