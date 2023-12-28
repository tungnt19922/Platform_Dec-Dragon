using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range; 

    [SerializeField] private float damage;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer > attackCooldown)
            {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("meeleAttack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
             0, Vector2.left, 0,playerLayer);

        if (hit.collider != null)
            playerHealth = hit.collider.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));  
    }
}
