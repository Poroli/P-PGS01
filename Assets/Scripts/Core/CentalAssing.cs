using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CentalAssing : MonoBehaviour
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

    private void Start()
    {
        OptionsSORef = optionsSOAssing;
        DialoguesSORef = dialoguesSOAssing;
        ReferenceHolderSORef = referenceHolderSOAssing;

        TagPlacesRef = tagPlacesAssing;
        TagPlayerRef = tagPlayerAssing;
    }
}
