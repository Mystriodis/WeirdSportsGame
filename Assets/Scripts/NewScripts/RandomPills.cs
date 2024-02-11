using System.Collections.Generic;
using UnityEngine;

public class RandomPills : MonoBehaviour
{
    [SerializeField] List <Pills> pillTypes;            //list of all possible pill types, return the scriptable object

    [SerializeField] List<Pills> availablePills;        //the amount of pills left (and which ones)
    [SerializeField] Pills currentPill;                 //swapped pill after a selection is made - the new pill

    //[SerializeField] List<Pills> player1Pills;          //seperate the list to 2 - ensure both players get their own and seperate lists
    //[SerializeField] List<Pills> player2Pills;          

    [SerializeField] int optionsAmount = 25;            //how many pills are in the list total

    [SerializeField] pillMenu whichPill;                //grab the current index and the pill capsules
    [SerializeField] List<SpriteRenderer> pillDisplays; //the icon of all 3 pills


    // Start is called before the first frame update
    void Start()
    {
        //initialize pill list - give it (25 for now) slots
        availablePills = new List<Pills>(optionsAmount);

        //randomnize which pills are on the list based on list of pill types
        for (int i = 0; i < optionsAmount; i++)
        {
            availablePills.Add(pillTypes[Random.Range(0, pillTypes.Count)]);

            //duplicate the list to both players
            //player1Pills.Add(availablePills[i]);
            //player2Pills.Add(availablePills[i]);
        }

        //nab the pill capsules from the menu script and steal the sprite renderer
        pillDisplays = new List<SpriteRenderer>(3);
        for (int i = 0; i < 3; i++)
        {
            pillDisplays.Add(whichPill.pills[i].GetComponent<SpriteRenderer>());
            //set the starting 3 pills
            newPill(i);
        }
    }


    //randomnize new pill to display out of the array
    public void newPill(int selectedPill)
    {
        //error message if the list is empty and remove the sprite
        if (availablePills.Count == 0)
        {
            Debug.LogWarning("No available pills!");
            pillDisplays[selectedPill].enabled = false;
            return;
        }

        //get a new pill and change the sprite image
        currentPill = availablePills[Random.Range(0, availablePills.Count)];
        pillDisplays[selectedPill].sprite = currentPill.image;

        //remove pill from the list to prevent it from being pulled again
        availablePills.Remove(currentPill);
    }
}
