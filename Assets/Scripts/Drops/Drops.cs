using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop", menuName = "Drop")]
public class Drops : ScriptableObject
{
    public GameObject itemToDrop;
    public float dropRate;
    public bool canDrop;

}
