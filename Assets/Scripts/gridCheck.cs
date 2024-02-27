using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gridCheck : MonoBehaviour
{
    [HideInInspector] public int playerSide;
    [HideInInspector] public Vector2 gridSize; //half size of grid
    [HideInInspector]  public GameObject pillParent;
    [SerializeField] Transform gridCorner; //top left of grid
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
        int lineClearAmount = 0;

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
                lineClearAmount++;
            }

            if (columnHits.Length >= gridSize.y * 2 + 1)
            {
                deleteList = addToList(columnHits);
                lineClearAmount++;
            }

        }

        phoneCheck();
        syringeClear();
        clearPills();

        pillCount();

        if (lineClearAmount > 0)
        {
            for (int i = 0; i < lineClearAmount; i++)
            {
                Actions.shakeCamera(1);
            }
        }
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
        //clear pills and then send cleared pill values to game manager
        //

        float clearedValue = 0;
        
        //clear pills
        for (int i = 0; i < deleteList.Count; i++)
        {
            if (deleteList[i] == null) continue;

           
            if (deleteList[i].tag == "Pill")
            {
                Pills pillGroupStats = deleteList[i].transform.parent.GetComponent<pillStats>().stats; //stats of the entire pill group
                clearedValue += pillGroupStats.slots / pillGroupStats.strength;
            }
            Destroy(deleteList[i]);
        }

        Actions.increaseScore(playerSide, clearedValue);

        //clear parents
        for (int i = 0; i < pillParent.transform.childCount; i++)
        {
            if (pillParent.transform.GetChild(i).transform.childCount == 0)
            {
                Destroy(pillParent.transform.GetChild(i).gameObject);
                
            }
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

    private void syringeClear()
    {
        //modified version of  phoneCheck()

        List<GameObject> syringeList = new List<GameObject>();
        for (int i = 0; i < deleteList.Count; i++)
        {
            //puts parent object in list
            if (deleteList[i].tag == "Syringe" && !syringeList.Contains(deleteList[i].transform.parent.gameObject))
            {
                syringeList.Add(deleteList[i].transform.parent.gameObject);
            }
        }

        
        if (syringeList.Count == 0) return;

        //add children to delete list
        for (int i = 0; i < syringeList.Count; i++)
        {
            for (int j = 0; j < syringeList[i].transform.childCount; j++)
            {
                GameObject currentSyringeSection = syringeList[i].transform.GetChild(j).gameObject;
                deleteList.Add(currentSyringeSection);

            }

        }
        

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