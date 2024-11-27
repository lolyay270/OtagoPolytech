using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private categories category;
    public categories Category { get => category; }

    [SerializeField]
    private string itemName;
    public string ItemName { get => itemName; }

    [SerializeField][Min(0)]
    private int weight;
    public int Weight { get => weight; }

    [SerializeField]
    private rarity rarity;
    public rarity Rarity { get => rarity; }

    [SerializeField][Min(0)]
    private int damage;
    public int Damage { get => damage; }

    [SerializeField][Min(0)]
    private int armour;
    public int Armour { get => armour; }

    [Tooltip("The image that will show in menu of the item")][SerializeField]
    private Sprite image;

    // Start is called before the first frame update
    void Start()
    {
        if (damage > 0 && armour > 0|| damage == 0 && armour == 0) //must be one of armour or weapon
        {
            throw new Exception("Inventory item " + name + "must have positive values for one of armour or weapon");
        }
    }
}
