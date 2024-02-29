using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
    public void confirm(InputAction.CallbackContext context)
    {
        if (state != "move") return;
        if (currentPill == null) return;

        if (context.started)
        {
            if (!canPlace()) return;

            place();
        }
    }

    public void movePill(InputAction.CallbackContext context)
    {
        if (state != "move") return;
        if (currentPill == null) return;

        if (context.started)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            if (isOutOfBounds(direction)) return;

            relativePosition += direction;
            currentPill.transform.position += (Vector3)direction;

            updateCursorPosition();
            
        }
    }

    public void rotate(InputAction.CallbackContext context)
    {
        if (state != "move") return;
        if (currentPill == null) return;

        if (context.started)
        {
            int rotateDirection = (int)context.ReadValue<float>();

            currentPill.transform.eulerAngles = new Vector3(0, 0, currentPill.transform.eulerAngles.z + (rotateDirection * 90));
            updateCursorPosition();
        }
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

        currentPill.GetComponent<AudioSource>().Play();
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
            if (currentSegment.tag == "Connection") continue;

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

    public void updateCursorPosition()
    {
        //MOVE CURSOR
        Vector2 sumVector = new Vector3(0f, 0f);
        int connections = 0;

        foreach (Transform child in currentPill.transform)
        {
            if (child.tag == "Connection")
            {
                connections++;
                continue;
            }
            
            sumVector += (Vector2)child.position;
        }

        Vector3 groupCenter = sumVector / (currentPill.transform.childCount-connections);

        Actions.moveCursor(manager.playerSide, groupCenter.x, groupCenter.y, true);
    }
}
