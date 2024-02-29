using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class replayButton : MonoBehaviour
{
    [SerializeField] Button replay;
    [SerializeField] Image icon;
    // Start is called before the first frame update
    void Start()
    {
        icon = replay.GetComponent<Image>();
        icon.enabled = false;
        replay.enabled = false;
        replay.onClick.AddListener(play);
        Invoke("showButton", 3f);
    }

    private void showButton()
    {
        icon.enabled = true;
        replay.enabled = true;
    }

    void play()
    {
        //enter main game
        SceneManager.LoadScene("main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
