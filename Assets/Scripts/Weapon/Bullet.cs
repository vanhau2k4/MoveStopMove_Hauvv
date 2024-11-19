using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public float quay;
    private Vector3 target;
    private CharactedBase owner;
    private float timer = 0f; // Bộ đếm thời gian
    private float lifespan = 0.5f; // Thời gian tồn tại của viên đạn là 2 giây

    public void Initialize(Vector3 targetPosition, CharactedBase characterOwner)
    {
        target = targetPosition;
        owner = characterOwner;
        rb.useGravity = false;
        Move();
    }
    private void Start()
    {
    }
    private void Update()
    {
        // Quay viên đạn
        transform.eulerAngles = new Vector3(90, 0, quay);
        quay += 1000 * Time.deltaTime;

        // Tăng thời gian và kiểm tra để hủy viên đạn sau 2 giây
        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Move()
    {
        if (target != Vector3.zero)
        {
            Vector3 direction = (target - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharactedBase hitCharacter = other.GetComponent<CharactedBase>();

        if (hitCharacter != null && hitCharacter != owner)
        {
            int enemyPoints = hitCharacter.point;
            // Kiểm tra nếu đối tượng bị trúng đạn là Player
            if (hitCharacter is Player player && owner is Enemy enemy) // Kiểm tra nếu chủ sở hữu viên đạn là Enemy
            {
                string enemyName = enemy.randomName;
                GameManager.Instance.KilledName = enemyName;
            }
            hitCharacter.Die();
            owner.AddPoint(enemyPoints); 
            Destroy(gameObject);
        }
    }
}
