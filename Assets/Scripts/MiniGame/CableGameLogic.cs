using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;

public class CableGameLogic : MonoBehaviour
{
    public WireAccess[] wireAccesses;
    
    [Header("Tags")]
    [SerializeField] private string tagWireAccessPoints;
    [SerializeField] private string tagWirePlugs;

    public void CheckWireAccess(Vector3 basePos ,out Vector3 outcomePos)
    {
        bool helperBool = false;
        int i;
        for (i = 0;  i < wireAccesses.Length; i++)
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
    }
}

[Serializable]
public class WireAccess
{
    public bool Accessable;
    public GameObject WireAccessPointGO;
}

public class WireAccessClient : MonoBehaviour
{
    [HideInInspector] public CableGameLogic cableGameLogic;
    [HideInInspector] public int WireAccessID;

    private void OnMouseEnter()
    {
        print("Mouse Entered AccesPoint: " + gameObject.transform.parent.name);
        cableGameLogic.wireAccesses[WireAccessID].Accessable = true;
    }

    private void OnMouseExit()
    {
        print("Mouse Left AccesPoint");
        cableGameLogic.wireAccesses[WireAccessID].Accessable = false;
    }
}

public class WireMoveClient : MonoBehaviour
{
    [HideInInspector] public CableGameLogic cableGameLogic;

    private Vector3 basePos;
    private bool moveWire;

    private void OnMouseDown()
    {
        moveWire = true;
        print(moveWire);
    }

    private void OnMouseUp()
    {
        moveWire = false;
        print(moveWire);
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
