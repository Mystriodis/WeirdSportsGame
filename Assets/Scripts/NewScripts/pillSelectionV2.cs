using UnityEngine;
using UnityEngine.Events;

public class pillSelectionV2 : MonoBehaviour
{
    //handles input for pillMenu

    public string state;
    [SerializeField] UnityEvent scrollUp, scrollDown;

    [SerializeField] RandomPills pillList;

    [SerializeField] pillMenu whichPill;    //reference currentIndex


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
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            scrollDown.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //switches pill when selected - sends info on which pill is selected
            pillList.newPill(whichPill.currentIndex);
        }
    }

    /*private void confirm()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }*/
}
