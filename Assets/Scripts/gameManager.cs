using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    private float player1points, player2points;
    [SerializeField] Image player1Display, player2Display;
    // Start is called before the first frame update
    void Start()
    {
        player1points = 1;
        player2points = 1;
    }

    private void OnEnable()
    {
        Actions.increaseScore += increasePoints;
    }

    private void OnDisable()
    {
        Actions.increaseScore -= increasePoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increasePoints(int player, float amount)
    {
        if (player == 1)
        {
            player1points+=amount;
        }

        if (player == 2)
        {
            player2points += amount;
        }

        refreshUI();

    }

    private void refreshUI()
    {
        //player points display
        float pointTotal = player1points + player2points;
        player1Display.fillAmount = player1points / pointTotal;
        player2Display.fillAmount = player2points / pointTotal;
    }
}
