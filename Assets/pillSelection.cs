using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillSelection : MonoBehaviour
{
    public string state;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != "selection") return;
    }
}
