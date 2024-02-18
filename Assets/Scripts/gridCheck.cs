using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gridCheck : MonoBehaviour
{
    [SerializeField] Transform gridCorner; //top left of grid
    public Vector2 gridSize; //half size of grid
    private List<GameObject> deleteList = new List<GameObject>();
    private pillManager manager;
    [SerializeField] UnityEvent updateCaught;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<pillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string pillCheck(GameObject currentPill)
    {
        for (int i = 0; i < currentPill.transform.childCount; i++)
        {
            Vector2 segmentPosition = currentPill.transform.GetChild(i).position;

            Vector2 rowCastPos = new Vector2(gridCorner.position.x, segmentPosition.y);
            Vector2 columnCastPos = new Vector2(segmentPosition.x, gridCorner.position.y);

            RaycastHit2D[] rowHits = Physics2D.RaycastAll(rowCastPos, Vector2.right, gridSize.x * 2);
            RaycastHit2D[] columnHits = Physics2D.RaycastAll(columnCastPos, Vector2.down, gridSize.y * 2);

            if (rowHits.Length >= gridSize.x * 2 + 1)
            {
                deleteList = addToList(rowHits);
            }

            if (columnHits.Length >= gridSize.y * 2 + 1)
            {
                deleteList = addToList(columnHits);
            }

        }

        phoneCheck();
        clearPills();

        pillCount();
        return "";
    }

    private List<GameObject> addToList(RaycastHit2D[] hitArray)
    {
        for (int i = 0; i < hitArray.Length; i++)
        {
            deleteList.Add(hitArray[i].collider.gameObject);
        }

        return deleteList;
    }

    private void clearPills()
    {
        for (int i = 0; i < deleteList.Count; i++)
        {
            if (deleteList[i] == null) continue;

            Destroy(deleteList[i]);
        }


        deleteList.Clear();
    }

    private bool phoneCheck()
    {
        bool hasConnectedPhone = false;
        //adds any phone component in deletelist's parent to phonelist
        //returns true if there is at least 1 correctly connected pair of phone segments
        

        List<GameObject> phoneList = new List<GameObject>();
        for (int i = 0; i < deleteList.Count; i++)
        {
            //puts parent object in list
            if (deleteList[i].tag == "Phone" && !phoneList.Contains(deleteList[i].transform.parent.gameObject))
            {
                phoneList.Add(deleteList[i].transform.parent.gameObject);
            }
        }

        //add child phone components to list
        if (phoneList.Count > 0)
        {
            for (int i = 0; i < phoneList.Count;i++)
            {
                for (int j = 0; j < phoneList[i].transform.childCount; j++)
                {
                    GameObject currentPhoneSection = phoneList[i].transform.GetChild(j).gameObject;
                    deleteList.Add(currentPhoneSection);

                    //add other phone section if phone is connected and hasn't been added to list yet
                    if (currentPhoneSection.GetComponent<connectionCheck>() == null) continue;
                    connectionCheck connectedSegment = currentPhoneSection.GetComponent<connectionCheck>();
                    if (connectedSegment.connected == false) continue;
                    if (phoneList.Contains(connectedSegment.connectedObject)) continue;


                    phoneList.Add(currentPhoneSection.GetComponent<connectionCheck>().connectedObject);
                    hasConnectedPhone = true;
                }
                
            }
        }

        //TO ADD
        //trigger phone effect

        return hasConnectedPhone;
    }

    private void pillCount()
    {
        //checks the amount of pills on the grid for the caught meter
        int pillAmount = 0;
        for (int i = 0; i < gridSize.x*2+1; i++)
        {
            Vector2 columnCastPos = new Vector2(gridCorner.position.x + i, gridCorner.position.y);
            RaycastHit2D[] cast = Physics2D.RaycastAll(columnCastPos, Vector2.down, gridSize.y * 2);
            pillAmount += cast.Length;
        }

        manager.pillAmount = pillAmount;
        updateCaught.Invoke();
    }
}
