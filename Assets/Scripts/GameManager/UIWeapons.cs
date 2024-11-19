using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapons : MonoBehaviour
{
    public GameObject canvasShop;
    public Button buttonWeapons;
    // Start is called before the first frame update
    void Start()
    {
        buttonWeapons.onClick.AddListener(Shop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Shop()
    {
        canvasShop.SetActive(true);
    }
}