using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ShoopMenuPant", menuName = "scriptable objects/New shop Item Pant", order = 1)]
public class ShopItemPant : ScriptableObject
{
    public string title;
    public string descpription;
    public Sprite image;
    public int baseCost;
    public bool isPurchased;

}
