using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    private bool isShaking;
    private float currentDuration;
    private float currentMagnitude;
    [SerializeField] float defaultMagnitude, defaultDuration, additionalMagnitude;
    private Vector3 originalPosition;

    float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        Actions.shakeCamera += startShake;
    }

    private void OnDisable()
    {
        Actions.shakeCamera -= startShake;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            shake();
        }
    }

    public void startShake(int multiplier)
    {
        for (int i = 0; i < multiplier; i++)
        {
            if (isShaking)
            {
                currentDuration = elapsedTime + defaultDuration;
                currentMagnitude += additionalMagnitude;

            }
            else
            {
                currentDuration = defaultDuration;
                currentMagnitude = defaultMagnitude;
                isShaking = true;
            }
        }
    }
    private void shake()
    {
        if (elapsedTime < currentDuration)
        {
            Vector2 direction = originalPosition + (Vector3)(Random.insideUnitCircle.normalized * currentMagnitude);

            transform.position = new Vector3(direction.x, direction.y, -10f);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            elapsedTime = 0f;
            transform.position = originalPosition;
            isShaking = false;
        }
    }
}
