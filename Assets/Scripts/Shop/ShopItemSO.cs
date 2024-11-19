using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ShoopMenu",menuName ="scriptable objects/New shop Item",order =1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string descpription;
    public Sprite image;
    public int baseCost;
    public bool isPurchased;
}
