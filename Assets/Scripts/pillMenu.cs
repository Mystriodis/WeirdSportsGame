using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillMenu : MonoBehaviour
{
    //turned public to reference which pill is selected
    public GameObject[] pills;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public int playerSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scrollUp()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = pills.Length - 1;
        }
        
        updateDisplay();
    }

    public void scrollDown()
    {
        currentIndex++;

        if (currentIndex == pills.Length)
        {
            currentIndex = 0;
        }

        updateDisplay();
    }

    public void updateDisplay()
    {
        //highlight selected pill
        for (int i = 0; i < pills.Length; i++)
        {
            if (currentIndex == i)
            {
                Actions.moveCursor(playerSide, pills[i].transform.position.x, pills[i].transform.position.y, false);
                pills[i].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            } else
            {
                pills[i].transform.localScale = Vector3.one;
            }
        }
    }

    public Pills confirm()
    {
        return pills[currentIndex].GetComponent<pillDisplay>().currentPill;
    }
}
