using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class gridCheck : MonoBehaviour
{
    [SerializeField] Transform gridCorner; //top left of grid
    public Vector2 gridSize; //half size of grid

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string pillCheck(GameObject currentPill)
    {
        List<GameObject> deleteList = new List<GameObject>();

        for (int i = 0; i < currentPill.transform.childCount; i++)
        {
            Vector2 segmentPosition = currentPill.transform.GetChild(i).position;

            Vector2 rowCastPos = new Vector2(gridCorner.position.x, segmentPosition.y);
            Vector2 columnCastPos = new Vector2(segmentPosition.x, gridCorner.position.y);

            RaycastHit2D[] rowHits = Physics2D.RaycastAll(rowCastPos, Vector2.right, gridSize.x * 2);
            RaycastHit2D[] columnHits = Physics2D.RaycastAll(columnCastPos, Vector2.down, gridSize.y * 2);

            if (rowHits.Length >= gridSize.x * 2 + 1)
            {
                deleteList = addToList(deleteList, rowHits);
            }

            if (columnHits.Length >= gridSize.y * 2 + 1)
            {
                deleteList = addToList(deleteList, columnHits);
            }

        }

        phoneCheck(deleteList);
        clearPills(deleteList);

        return "";
    }

    private List<GameObject> addToList(List<GameObject> deleteList, RaycastHit2D[] hitArray)
    {
        for (int i = 0; i < hitArray.Length; i++)
        {
            deleteList.Add(hitArray[i].collider.gameObject);
        }

        return deleteList;
    }

    private void clearPills(List<GameObject> deleteList)
    {
        for (int i = 0; i < deleteList.Count; i++)
        {
            if (deleteList[i] == null) continue;

            Destroy(deleteList[i]);
        }
    }

    private List<GameObject> phoneCheck(List<GameObject> deleteList)
    {
        //adds any phone component in deletelist's parent to phonelist

        List<GameObject> phoneList = new List<GameObject>();
        for (int i = 0; i < deleteList.Count; i++)
        {
            if (deleteList[i].tag == "Phone" && !phoneList.Contains(deleteList[i].transform.parent.gameObject))
            {
                phoneList.Add(deleteList[i].transform.parent.gameObject);
            }
        }

        //add other phone components to list
        if (phoneList.Count > 0)
        {
            for (int i = 0; i < phoneList.Count;i++)
            {
                for (int j = 0; j < phoneList[i].transform.childCount; j++)
                {
                    deleteList.Add(phoneList[i].transform.GetChild(j).gameObject);
                }
                
            }
        }

        //trigger phone effect
        //TO ADD

        return deleteList;
    }
}
