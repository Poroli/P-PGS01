using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ReferenceHolder")]
public class ReferenceHolderSO : ScriptableObject
{
    public List<GOReference> GOReferences;
}

[Serializable]
public class GOReference
{
    public string GONameAsks;
    public string GONameReferenced;
}
