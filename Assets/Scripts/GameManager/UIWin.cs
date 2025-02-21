using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : MonoBehaviour
{
    public TMP_Text aliveScoreFinal;
    public TMP_Text ScoreFinal;
    public Button touchToContinue;
    private SpawnEnemy spawnEnemy;
    public Player player;
    public GameObject CanvasWin;
    public GameObject CanvasAlive;
    public GameObject gameMenu;
    public UIPlay uiPlay;
    public GameObject CanvasName;
    public GameObject CanvasRadiu;
    void Start()
    {
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        // Load last saved scoin value
        touchToContinue.onClick.AddListener(() => Invoke(nameof(TouchToContinue), 1f));
    }

    public void ScoreFinals()
    {
        int scoseFinal = 1 + SpawnEnemy.spawnCounter;
        aliveScoreFinal.text = "#" + scoseFinal;
        ScoreFinal.text = player.point.ToString();
    }

    private void TouchToContinue()
    {
        if (CanvasWin != null && CanvasAlive != null && gameMenu != null)
        {
            gameMenu.SetActive(true);
            CanvasAlive.SetActive(false);
            CanvasName.SetActive(false);
            CanvasRadiu.SetActive(false);
            uiPlay.UpdateCoin();
            spawnEnemy.ClearAllEnemies();
            player.scaleAnimator.transform.localScale = new Vector3(1, 1, 1);
            player.SpamwPlayer();
            CanvasWin.SetActive(false);
        }
    }
}
