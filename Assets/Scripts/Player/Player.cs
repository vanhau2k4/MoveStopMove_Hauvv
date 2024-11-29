using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : CharactedBase
{
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState playerIdleState {  get; private set; }
    public PlayerMoveState playerMoveState {  get; private set; }
    public PlayerAttack playerAttackState {  get; private set; }
    public PlayerDieState playerDieState {  get; private set; }
    public PlayerDanceState playerDanceState {  get; private set; }
    public MainJoystick joystick { get; private set; }
    public SpawnEnemy spawnEnemy { get; private set; }


    private Vector3 currentCricle;
    public GameObject cricleTarget;
    private GameObject currentCricleTarget;


    public CapsuleCollider capsuleCollider;

    public CharacterController characted;
    public CinemachineVirtualCamera virtualCamera;

    public Transform followPoint;
    public Transform lookAtPoint;

    public Canvas canVas;
    public Canvas canvasDie;

    public bool dieCheck = false;

    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");
        playerMoveState = new PlayerMoveState(this, stateMachine, "Move");
        playerAttackState = new PlayerAttack(this, stateMachine, "Attack");
        playerDieState = new PlayerDieState(this, stateMachine, "Die");
        playerDanceState = new PlayerDanceState(this, stateMachine, "Dance");
    }

    protected override void Start()
    {
        base.Start();
        joystick = FindObjectOfType<MainJoystick>();
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
        stateMachine.Initialize(playerIdleState);
        capsuleCollider = GetComponent<CapsuleCollider>();
        characted = GetComponent<CharacterController>();
        UpdateScoreText();
        point = 0;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        FindTarget(transform.position);
        CircleTarget();
        if (point >= lastScalePointThreshold + 10)
        {
            lastScalePointThreshold += 10;
            ScaleAnimator();
        }
    }

    public override void Attack()
    {
        base.Attack();

    }
    public override void Die()
    {
        base.Die(); 
        dieCheck = true;
        Invoke(nameof(CanvasDie), 1);
        joystick.movementDirection = Vector3.zero;
        joystick.movementSpeed = 0;
        joystick.isJovstick = false;
        stateMachine.ChangeState(playerDieState);
        joystick.inputCanvas.gameObject.SetActive(false);
        capsuleCollider.enabled = false;
        characted.enabled = false;
        souscePlay.src.clip = souscePlay.lost;
        souscePlay.src.Play();
        Invoke(nameof(AnPlayer), 2);
    }
    public void CanvasDie()
    {
        canvasDie.gameObject.SetActive(true);
    }
    private void AnPlayer()
    {
        gameObject.SetActive(false);
    }
    
    private void CircleTarget()
    {
        if (target.HasValue)
        {
            
            if (currentCricleTarget == null)
            {
                Vector3 enemyPositions = target.Value;
                currentCricle = new Vector3(enemyPositions.x, enemyPositions.y - 1f, enemyPositions.z);
                currentCricleTarget = Instantiate(cricleTarget, currentCricle, Quaternion.identity);
            }
            else
            {
                Vector3 enemyPositions = target.Value;
                currentCricle = new Vector3(enemyPositions.x, enemyPositions.y - 1f, enemyPositions.z);
                currentCricleTarget.transform.position = currentCricle;
                currentCricleTarget.SetActive(true);
            }
        }
        else if (currentCricleTarget != null)
        {
            currentCricleTarget.SetActive(false);
        }
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
        GameManager.Instance.scoin = point;
        UpdateScoreText();
    }
    public void ScaleAnimator()
    {
        scaleAnimator.transform.localScale += scaleIncrease;

        Vector3 canvasScale = canVas.transform.position;
        canVas.transform.position = new Vector3(canvasScale.x, canvasScale.y + 0.5f, canvasScale.z);
    }
    public Vector3 scaleIncrease = new Vector3(0.2f, 0.2f, 0.2f);

    public void UpdateScoreText()
    {
        scoreText.text = "" + point.ToString();
    }

}
