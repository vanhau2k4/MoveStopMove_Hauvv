using System.Collections;
using System.Collections.Generic;
using Custom.Indicators;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharactedBase
{
    public NavMeshAgent agent;
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyIdle enemyIdle { get; private set; }
    public EnemyMove enemyMove { get; private set; }
    public EnemyAttack enemyAttack { get; private set; }
    public EnemyDie enemyDie { get; private set; }
    public EnemyDanceState enemyDance { get; private set; }
    public SpawnEnemy spawnEnemy { get; private set; }

    public float range;
    public Transform centrePoint;

    private CapsuleCollider capsuleCollider;

    private float timeToMove;
    private float moveInterval = 0.5f; // Thay đổi thời gian di chuyển ở đây
    public TMP_Text nameEnemyText;
    private string nameEnemy = "qwertyuiopasdfghjklzxcvbnm1234567890";
    public string randomName;
    public Canvas canVas;

    private OffscreenIndicators offscreenIndicators;
    private ChangeHat changeHat;
    private ChangePant changePant;
    
    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        enemyIdle = new EnemyIdle(this, stateMachine, "Idle");
        enemyMove = new EnemyMove(this, stateMachine, "Move");
        enemyAttack = new EnemyAttack(this, stateMachine, "Attack");
        enemyDie = new EnemyDie(this, stateMachine, "Die");
        enemyDance = new EnemyDanceState(this, stateMachine, "Dance");
    }

    protected override void Start()
    {
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        offscreenIndicators = FindObjectOfType<OffscreenIndicators>();
        changePant = GetComponent<ChangePant>();
        changeHat = GetComponent<ChangeHat>();
        base.Start();
        stateMachine.Initialize(enemyIdle);
        capsuleCollider = GetComponent<CapsuleCollider>();
        
        UpdateText();
        RandomPointEnemy();
        UpdateScoreText();
    }
    public void RandomPointEnemy()
    {
        point = Random.Range(0, 25);
    }
    private void RandomName()
    {
        int number = Random.Range(5, 9);
        for (int i = 0; i < number; i++)
        {
            randomName += nameEnemy[Random.Range(0, nameEnemy.Length)]; 
        }

    }
    public void UpdateText()
    {
        RandomName();
        nameEnemyText.text = randomName;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        // Kiểm tra thời gian để di chuyển
        timeToMove += Time.deltaTime;
        if (timeToMove >= moveInterval)
        {
            MoveToRandomPoint();
            timeToMove = 0f; // Đặt lại thời gian
        }
        if (point >= lastScalePointThreshold + 10)
        {
            lastScalePointThreshold += 10;
            ScaleAnimator();
        }
    }

    private void MoveToRandomPoint()
    {
        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
        }
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public override void Attack()
    {
        base.Attack();
    }
    public override void AddPoint(int enemyPoints)
    {
        base.AddPoint(enemyPoints);

        point += amount;
        UpdateScoreText();
    }
    public void ScaleAnimator()
    {
        scaleAnimator.transform.localScale += scaleIncrease;

        Vector3 canvasPosition = canVas.transform.position;
        canVas.transform.position = new Vector3(canvasPosition.x, canvasPosition.y + 0.5f, canvasPosition.z);
    }
    public Vector3 scaleIncrease = new Vector3(0.2f, 0.2f, 0.2f);
    public void UpdateScoreText()
    {
        scoreText.text = "" + point.ToString();
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(enemyDie);
        capsuleCollider.enabled = false;
        agent.speed = 0;
        if (SpawnEnemy.spawnCounter > 8) Invoke(nameof(BackEnemy),4);
        Invoke(nameof(HideEnemy),2);
    }
    private void HideEnemy()
    {
        offscreenIndicators.SyncIndicatorState(transform, false);
        gameObject.SetActive(false);
    }
    public void BackEnemy()
    {
        if(spawnEnemy.beBack == true)
        {
            stateMachine.ChangeState(enemyIdle);
            capsuleCollider.enabled = true;
            agent.speed = 5;
            randomName = "";
            RandomPointEnemy();
            UpdateScoreText();
            UpdateText();
            ChangeColor();
            changePant.ApplyPantChange();
            transform.position = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            offscreenIndicators.SyncIndicatorState(transform, true);
            gameObject.SetActive(true);
        }
    }
    // Hàm RandomPoint: Tìm một điểm hợp lệ trên NavMesh gần vị trí ngẫu nhiên được chọn trong phạm vi cho trước
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // Tạo điểm ngẫu nhiên trong hình cầu với bán kính là range
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        // Tìm vị trí gần nhất trên NavMesh từ điểm ngẫu nhiên
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
