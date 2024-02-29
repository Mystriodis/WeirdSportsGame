using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    [HideInInspector] public int playerSide;

    [SerializeField] Sprite grabSprite, hoverSprite;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Actions.moveCursor += updateCursorPosition;
    }

    private void OnDisable()
    {
        Actions.moveCursor -= updateCursorPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCursorPosition(int callingPlayerSide, float xPosition, float yPosition, bool isGrabbing)
    {
        if (callingPlayerSide != playerSide) return;

        if (isGrabbing)
        {
            transform.position = new Vector2(xPosition, yPosition);
        }
        else
        {
            transform.position = new Vector2(xPosition + 0.5f, yPosition - 0.75f);
        }


        if (isGrabbing)
        {
          spriteRenderer.sprite = grabSprite;
        } else
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }
}
