using System.Collections;
using UnityEngine;

public class connectionCheck : MonoBehaviour
{
    [HideInInspector] public string currentObject = "";
    [HideInInspector] public bool connected = false;
    [HideInInspector] public GameObject connectedObject, connectedObject1;
    [HideInInspector] public pillManager manager, opponentManager; //manager set from pillMove.cs
    private AudioSource callSFX;

    // Start is called before the first frame update
    void Start()
    {
        callSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void check()
    {
        switch (currentObject)
        {
            case "phone0": //phone top
                checkPhonePosition("phone1");
            break;

            case "phone1": //phone bottom
                checkPhonePosition("phone0");
            break;

            case "syringe0":
            case "syringe1":
            case "syringe2":
                checkSyringePosition();
            break;
        }
    }

    private void checkPhonePosition(string target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);


        if (!hit) return;
        if (hit.collider.gameObject.GetComponent<connectionCheck>() == null) return;

        connectionCheck neighborSegment = hit.collider.gameObject.GetComponent<connectionCheck>();
        if (neighborSegment.currentObject == target) 
        {
            startCall(neighborSegment);
        }
    }

    private void checkSyringePosition()
    {
        Vector2 castPosition0 = Vector2.zero; 
        Vector2 castPosition1 = Vector2.zero;
        string target0 = "";
        string target1 = "";
        Vector2 syringeDirection = (transform.position - transform.parent.position).normalized;
        if (currentObject == "syringe2") syringeDirection = -syringeDirection;

        switch (currentObject)
        {
            case "syringe0":
                castPosition0 = (Vector2)transform.position + syringeDirection;
                castPosition1 = (Vector2)transform.position + (syringeDirection * 2);

                target0 = "syringe1";
                target1 = "syringe2";
            break;

            case "syringe1":
                castPosition0 = (Vector2)transform.position - syringeDirection;
                castPosition1 = (Vector2)transform.position + syringeDirection;

                target0 = "syringe0";
                target1 = "syringe2";
            break;

            case "syringe2":
                castPosition0 = (Vector2)transform.position - syringeDirection;
                castPosition1 = (Vector2)transform.position - syringeDirection * 2;

                target0 = "syringe1";
                target1 = "syringe0";
            break;

            default:
                return;
        }

        RaycastHit2D hit0 = Physics2D.Raycast(castPosition0, Vector2.zero);
        RaycastHit2D hit1 = Physics2D.Raycast(castPosition1, Vector2.zero);

        if (!hit0 || !hit1) return;
        if (hit0.collider.gameObject.GetComponent<connectionCheck>() == null) return;
        if (hit1.collider.gameObject.GetComponent<connectionCheck>() == null) return;

        connectionCheck neighborSegment0 = hit0.collider.gameObject.GetComponent<connectionCheck>();
        connectionCheck neighborSegment1 = hit1.collider.gameObject.GetComponent<connectionCheck>();

        if (neighborSegment0.currentObject == target0 && neighborSegment1.currentObject == target1)
        {
            manager.switchToSyringe();

            Destroy(neighborSegment0.transform.parent);
            Destroy(neighborSegment1.transform.parent);
            Destroy(gameObject.transform.parent);
        }

    }

    private void startCall(connectionCheck neighborSegment)
    {
        neighborSegment.connected = true;
        connected = true;
        connectedObject = neighborSegment.transform.parent.gameObject;
        neighborSegment.connectedObject = transform.parent.gameObject;
        Actions.getOpponentManager(manager.playerSide, this);

        StartCoroutine(call());

    }

    IEnumerator call()
    {
        for (int i = 0; i <3; i++)
        {
            Actions.shakeCamera(1);
            callSFX.Play();
            opponentManager.extraCaughtValue += 2;
            opponentManager.updateCaught();
            yield return new WaitForSeconds(2);
        }

        
        Destroy(connectedObject);
        Destroy(gameObject.transform.parent);
    }
}
