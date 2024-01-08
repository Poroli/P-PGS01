using System;
using System.Collections.Generic;
using UnityEngine;

public class CableGameLogic : MonoBehaviour
{
    public WireAccess[] wireAccesses;
    
    [Header("Tags")]
    [SerializeField] private string tagWireAccessPoints;
    [SerializeField] private string tagWirePlugs;
    private int randomCallIncomming;
    private int randomCallOutgoing;

    public void CheckWireAccess(Vector3 basePos, out Vector3 outcomePos)
    {
        bool helperBool = false;
        int i;
        for (i = 0; i < wireAccesses.Length; i++)
        {
            if (!wireAccesses[i].Accessable)
            {
                continue;
            }
            helperBool = true;
            break;
        }

        if (!helperBool)
        {
            outcomePos = basePos;
            return;
        }

        wireAccesses[i].Accessable = false;
        outcomePos = wireAccesses[i].WireAccessPointGO.transform.position;

        // Anwenden des Calls
        if (i != randomCallIncomming && i != randomCallOutgoing)
        {
            return;
        }

        if (i == randomCallIncomming && !wireAccesses[randomCallOutgoing].IsInACall)
        {
            wireAccesses[randomCallOutgoing].WaitingForCall = true;
            wireAccesses[randomCallOutgoing].WireAccessPointGO.transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        wireAccesses[i].IsInACall = true;
        wireAccesses[i].WaitingForCall = false;
        wireAccesses[i].WireAccessPointGO.transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void CreateCall()
    {
        // erstellen einer Liste aller möglichen WireAccesses Positionen
        List<int> WireAccessIDs = new();
        for (int i = 0; i < wireAccesses.Length; i++)
        {
            WireAccessIDs.Add(i);
        }

        // zufällige Auswahl von zwei WireAccesses Positionen
        randomCallIncomming = WireAccessIDs[UnityEngine.Random.Range(0, WireAccessIDs.Count)];
        WireAccessIDs.Remove(randomCallIncomming);
        randomCallOutgoing = WireAccessIDs[UnityEngine.Random.Range(0, WireAccessIDs.Count)];
        WireAccessIDs.Remove(randomCallOutgoing);

        //eintagen des Calls in WireAccesses
        wireAccesses[randomCallIncomming].WaitingForCall = true;
        wireAccesses[randomCallIncomming].WireAccessPointGO.transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = Color.yellow;

        CallIsGoingOn();
    }

    private void CallIsGoingOn()
    {
        if (wireAccesses[randomCallIncomming].IsInACall || wireAccesses[randomCallOutgoing].IsInACall)
        {
            return;
        }
        print("U did it! The call is going on");
    }

    private void SetUpWireMovement()
    {
        GameObject[] tmpGOs = GameObject.FindGameObjectsWithTag(tagWirePlugs);
        for (int i = 0; i < tmpGOs.Length; i++)
        {
            tmpGOs[i].AddComponent<WireMoveClient>().cableGameLogic = this;
        }
    }

    private void SetUpWireAccesses()
    {
        GameObject[] tmpGOs = GameObject.FindGameObjectsWithTag(tagWireAccessPoints);
        wireAccesses = new WireAccess[tmpGOs.Length];
        for (int i = 0; i < tmpGOs.Length; i++)
        {
            wireAccesses[i] = new();
            wireAccesses[i].WireAccessPointGO = tmpGOs[i];
            WireAccessClient tmpWAC = tmpGOs[i].AddComponent<WireAccessClient>();
            tmpWAC.cableGameLogic = this;
            tmpWAC.WireAccessID = i;
        }
    }

    void Start()
    {
        SetUpWireAccesses();
        SetUpWireMovement();

        CreateCall();
    }
}

[Serializable]
public class WireAccess
{
    public bool Accessable;
    public bool WaitingForCall;
    public bool IsInACall;
    public GameObject WireAccessPointGO;
}

public class WireAccessClient : MonoBehaviour
{
    [HideInInspector] public CableGameLogic cableGameLogic;
    [HideInInspector] public int WireAccessID;

    private void OnMouseEnter()
    {
        cableGameLogic.wireAccesses[WireAccessID].Accessable = true;
    }

    private void OnMouseExit()
    {
        cableGameLogic.wireAccesses[WireAccessID].Accessable = false;
    }
}

public class WireMoveClient : MonoBehaviour
{
    [HideInInspector] public CableGameLogic cableGameLogic;

    private Vector3 basePos;
    private bool moveWire;
    private CircleCollider2D circleCollider2D;

    private void OnMouseDown()
    {
        moveWire = true;
        circleCollider2D.enabled = false;
    }

    private void OnMouseUp()
    {
        circleCollider2D.enabled = true;
        moveWire = false;
        cableGameLogic.CheckWireAccess(basePos, out Vector3 outcomePos);
        gameObject.transform.position = outcomePos;
    }

    private void MoveWire()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = new Vector3 (Camera.main.ScreenToWorldPoint(p).x, Camera.main.ScreenToWorldPoint(p).y, 0);
        gameObject.transform.position = pos;
    }

    void Start()
    {
        basePos = gameObject.transform.position;
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!moveWire)
        {
            return;
        }
    
        MoveWire();
    }
}
