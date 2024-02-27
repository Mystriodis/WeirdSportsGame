using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class pillDeath : MonoBehaviour
{
    public float scoreAmount;
    public int playerSide;
    public GameObject target;

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

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, distance/200);
        transform.localScale = new Vector2((distance+20)/(maxDistance+20), (distance+20)/(maxDistance+20)); 

        if (distance < 0.1f)
        {
            delete();
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
