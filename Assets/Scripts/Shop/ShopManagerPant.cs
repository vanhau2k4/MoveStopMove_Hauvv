using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerPant : MonoBehaviour
{
    public ShopItemPant[] shopItemPant;
    public ShopTamplatePant[] shopTemplatePant;
    public GameObject[] shopPanels;
    public Button[] myPurchaseBtns;
    public UIPlay uIPlay;
    public ChangePant changePant;
    public bool exitPant;
    private void Start()
    {
        for (int i = 0; i < shopItemPant.Length; i++)
        {
            shopPanels[i].SetActive(true);
        }
        for (int i = 0; i < shopItemPant.Length; i++)
        {
            shopItemPant[i].isPurchased = PlayerPrefs.GetInt("ShopItemPurchased" + i, 0) == 1;
        }
        LoadPanels();
        CheckPurchaseable();
        exitPant = true;
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemPant.Length; i++)
        {
            if (shopItemPant[i].isPurchased)
            {
                shopTemplatePant[i].purchaseButton.gameObject.SetActive(false);
                shopTemplatePant[i].equipButton.gameObject.SetActive(true);
            }
            else if (GameManager.Instance.buyScoin >= shopItemPant[i].baseCost)
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }
    public void PurchaseItem(int btnNo)
    {
        if (GameManager.Instance.buyScoin >= shopItemPant[btnNo].baseCost)
        {
            GameManager.Instance.buyScoin = GameManager.Instance.buyScoin - shopItemPant[btnNo].baseCost;
            uIPlay.lastScoin = GameManager.Instance.buyScoin;
            PlayerPrefs.SetInt("Scoin", uIPlay.lastScoin);
            PlayerPrefs.Save();
            // Đánh dấu mục hàng là đã mua
            shopItemPant[btnNo].isPurchased = true;

            PlayerPrefs.SetInt("ShopItemPurchased" + btnNo, shopItemPant[btnNo].isPurchased ? 1 : 0);
            CheckPurchaseable();
        }
    }
    public void LoadPanels()
    {
        for (int i = 0; i < shopItemPant.Length; i++)
        {
            shopTemplatePant[i].titleTxt.text = shopItemPant[i].title;
            shopTemplatePant[i].descriptionTxt.text = shopItemPant[i].descpription;
            shopTemplatePant[i].imageTxt.sprite = shopItemPant[i].image;
            shopTemplatePant[i].costTxt.text = shopItemPant[i].baseCost.ToString();
        }
    }
    public void EquipItem(int itemIndex)
    {

        // Đảm bảo rằng item này là một loại mũ
        if (itemIndex < shopItemPant.Length)
        {

            changePant.PantType = (PantType)itemIndex;

            changePant.ApplyPantChange();
        }
    }
}
