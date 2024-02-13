using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillMenu : MonoBehaviour
{
    //turned public to reference which pill is selected
    public GameObject[] pills;
    public int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateDisplay();
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

    private void updateDisplay()
    {
        //highlight selected pill
        for (int i = 0; i < pills.Length; i++)
        {
            if (currentIndex == i)
            {
                pills[i].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            } else
            {
                pills[i].transform.localScale = Vector3.one;
            }
        }
    }

    public GameObject confirm()
    {
        return pills[currentIndex]; //PLACEHOLDER RETURN, should return chosen prefab object
    }
}
