using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopTemplate;
    public GameObject[] shopPanels;
    public Button[] myPurchaseBtns;
    public UIPlay uIPlay;
    public ChangeHat changeHat;
    private void Start()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].SetActive(true); 
        }

        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopItemSO[i].isPurchased = PlayerPrefs.GetInt("ShopItemPurchased" + i, 0) == 1;
        }
        LoadPanels();
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (shopItemSO[i].isPurchased)
            {
                shopTemplate[i].purchaseButton.gameObject.SetActive(false);
                shopTemplate[i].equipButton.gameObject.SetActive(true);
            }
            else if (GameManager.Instance.buyScoin >= shopItemSO[i].baseCost)
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
        if(GameManager.Instance.buyScoin >= shopItemSO[btnNo].baseCost)
        {
            GameManager.Instance.buyScoin = GameManager.Instance.buyScoin - shopItemSO[btnNo].baseCost;
            uIPlay.lastScoin = GameManager.Instance.buyScoin;
            PlayerPrefs.SetInt("Scoin", uIPlay.lastScoin);
            PlayerPrefs.Save();
            // Đánh dấu mục hàng là đã mua
            shopItemSO[btnNo].isPurchased = true;

            PlayerPrefs.SetInt("ShopItemPurchased" + btnNo, shopItemSO[btnNo].isPurchased ? 1 : 0);
            CheckPurchaseable();
        }
    }
    public void LoadPanels()
    {
        for(int i = 0; i < shopItemSO.Length; i++)
        {
            shopTemplate[i].titleTxt.text = shopItemSO[i].title;
            shopTemplate[i].descriptionTxt.text = shopItemSO[i].descpription;
            shopTemplate[i].imageTxt.sprite = shopItemSO[i].image;
            shopTemplate[i].costTxt.text = shopItemSO[i].baseCost.ToString();
        }
    }
    public void EquipItem(int itemIndex)
    {
        // Đảm bảo rằng item này là một loại mũ
        if (itemIndex < shopItemSO.Length)
        {
            changeHat.hatType = (HatType)itemIndex;

            changeHat.CreateSpecificHat();
        }
    }
}
