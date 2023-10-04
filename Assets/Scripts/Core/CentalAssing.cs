using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CentalAssing : MonoBehaviour
{
    public static OptionsSO OptionsSORef;
    public static DialoguesSO DialoguesSORef;

    public static string TagPlacesRef;

    [Header("SOs")]
    [SerializeField] private OptionsSO optionsSOAssing;
    [SerializeField] private DialoguesSO dialoguesSOAssing;
    
    [Header("Tags")]
    [SerializeField] private string tagPlacesAssing;

    private void Start()
    {
        OptionsSORef = optionsSOAssing;
        DialoguesSORef = dialoguesSOAssing;
        TagPlacesRef = tagPlacesAssing;
    }
}
