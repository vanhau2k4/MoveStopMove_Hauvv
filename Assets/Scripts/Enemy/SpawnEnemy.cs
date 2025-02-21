using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Custom.Indicators;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();
    public OffscreenIndicators indicators;

    public static int spawnCounter = 20; // Biến đếm bắt đầu từ 20
    public TMP_Text aliveScore;
    public int targetEnemyCount;
    public bool beBack;
    private void Update()
    {
        int totalAlive = spawnCounter + 1; // Thêm người chơi vào tổng số
        aliveScore.text = "Alive: " + totalAlive.ToString();
    }
    // Khởi tạo kẻ địch ban đầu
    public void InitializeEnemies()
    {
        beBack = true;
        if (enemyList.Count == 0)
        {
            for (int i = 0; i < targetEnemyCount; i++)
            {
                SpawnEnemyAtRandomPosition();
            }
        }
        else
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                Enemy enemy = enemyList[i].GetComponent<Enemy>();

                // Kiểm tra nếu component Enemy không null
                if (enemy != null)
                {
                    // Gọi phương thức BackEnemy() từ đối tượng Enemy
                    enemy.BackEnemy();
                }
            }
        }
    }
    public void ClearAllEnemies()
    {
        beBack = false;
        for(int i = 0;i < enemyList.Count; i++)
        {
            if(enemyList[i] != null)
            {
                enemyList[i].SetActive(false);
            }
        }
        spawnCounter = 20;
    }
    // Tạo kẻ địch tại vị trí ngẫu nhiên và thêm vào danh sách
    private void SpawnEnemyAtRandomPosition()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
        var enemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        indicators.AddTarget(enemy);
        enemyList.Add(enemy);
    }
}