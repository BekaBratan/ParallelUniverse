using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject[] players;
    GameObject player;

    public float speed;
    public float nearDistance;

    private float distance;
    private float distance1;
    private float distance2;

    private Animator animator;
    private bool isAttack;

    public Transform atackPoint;
    public float atackRange = 0.5f;
    public LayerMask enemyLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    { 
        distance1 = Vector2.Distance(transform.position, players[0].transform.position); 
        distance2 = Vector2.Distance(transform.position, players[1].transform.position);
        if (players[0].activeSelf && players[1].activeSelf)
        {
            if (distance1 < distance2)
            {
                distance = distance1;
                player = players[0];
            } else
            {
                distance = distance2;
                player = players[1];
            }
        } else if (players[1].activeSelf)
        {
            distance = distance2;
            player = players[1];
        } else if (players[0].activeSelf)
        {
            distance = distance1;
            player = players[0];
        } else
        {
            player = null;
        }

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize(); 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance > nearDistance)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isWalk", true);
                if (this.transform.position.x - player.transform.position.x > 0)
                {
                    Quaternion target = Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1000);
                }
                else
                {
                    Quaternion target = Quaternion.Euler(0, 0, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1000);
                }
            } else if (!isAttack) {
                Attack1();
            }   
            else
            {
                animator.SetBool("isWalk", false);            }
        }
    }

   /* private List<GameObject> GetObjectsInLayer(GameObject root, int layer)
    {
        var ret = new List<GameObject>();
        foreach (Transform t in root.transform.GetComponentsInChildren(typeof(GameObject), true))
        {
            if (t.gameObject.layer == layer)
            {
                ret.Add(t.gameObject);
            }
        }
        return ret;
    } */



    public void Attack1()
    {
        isAttack = true;
        animator.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        isAttack = true;
        animator.SetTrigger("Attack2");
    }

    public void Attack3()
    {
        isAttack = true;
        animator.SetTrigger("Attack3");
    }

    public void Damage(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atackPoint.position, atackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public void Protect()
    {
        animator.SetTrigger("Protect");
    }

    public void stopAtack()
    {
        isAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        if (atackPoint == null)
            return;

        Gizmos.DrawWireSphere(atackPoint.position, atackRange);
    }
}
