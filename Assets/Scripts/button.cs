using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button : MonoBehaviour
{
    [SerializeField] Button theButton;
    [SerializeField] string scene;

    void Start()
    {
        theButton.onClick.AddListener(click);
    }

    void click()
    {
        SceneTransitions.instance.curtainScene(scene);
    }
}
