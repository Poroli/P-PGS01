using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BorderHolder : MonoBehaviour
{
    [HideInInspector] public bool ShowBorder;
    public Transform[] BorderPoints;

    private void OnDrawGizmos()
    {
        if (BorderPoints == null || BorderPoints.Length < 4 || !ShowBorder)
        {
            return;
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < BorderPoints.Length; i++)
        {
            Gizmos.DrawLine(BorderPoints[i].position, BorderPoints[(i + 1) % BorderPoints.Length].position);
        }
    }

    public void UpdateBorderPoints()
    {
        int tmpChildCount = gameObject.transform.childCount;
        BorderPoints = new Transform[tmpChildCount];

        for (int i = 0; i < tmpChildCount; i++)
        {
            BorderPoints[i] = gameObject.transform.GetChild(i);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BorderHolder))]
class BorderHolderEditor : Editor
{
    private BorderHolder borderDebug;

    private void OnEnable()
    {
        MonoBehaviour monoBev = (MonoBehaviour)target;
        borderDebug = monoBev.GetComponent<BorderHolder>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        string tmpName = !borderDebug.ShowBorder ? "false" : "true";
        
        if (GUILayout.Button("ShowBorders " + tmpName))
        {
            if (borderDebug.ShowBorder)
            {
                borderDebug.ShowBorder = false;
            }
            else if (!borderDebug.ShowBorder)
            {
                borderDebug.ShowBorder = true;
            }
        }
        if (GUILayout.Button("Update BorderPoints"))
        {
            borderDebug.UpdateBorderPoints();
        }

        // Wenn sich das Objekt geändert hat
        if (GUI.changed)
        {
            // Markieren Sie die Szene als "dirty"
            EditorSceneManager.MarkSceneDirty(borderDebug.gameObject.scene);
        }
    }
}
#endif
