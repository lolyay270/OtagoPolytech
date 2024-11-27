using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;

public class InventoryFilter : MonoBehaviour
{
    [SerializeField]
    private categories category;
    public categories Category { get { return category; } }
}
