using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillMove : MonoBehaviour
{
    //controlls pill movement
    public GameObject currentPill;
    public string state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != "move") return;
        movePill();
    }

    private void movePill()
    {
        currentPill.transform.position = inputDirection() + (Vector2)currentPill.transform.position;
        currentPill.transform.eulerAngles = new Vector3(0, 0, currentPill.transform.eulerAngles.z + (inputRotation() * 90));
    }

    private Vector2 inputDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;

        } else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;

        } else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;

        } else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }

        return direction;
    }

    private int inputRotation()
    {
        int rotateDirection = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotateDirection = 1;

        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateDirection = -1;
        }

        return rotateDirection;
    }
}
