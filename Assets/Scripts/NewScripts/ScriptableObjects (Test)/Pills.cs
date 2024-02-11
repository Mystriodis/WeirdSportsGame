using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/pills")]
public class Pills : ScriptableObject
{
    public GameObject prefab;                       //the actual pill
    public Sprite image;                            //image of the pill/item
    public int slots;                               //how many slots it will take up total
    public Vector2Int size = new Vector2Int(3, 3);  //sizing including length and width
    public int strength;                            //how many points to muscle it gives when placed
    public string itemName;                         //name (identify specifics of item type eg - which part of the syringe)
    public ItemType type;                           //general type
    

    //categorize item by 3 types
    public enum ItemType
    {
        Pill,
        Syringe,
        Phone   
    }
}
