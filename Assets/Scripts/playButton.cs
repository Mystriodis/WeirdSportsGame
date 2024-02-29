using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    [SerializeField] Button play;   //start or restart the game

    void Start()
    {
        play.onClick.AddListener(start);
    }

    void start()
    {
        //enter main game
        SceneTransitions.instance.curtainScene("main");
    }
}
