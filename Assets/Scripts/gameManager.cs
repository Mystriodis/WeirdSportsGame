using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    private float player1points, player2points;
    [SerializeField] Image player1Display, player2Display;
    [SerializeField] pillManager player1Manager, player2Manager;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] int totalGameTime;
    [SerializeField] Transform pointsLeftEdge, pointsRightEdge, pointsLine;

    // Start is called before the first frame update
    void Start()
    {
        player1points = 1;
        player2points = 1;

        StartCoroutine(timer());
    }

    private void OnEnable()
    {
        Actions.increaseScore += increasePoints;
        Actions.getOpponentManager += getOpponentManager;
        Actions.playerCaught += caughtEnd;
    }

    private void OnDisable()
    {
        Actions.increaseScore -= increasePoints;
        Actions.getOpponentManager -= getOpponentManager;
        Actions.playerCaught -= caughtEnd;
    }

    // Update is called once per frame
    void Update()
    {
        refreshUI();
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

        

    }

    
    private void refreshUI()
    {
        //player points display
        float pointTotal = player1points + player2points;

        float player1DisplayTarget = player1points / pointTotal;
        float player2DisplayTarget = player2points / pointTotal;

        player1Display.fillAmount = Mathf.Lerp(player1Display.fillAmount, player1DisplayTarget, 0.001f);
        player2Display.fillAmount = Mathf.Lerp(player2Display.fillAmount, player2DisplayTarget, 0.001f);

        float pointsWidth = pointsRightEdge.position.x - pointsLeftEdge.position.x;

        pointsLine.position = new Vector2(pointsLeftEdge.position.x + player1Display.fillAmount * pointsWidth, pointsLine.position.y);
    }

    public void getOpponentManager(int playerSide, connectionCheck callingScript)
    {
        if (playerSide == 1)
        {
            callingScript.opponentManager = player2Manager;
        } else if (playerSide == 2)
        {
            callingScript.opponentManager = player1Manager;
        }

    }

    IEnumerator timer()
    {
        int timerCountDown = totalGameTime;
        timerDisplay.text = calculateTime(timerCountDown);

        for (int i = 0; i < totalGameTime; i++)
        {
            yield return new WaitForSeconds(1);
            timerCountDown--;
            timerDisplay.text = calculateTime(timerCountDown);
        }

        normalEnd();
    }

    private string calculateTime(int time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = time - (minutes * 60);

        string minuteText = minutes.ToString();
        string secondText = seconds.ToString();

        if (secondText.Length < 2)
        {
            secondText = "0" + secondText;
        }

        return minuteText + ":" + secondText;
    }

    private void normalEnd()
    {
        if (player1points > player2points)
        {
            persistentManager.Instance.ending = "player1Won";
        } else if (player2points > player1points)
        {
            persistentManager.Instance.ending = "player2Won";
        } else
        {
            persistentManager.Instance.ending = "tie";
        }

        SceneTransitions.instance.curtainScene("gameOver");
    }

    public void caughtEnd(int caughtSide)
    {
        if (caughtSide == 1)
        {
            persistentManager.Instance.ending = "player1Caught";
        } else if (caughtSide == 2)
        {
            persistentManager.Instance.ending = "player2Caught";
        }

        SceneTransitions.instance.curtainScene("gameOver");
    }
}
