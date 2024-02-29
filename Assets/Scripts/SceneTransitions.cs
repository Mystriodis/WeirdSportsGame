using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions instance;
    public Animator curtains;
    private bool isTransitioning = false; // Flag to check if a transition is already in progress

    private void Awake()
    {
        instance = this;
    }

    public void curtainScene(string scene)
    {
        if (!isTransitioning)
        {
            StartCoroutine(LoadCurtain(scene));
        }
    }

    IEnumerator LoadCurtain(string toScene)
    {
        isTransitioning = true; // Set the flag to indicate a transition is in progress
        curtains.SetTrigger("end");
        yield return new WaitForSeconds(0.9f);

        SceneManager.LoadScene(toScene);

        // Ensure the scene has fully loaded before triggering the start animation
        yield return new WaitForEndOfFrame();

        curtains.SetTrigger("start");
        yield return new WaitForSeconds(0.9f); // Optional delay before allowing another transition
        isTransitioning = false; // Reset the flag to allow subsequent transitions
    }
}