using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    private static GameObject sceneTransitions;
    private void Awake()
    {
        if (sceneTransitions == null)
        {
            sceneTransitions = gameObject;
            DontDestroyOnLoad(this);

        }
        else
        {
            Destroy(gameObject);
        }

        
    }
}
