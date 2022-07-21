using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleHeadBarRoot : MonoBehaviour
{
    public static RoleHeadBarRoot Instance;
    private void Awake()
    {
        Instance = this;
    }
}
