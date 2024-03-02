using UnityEngine;

public class EnemySoldier : MonoBehaviour
{
    
    public float moveSpeed = 2f; 
    public int attackDamage = 1; 
    public Collider2D attackCollider; 
    public Animator enemyAnimator;

    public Transform playerTransform;
    public float detectionRange = 5f; 
    public float attackRange = 2f; 
    private bool isDead = false;
    private bool isFacingRight = true;

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRange && !isDead)
        {
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else
            {
                ChasePlayer();
            }

        }
        else
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        enemyAnimator.SetBool("isWalkSoldier", true);
        MoveTowardsPlayer();
        LookAtPlayer();
    }

    private void StopChasingPlayer()
    {
        enemyAnimator.SetBool("isWalkSoldier", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Enemy takes damage");
        Die();
    }

    private void Die()
    {
        if (isDead) return;

        enemyAnimator.SetTrigger("die");
        isDead = true;
        StopChasingPlayer();
        attackCollider.enabled = false;
        Invoke("DestroyObject", 1.0f);
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void StartAttack()
    {
        attackCollider.enabled = true; 
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y), moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        enemyAnimator.SetBool("isWalkSoldier", false);
        enemyAnimator.SetTrigger("attack");
        StartAttack();
    }
    private void LookAtPlayer()
    {
        var direction = playerTransform.position.x > transform.position.x ? 1 : -1;
        if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

}
