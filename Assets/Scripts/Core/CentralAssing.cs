using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CentralAssing : MonoBehaviour
{
    public static OptionsSO OptionsSORef;
    public static DialoguesSO DialoguesSORef;

    public static ReferenceHolderSO ReferenceHolderSORef;
    public static string TagPlacesRef;
    public static string TagPlayerRef;

    [Header("SOs")]
    [SerializeField] private OptionsSO optionsSOAssing;
    [SerializeField] private DialoguesSO dialoguesSOAssing;
    [SerializeField] private ReferenceHolderSO referenceHolderSOAssing;
    
    [Header("Tags")]
    [SerializeField] private string tagPlacesAssing;
    [SerializeField] private string tagPlayerAssing;

    public void UpdateReferences()
    {
        OptionsSORef = optionsSOAssing;
        DialoguesSORef = dialoguesSOAssing;
        ReferenceHolderSORef = referenceHolderSOAssing;

        TagPlacesRef = tagPlacesAssing;
        TagPlayerRef = tagPlayerAssing;
    }
    private void OnEnable()
    {
        UpdateReferences();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CentralAssing))]
class CentralAssingEditor : Editor
{
    private CentralAssing centralAssing;

    private void OnEnable()
    {
        MonoBehaviour monoBev = (MonoBehaviour)target;
        centralAssing = monoBev.GetComponent<CentralAssing>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Update References"))
        {
            centralAssing.UpdateReferences();
        }
    }
}
#endif