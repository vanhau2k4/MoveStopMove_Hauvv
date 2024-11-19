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

    protected override void Awake()
    {
        base.Awake();
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
        base.Start();
        stateMachine.Initialize(enemyIdle);
        capsuleCollider = GetComponent<CapsuleCollider>();
        RandomName();
        UpdateText();
        point = Random.Range(0,25);
        UpdateScoreText();
    }
    private void RandomName()
    {
        int number = Random.Range(5, 9);
        for (int i = 0; i < number; i++)
        {
            randomName += nameEnemy[Random.Range(0, nameEnemy.Length)]; 
        }
    }
    private void UpdateText()
    {
        nameEnemyText.text = randomName;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        FindTarget(transform.position);

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
        int amount = 1;

        if (enemyPoints >= 3 && enemyPoints <= 7)
        {
            amount = 2;
        }
        else if (enemyPoints >= 8 && enemyPoints <= 12)
        {
            amount = 3;
        }
        else if (enemyPoints >= 13 && enemyPoints <= 17)
        {
            amount = 4;
        }
        else if (enemyPoints >= 18 && enemyPoints <= 24)
        {
            amount = 5;
        }
        // Kiểm tra nếu đạt ngưỡng 10 điểm

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
        Destroy(gameObject, 2f);
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
