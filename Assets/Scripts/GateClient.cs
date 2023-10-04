using System.Collections;
using UnityEditor;
using UnityEngine;

public class GateClient : MonoBehaviour
{
    [HideInInspector] public GameObject AssingedPlace;

    public void EnterGate()
    {
        SwitchPlace();
    }

    private void SwitchPlace()
    {
        
    }

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

        string tmpName = gateClient.AssingedPlace == null ? "None" : gateClient.AssingedPlace.name;
        
        if (GUILayout.Button("Gate To Room: " + tmpName))
        {
            GameObject[] tempRooms = GameObject.FindGameObjectsWithTag(CentalAssing.TagPlacesRef);

            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < tempRooms.Length; i++)
            {
                GameObject tempRoom = tempRooms[i];
                menu.AddItem(new GUIContent(tempRooms[i].name), false, () =>
                {
                    gateClient.AssingedPlace = tempRoom;
                    Debug.Log("DialogueSelected: " + tempRoom);
                });
            }
            menu.ShowAsContext();
        }
    }
}
#endif
