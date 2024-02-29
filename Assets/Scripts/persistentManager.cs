using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class persistentManager : MonoBehaviour
{
    public static persistentManager Instance { get; private set; }

    public string ending = ""; //player1Caught, player2Caught, player1Won, player2Won, tie

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