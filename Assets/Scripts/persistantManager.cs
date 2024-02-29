using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistantManager : MonoBehaviour
{
    public static persistantManager Instance { get; private set; } //Tied to the class and not the instance itself

    public string ending = ""; //player1Caught, player2Caught, player1Won, player2Won

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}