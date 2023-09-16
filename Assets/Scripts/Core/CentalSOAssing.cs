using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CentalSOAssing : MonoBehaviour
{
    public static OptionsSO optionsSORef;
    public static DialoguesSO dialoguesSORef;

    [SerializeField] private OptionsSO optionsSOAssing;
    [SerializeField] private DialoguesSO dialoguesSOAssing;

    private void Start()
    {
        optionsSORef = optionsSOAssing;
        dialoguesSORef = dialoguesSOAssing;
    }
}
