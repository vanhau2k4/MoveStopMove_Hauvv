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
    public int spawnCounter = 20; // Biến đếm bắt đầu từ 50
    public TMP_Text AliveScore;

    private int targetEnemyCount = 8;
    public List<GameObject> waitingToRespawn = new List<GameObject>();

    private void Start()
    {

    }
    public void SpamnEnemy()
    {
        InitializeEnemies();
        StartCoroutine(ManageEnemies());
    }
    private void Update()
    {
        AliveScore.text = "Alive:" + spawnCounter.ToString();
    }
    // Khởi tạo kẻ địch ban đầu
    private void InitializeEnemies()
    {
        for (int i = 0; i < targetEnemyCount; i++)
        {
            SpawnEnemyAtRandomPosition();
        }
    }
    public void ClearAllEnemies()
    {
        foreach (var enemy in enemyList)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        enemyList.Clear();
        waitingToRespawn.Clear();
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

    // Coroutine quản lý việc kiểm tra và tạo lại enemy
    private IEnumerator ManageEnemies()
    {
        while (true)
        {
            List<int> nullIndices = new List<int>();

            // Duyệt qua danh sách enemyList để tìm các mục null
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    nullIndices.Add(i);  // Ghi lại chỉ số của mục null
                    spawnCounter--;

                    if (spawnCounter >= 8)
                    {
                        waitingToRespawn.Add(enemyPrefab);
                    }
                }
            }

            // Loại bỏ các phần tử null sau khi đã duyệt qua hết danh sách
            foreach (int index in nullIndices.OrderByDescending(i => i))
            {
                enemyList.RemoveAt(index);  // Xóa mục tại các chỉ số null
            }

            // Tạo lại các enemy nếu cần thiết
            while (waitingToRespawn.Count > 0 && enemyList.Count < targetEnemyCount)
            {
                yield return new WaitForSeconds(3);
                var enemyToRespawn = waitingToRespawn[0];
                waitingToRespawn.RemoveAt(0);
                Vector3 respawnPosition = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
                var enemy = Instantiate(enemyToRespawn, respawnPosition, Quaternion.identity);
                indicators.AddTarget(enemy);
                enemyList.Add(enemy);
            }

            yield return new WaitForSeconds(0.2f);

            if (spawnCounter < 8)
            {
                continue;
            }
        }
    }

}