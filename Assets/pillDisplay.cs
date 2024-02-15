using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class pillDisplay : MonoBehaviour
{
    public Pills currentPill;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = currentPill.image;
    }
}
