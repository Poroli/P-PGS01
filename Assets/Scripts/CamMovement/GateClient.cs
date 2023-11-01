using System.Collections;
using UnityEditor;
using UnityEngine;

public class GateClient : MonoBehaviour
{
    private CameraMove camMove;
    private GameObject player;
    private GameObject assingedPlace;

    public void EnterGate()
    {
        SwitchPlace();
    }

    private void SwitchPlace()
    {
        print (assingedPlace);
        //Set new CamBorders
        if (!assingedPlace.transform.GetChild(0).GetChild(0).TryGetComponent(out BorderHolder borderHolder))
        {
            return;
        }
        camMove.BorderPoints = borderHolder.BorderPoints;

        //Set Player and Cam to new Positions
        player.transform.position = assingedPlace.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).transform.position;
        camMove.gameObject.transform.position = assingedPlace.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).transform.position;
        //Set new Player Target Position
        PlayerMove.TargetPosition = assingedPlace.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).transform.position;
    }

    private void GetRefOfObject()
    {
        var GORefLink = CentralAssing.ReferenceHolderSORef.GOReferences;
        for (int i = 0; i < GORefLink.Count; i++)
        {
            if (GORefLink[i].GONameAsks != gameObject.name)
            {
                continue;
            }
            assingedPlace = GameObject.Find(GORefLink[i].GONameReferenced);
            break;
        }
    }

    //Turn On/OFF the Gate Visual if Mouse over it
    private void OnMouseEnter()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        camMove = FindObjectOfType<CameraMove>();
        player = GameObject.FindWithTag(CentralAssing.TagPlayerRef);
        GetRefOfObject();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GateClient))]
class GateClientEditor : Editor
{
    private GateClient gateClient;

    private void OnEnable()
    {
        MonoBehaviour monoBev = (MonoBehaviour)target;
        gateClient = monoBev.GetComponent<GateClient>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        string tmpName = "None";
        bool helperBool = false;
        int i = 0;
        var GORefLink = CentralAssing.ReferenceHolderSORef.GOReferences;

        for (; i < GORefLink.Count; i++)
        {
            if (GORefLink[i].GONameAsks != gateClient.gameObject.name)
            {
                continue;
            }
            tmpName = GORefLink[i].GONameReferenced;
            helperBool = true;
            break;
        }
        if (!helperBool)
        {
            Debug.Log("CreatedNewListElement");
            GOReference tmpGORef = new();
            tmpGORef.GONameAsks = gateClient.gameObject.name;
            GORefLink.Add(tmpGORef);
        }
        
        if (GUILayout.Button("Gate To Room: " + tmpName))
        {
            GameObject[] tempRooms = GameObject.FindGameObjectsWithTag(CentralAssing.TagPlacesRef);

            GenericMenu menu = new GenericMenu();
            for (int j = 0; j < tempRooms.Length; j++)
            {
                GameObject tempRoom = tempRooms[j];
                menu.AddItem(new GUIContent(tempRooms[j].name), false, () =>
                {
                    GORefLink[i].GONameReferenced = tempRoom.name;
                    Debug.Log("GateSelected: " + tempRoom);
                });
            }
            menu.ShowAsContext();
        }

        if (GUI.changed)
        {
            // Markieren Sie das Objekt als "dirty"
            EditorUtility.SetDirty(CentralAssing.ReferenceHolderSORef);

            // Und speichern Sie alle Änderungen sofort
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif
