using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class InventoriItem 
{
    [SerializeField] private CrystallType crystallType;
    [SerializeField] private int count;

    public CrystallType CrystallType { get => crystallType; set => crystallType = value; }
    public int Count { get => count; set => count = value; }
}

