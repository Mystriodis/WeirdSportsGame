using UnityEngine;

public class pillDeath : MonoBehaviour
{
    public float scoreAmount;
    public int playerSide;
    public GameObject target;
    private bool hasBeenClose = false;

    private float maxDistance;

    private void Update()
    {   
        fly();
    }

    private void fly()
    {
        // Vector2 direction = transform.position - target.transform.position;
        //direction.Normalize();

        float distance = Vector2.Distance(transform.position, target.transform.position);

        //makes the 
        if (!hasBeenClose)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.01f);
        } else
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.05f);
        }
        
        //transform.localScale = new Vector2((distance+20)/(maxDistance+20), (distance+20)/(maxDistance+20)); 

        
        if (distance < 0.25f)
        {
            delete();
        } else if (distance < 0.5f)
        {
            
           GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        } else if (distance < 1.5)
        {
            hasBeenClose = true;
        }
    }

    private void delete()
    {
        
        Actions.increaseScore(playerSide, scoreAmount);
        Destroy(gameObject);
    }

    public void setMaxDistance()
    {
        maxDistance = Vector2.Distance(transform.position, target.transform.position);
    }
}
