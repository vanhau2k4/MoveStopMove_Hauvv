using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public enum ColorType
{
    Red = 0, Green = 1, Blue = 2,Yellow = 3, Pink = 4
}
public class CharactedBase : MonoBehaviour
{
    public ColorType color;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<Material> ListMau = new List<Material>();
    public Animator anim { get; private set; }
    public Weapon weapon { get; private set; }

 
    public LayerMask enemyLayer;
    private int numOfEnemy;
    Collider[] hitColliders = new Collider[20];
    public float radius;
    public Vector3? target;

    
    public string nameKilled = "";

    public int point;
    public TMP_Text scoreText;
    public GameObject scaleAnimator;
    public int lastScalePointThreshold = 0 ;

    public SouscePlay souscePlay;
    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        souscePlay = FindObjectOfType<SouscePlay>();
        ChangeColor();

    }

    protected virtual void Update()
    {

    }
    
    protected virtual void OnInit()
    {

    }

    public virtual void Attack()
    {
        if (target.HasValue)
        {
            // Tính hướng tới mục tiêu
            Vector3 direction = (target - transform.position).Value;
            direction.y = 0; // Loại bỏ thành phần theo trục Y (chỉ quay quanh trục Y)
            direction = direction.normalized; // Chuẩn hóa vector hướng

            if (direction != Vector3.zero)
            {
                // Tính toán góc quay
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                // Đặt góc quay chỉ theo trục Y, còn trục X và Z bằng 0
                transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
            }
        }
        else
        {
            return;
        }
        souscePlay.src.clip = souscePlay.attack;
        souscePlay.src.Play();
        weapon.Shoot(this);

    }

    public void ChangeColor()
    {
        Material[] mat = skinnedMeshRenderer.materials;
        mat[0] = ListMau[Random.Range(0, ListMau.Count)];
        skinnedMeshRenderer.materials = mat;
    }

    public void FindTarget(Vector3 position)
    {
        numOfEnemy = Physics.OverlapSphereNonAlloc(position, radius, hitColliders, enemyLayer);
        float minDistance = Mathf.Infinity;
        float minDistanceThreshold = 0.5f;
        Collider nearestEnemy = null;

        for (int i = 0; i < numOfEnemy; i++)
        {

            float distance = (position - hitColliders[i].transform.position).sqrMagnitude;
            if (distance < minDistanceThreshold * minDistanceThreshold)
                continue;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = hitColliders[i];
            }
        }
        if (nearestEnemy != null)
        {
            Vector3 enemyPosition = nearestEnemy.transform.position;
            target = new Vector3(enemyPosition.x, enemyPosition.y + 1f, enemyPosition.z);
        }
        else if (nearestEnemy == null)
        {
            target = null;
        }
    }
    public virtual void Die()
    {
        souscePlay.src.clip = souscePlay.touch;
        souscePlay.src.Play();
        souscePlay.src.clip = souscePlay.die;
        souscePlay.src.Play();
        
    }
    public virtual void AddPoint(int enemyPoints)
    {

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);

        for (int i = 0; i < numOfEnemy; i++)
        {
            Gizmos.DrawLine(transform.position, hitColliders[i].transform.position);
        }
    }
}
