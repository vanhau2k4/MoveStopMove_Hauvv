using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlay : MonoBehaviour
{
    public Button playGame;
    public GameObject canvasMenu;
    public Canvas inputCanvas;
    public Canvas canvasName;
    public Canvas canvasRadiu;
    public GameObject cameraPlayer;
    public GameObject enemyAlive;

    public TMP_Text textCoin;
    public int lastScoin;
    public SpawnEnemy spawnEnemy;

    public MainJoystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        playGame.onClick.AddListener(PlayGame);
        lastScoin = PlayerPrefs.GetInt("Scoin", 0);
        UpdateCoinText();
        GameManager.Instance.buyScoin = lastScoin;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinText();
    }

    private void PlayGame()
    {
        canvasMenu.SetActive(false);
        spawnEnemy.SpamnEnemy();
        joystick.isJovstick = true;
        inputCanvas.gameObject.SetActive(true);
        canvasName.gameObject.SetActive(true);
        canvasRadiu.gameObject.SetActive(true);
        cameraPlayer.transform.position = new Vector3(0,0,0);
        enemyAlive.SetActive(true);
    }
    public void UpdateCoin() 
    {

        lastScoin += GameManager.Instance.scoin;
        GameManager.Instance.buyScoin = lastScoin;
        GameManager.Instance.scoin = 0;
        PlayerPrefs.SetInt("Scoin", lastScoin);
        PlayerPrefs.Save();
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        textCoin.text = lastScoin.ToString();
    }
}
