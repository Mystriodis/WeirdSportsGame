using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectionCheck : MonoBehaviour
{
    public string currentObject = "";
    public bool connected = false;
    public GameObject connectedObject;

    // Start is called before the first frame update
    void Start()
    {
        print(currentObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void check()
    {
        switch (currentObject)
        {
            case "phone0": //phone top
                checkPosition("phone1");
            break;

            case "phone1": //phone bottom
                checkPosition("phone0");
            break;
                
        }
    }

    private void checkPosition(string target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);


        if (!hit) return;
        if (hit.collider.gameObject.GetComponent<connectionCheck>() == null) return;

        connectionCheck neighborSegment = hit.collider.gameObject.GetComponent<connectionCheck>();
        if (neighborSegment.currentObject == target) 
        {
            neighborSegment.connected = true;
            connected = true;
            connectedObject = neighborSegment.transform.parent.gameObject;
            neighborSegment.connectedObject = transform.parent.gameObject;
        }

        

    }
}
