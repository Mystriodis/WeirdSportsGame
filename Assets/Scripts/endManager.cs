using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textWhite, textBlack;
    [SerializeField] Animator player1Animator, player2Animator;

    // Start is called before the first frame update
    void Start()
    {
        if (persistentManager.Instance == null)
        {
            print("There's no persisten manager!");
            return;
        }

        print (persistentManager.Instance.ending);

        switch (persistentManager.Instance.ending)
        {
            case "tie":
                updateText("It's a tie!");
                player1Animator.Play("win", 0);
                player2Animator.Play("win", 0);
                break;

            case "player1Won":
                updateText("Player 1 Wins!");
                player1Animator.Play("win", 0);
                player2Animator.Play("lose", 0);
                break;

            case "player2Won":
                updateText("Player 2 Wins!");
                player1Animator.Play("lose", 0);
                player2Animator.Play("win", 0);
                break;

            case "player1Caught":
                updateText("Player 1 Disqualified\nPlayer 2 Wins!");
                player1Animator.Play("caught", 0);
                player2Animator.Play("win", 0);
                break;

            case "player2Caught":
                updateText("Player 2 Disqualified\nPlayer 1 Wins!");
                player1Animator.Play("win", 0);
                player2Animator.Play("caught", 0);
                break;

            default:
                updateText("Error");
            break;
        }
    }

    private void updateText(string displayText)
    {
        textWhite.text = displayText;
        textBlack.text = displayText;
    }


}
