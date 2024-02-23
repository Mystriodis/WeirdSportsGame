using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pillMove : MonoBehaviour
{
    //controls pill movement
    //
    //To Add: continuous movement on button hold

    public GameObject currentPill;
    public string state;
    private Vector2 relativePosition;
    public Vector2 gridSize;
    [SerializeField] UnityEvent shake;
    private pillManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<pillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != "move") return;
        if (currentPill == null) return;

        movePill();
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!canPlace()) return;

            place();
        }
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

    private void place()
    {
        //places tiles
        //also calls functions to check rows, columns, and item combinations
        //switches to state depending on checks

        //reset position and activate colliders
        currentPill.transform.localScale = Vector3.one;

        for (int i =0; i <currentPill.transform.childCount; i++)
        {
            if (currentPill.transform.GetChild(i).GetComponent<connectionCheck>() != null)
            {
                currentPill.transform.GetChild(i).GetComponent<connectionCheck>().manager = manager;
                currentPill.transform.GetChild(i).GetComponent<connectionCheck>().check();
            }

            if (currentPill.transform.GetChild(i).GetComponent<Collider2D>() != null)
            {
                currentPill.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            }

            if (currentPill.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
            {
                currentPill.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }


        GetComponent<gridCheck>().pillCheck(currentPill);
        currentPill = null;
        shake.Invoke();

        //switch back to start of gameplay loop
        if (state == "move")
        {
            StartCoroutine(nameof(switchToSelection));
        }
    }

    private bool canPlace()
    {
        //raycasts from each individual segment to check if there are any overlaps
        for (int i = 0; i < currentPill.transform.childCount; i++)
        {
            Transform currentSegment = currentPill.transform.GetChild(i);

            RaycastHit2D hit = Physics2D.Raycast(currentSegment.position, Vector2.zero);

            if (hit)
            {
                if (hit.collider.tag == "Pill") return false;
                if (hit.collider.tag == "Boundary") return false;
                if (hit.collider.tag == "Phone") return false;
            }
        }

        return true;
    }

    IEnumerator switchToSelection()
    {
        //start on next frame so input for the pill placement and pill selection doesn't overlap
        yield return null;
        manager.switchToSelection();
    }
}
