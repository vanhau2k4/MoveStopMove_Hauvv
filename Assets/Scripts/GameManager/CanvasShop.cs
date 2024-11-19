using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : MonoBehaviour
{
    public GameObject Shop;
    public GameObject uiPlay;
    public Button buttonExit;
    public Button buttonHat;
    public Button buttonPant;
    public GameObject hat;
    public GameObject pant;
    // Start is called before the first frame update
    void Start()
    {
        buttonExit.onClick.AddListener(ExitButton);
        buttonHat.onClick.AddListener(ButtonHat);
        buttonPant.onClick.AddListener(ButtonPant);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExitButton()
    {
        Shop.SetActive(false);
        uiPlay.SetActive(true);
    }
    private void ButtonHat()
    {
        pant.SetActive(false);
        hat.SetActive(true);
    }
    private void ButtonPant()
    {
        hat.SetActive(false);
        pant.SetActive(true);
    }
}
