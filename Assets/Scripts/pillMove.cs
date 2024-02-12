using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillMove : MonoBehaviour
{
    //controls pill movement
    //
    //To Add: continuous movement on button hold

    public GameObject currentPill;
    public string state;
    private Vector2 relativePosition;
    public Vector2 gridSize;

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
        //Gets direction from input, also checks using isOutOfBounds to see whether or not the cursor can be moved upwards
        //!!! only accounts for the cursor position, this method means that the actual pill can be moved outwards

        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isOutOfBounds(Vector2.up)) return Vector2.zero;

            direction = Vector2.up;
            relativePosition += Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            if (isOutOfBounds(Vector2.down)) return Vector2.zero;

            direction = Vector2.down;
            relativePosition += Vector2.down;

        } else if (Input.GetKeyDown(KeyCode.D))
        {
            if (isOutOfBounds(Vector2.right)) return Vector2.zero;

            direction = Vector2.right;
            relativePosition += Vector2.right;

        } else if (Input.GetKeyDown(KeyCode.A))
        {
            if (isOutOfBounds(Vector2.left)) return Vector2.zero;

            direction = Vector2.left;
            relativePosition += Vector2.left;
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

    private bool isOutOfBounds(Vector2 direction)
    {
        //takes a vector2 direction and checks if cursor can move in that direction 
        //returns true if move is going to be out of bounds
        if (direction == Vector2.up)
        {
            return relativePosition.y + 1 > gridSize.y;
        }

        if (direction == Vector2.down)
        {
            return relativePosition.y - 1 < -gridSize.y;
        }

        if (direction == Vector2.right)
        {
            return relativePosition.x + 1 > gridSize.x;
        }

        if (direction == Vector2.left)
        {
            return relativePosition.x - 1 < -gridSize.x;
        }

        return true;
    }

    public void newPill()
    {
        relativePosition = Vector2.zero;
    }
}
