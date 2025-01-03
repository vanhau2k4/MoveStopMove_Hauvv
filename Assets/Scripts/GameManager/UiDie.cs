﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UiDie : MonoBehaviour
{
    public TMP_Text aliveScoreFinal;
    public TMP_Text ScoreFinal;
    public TMP_Text nameKilled;
    public Button touchToContinue;
    private SpawnEnemy spawnEnemy;
    public Player player;
    public GameObject CanvasDie; 
    public GameObject CanvasAlive; 
    public GameObject gameMenu;
    public UIPlay uiPlay;
    public GameObject cameraPlyer;
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
        int scoseFinal = SpawnEnemy.spawnCounter += 1;
        aliveScoreFinal.text = "#" + scoseFinal;
        ScoreFinal.text = player.point.ToString();
        string lastKilledName = GameManager.Instance.KilledName;  // Đọc tên từ Singleton
        nameKilled.text = lastKilledName;
    }

    private void TouchToContinue()
    {
        if (CanvasDie != null && CanvasAlive != null && gameMenu != null)
        {
            gameMenu.SetActive(true);
            CanvasAlive.SetActive(false);
            CanvasName.SetActive(false);
            CanvasRadiu.SetActive(false);
            uiPlay.UpdateCoin();
            spawnEnemy.ClearAllEnemies();
            player.SpamwPlayer();
            CanvasDie.SetActive(false);
        }
    }
}
