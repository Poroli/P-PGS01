using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private RaycastHit2D hit;
    private Camera cam;
    private PlayerMove playerMove;
    private KeyCode inputKey;

    private void CheckInputOutcome()
    {
        Vector3 RayStartPos = cam.ScreenToWorldPoint(Input.mousePosition);
        RayStartPos.z = cam.transform.position.z;
        Ray ray = new Ray(RayStartPos, Vector3.forward);
        hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider == null)
        {
            print("hit nothing");
            return;
        }
        
        switch (hit.collider.gameObject.tag)
        {
            case "UselessObject":
                playerMove.SetTargetPosition(hit.collider.gameObject.transform.position, true);
                print("UselessObject");
                break;
            case "TalkableObject":
                GameObject tempGO = hit.collider.gameObject;
                playerMove.SetTargetPosition(tempGO.transform.position, true);
                tempGO.GetComponent<DialogueClient>().WaitForDialogue();
                print ("TalkableOnject");
                break;
            case "Gate":
                hit.collider.GetComponent<GateClient>().EnterGate();
                break;
        }

        // Check if GroundLayer
        if (hit.collider.gameObject.layer == 3)
        {
            playerMove.SetTargetPosition(hit.point, false);
            return;
        }
    }

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        cam = Camera.main;

        inputKey = CentralAssing.OptionsSORef.InputKey;
    }

    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            CheckInputOutcome();
        }
    }
}
