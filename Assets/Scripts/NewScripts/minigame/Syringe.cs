using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Syringe : MonoBehaviour
{
    [SerializeField] Transform posA, posB, vein;    //the two positions and the vein
    //[SerializeField] int speed = 5;               //how fast the syringe moves
    public AnimationCurve speed;                    //ramp up speed when closer to vein
    [SerializeField] Vector2 targetPos;             //where the syringe is headed

    [SerializeField] bool moving;                   //if the syringe is still moving
    [SerializeField] int currentRound = 0;          //how many loops its gone
    [SerializeField] int rounds = 1;                //how many loops it can go

    private Animator syringeAnim;                   //animator - 2 states, scrolling and end

    [SerializeField] minigameManager miniManager;

    private bool hasInjected = false;

    private void Start()
    {
        //syringe is moving
        moving = true;
        //starting direction
        targetPos = posA.position;
        transform.position = posB.position;
        //animation
        syringeAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (moving)
        {
            //continue swapping positions until the rounds end
            if (Vector2.Distance(transform.position, posA.position) < .1f)
            {
                //swap target position
                targetPos = posB.position;
                //increase the round number
                currentRound++;
            }
            //continue swapping position not on last round
            if (Vector2.Distance(transform.position, posB.position) < .1f && rounds > currentRound)
            {
                //swap target position
                targetPos = posA.position;
            }

            //on the very last round - end game
            if (Vector2.Distance(transform.position, posB.position) < .1f && rounds <= currentRound)
            {
                //"end" game when rounds end
                Inject();
                //targetPos = posA.position;
            }

            //move towards target (non speed ramp + speed ramp ver)
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed.Evaluate(transform.position.y) * Time.deltaTime);
        }


    }

    //function to calculate points, play animation, freeze movement, and anything else after minigame ends
    public void Inject()
    {
        if (hasInjected) return;

        //freeze movement
        moving = false;
        transform.position = transform.position;
        //change animation
        syringeAnim.SetTrigger("inject");

        hasInjected = true;
        StartCoroutine(nameof(finishMinigame));

    }

    IEnumerator finishMinigame()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Vector2.Distance(transform.position, vein.position) < 0.2f)
            {
                Actions.increaseScore(miniManager.playerSide, 50f / 10f);
            }
            else if (Vector2.Distance(transform.position, vein.position) < 0.5f)
            {
                Actions.increaseScore(miniManager.playerSide, 10f / 10f);
            }
            else if (Vector2.Distance(transform.position, vein.position) < 1f)
            {
                Actions.increaseScore(miniManager.playerSide, 5f / 10f);
            }
            else if (Vector2.Distance(transform.position, vein.position) < 1.5f)
            {
                Actions.increaseScore(miniManager.playerSide, 1f / 10f);
            }
            else
            {
                Actions.increaseScore(miniManager.playerSide, 0.5f / 10f);
            }

            yield return new WaitForSeconds(5f/60f);
        }

        yield return new WaitForSeconds(1f);
        miniManager.endMinigame();
    }
}
