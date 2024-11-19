using System.Collections;
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
    private Player player;
    public GameObject CanvasDie; // Canvas cha
    public GameObject CanvasAlive; // Canvas cha
    public GameObject gameMenu;
    public UIPlay uiPlay;
    public GameObject cameraPlyer;
    public GameObject CanvasName;
    public GameObject CanvasRadiu;
    void Start()
    {
        player = FindObjectOfType<Player>();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        // Load last saved scoin value

        AliveScoreFinal();
        touchToContinue.onClick.AddListener(() => Invoke("TouchToContinue", 1f));
    }
    private void Update()
    {
        ScoreFinals();
    }
    public void AliveScoreFinal()
    {
        aliveScoreFinal.text = "#" + spawnEnemy.spawnCounter.ToString();

    }
    private void ScoreFinals()
    {
        ScoreFinal.text = "" + player.point.ToString();
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
            player.scaleAnimator.transform.localScale = new Vector3(1,1,1);
            SpamwPlayer();
            CanvasDie.SetActive(false);
        }
    }

    private void SpamwPlayer()
    {
        player.gameObject.SetActive(true);
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(0,180,0);
        player.joystick.movementSpeed = 10;
        player.capsuleCollider.enabled = true;
        player.characted.enabled = true;
        
        cameraPlyer.transform.position = new Vector3(2.622683e-07f, -4, 3);
        player.point = 0;
        player.UpdateScoreText();
        player.dieCheck = false;
    }
}
