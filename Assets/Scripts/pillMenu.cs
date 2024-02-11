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
        if (currentIndex - 1 < 0) return;
        currentIndex--;

        updateDisplay();
    }

    public void scrollDown()
    {
        if (currentIndex + 1 == pills.Length) return;
        currentIndex++;

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
}
