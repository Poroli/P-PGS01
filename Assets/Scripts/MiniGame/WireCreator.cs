using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WireCreator : MonoBehaviour
{
    [SerializeField] private int LengthOfWire;
    [SerializeField] private int wireLayer;
    [SerializeField] private GameObject wirePartPrefab;
    [SerializeField] private float distanceBetweenWireParts;

    [Header("RelJointSettings")]
    [SerializeField][Range(0, 1)] private float correctionScale;
    [SerializeField] private float maxTorque;

    public void CreateWire()
    {
        if (LengthOfWire <= 0)
        {
            return;
        }
        GameObject tmpGO = new();
        GameObject[] tmpWireParts = new GameObject[LengthOfWire];
        for (int i = 0; i < LengthOfWire; i++)
        {
            Vector3 tmpPos = new Vector3(i * distanceBetweenWireParts, 0, 0);
            GameObject tmpWirePart = Instantiate(wirePartPrefab, tmpPos, Quaternion.identity, tmpGO.transform);
            tmpWireParts[i] = tmpWirePart;
        }

        for (int i = 0; i < tmpWireParts.Length; i++)
        {
            tmpWireParts[i].gameObject.layer = wireLayer;

            if (i > LengthOfWire - 2)
            {
                continue;
            }
            RelativeJoint2D tmpRelJoint = tmpWireParts[i].AddComponent<RelativeJoint2D>();
            tmpRelJoint.connectedBody = tmpWireParts[i + 1].GetComponent<Rigidbody2D>();
            tmpRelJoint.maxTorque = maxTorque;
            tmpRelJoint.correctionScale = correctionScale;

        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WireCreator))]
class WireCreatorEditor : Editor
{
    private WireCreator wireCreator;

    private void OnEnable()
    {
        MonoBehaviour monoBev = (MonoBehaviour)target;
        wireCreator = monoBev.GetComponent<WireCreator>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create Wire"))
        {
            wireCreator.CreateWire();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
#endif