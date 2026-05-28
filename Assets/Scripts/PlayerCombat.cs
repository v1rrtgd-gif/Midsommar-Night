using System.Diagnostics;
using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerStats stats;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextAttackTime)
        {
            
            Attack();
            nextAttackTime = Time.time + (1f / stats.attackSpeed);
        }
    }

    void Attack()
    {
        GetComponentInChildren<SwordMotion>().PlayAttack();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer 
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            float damage = stats.attack;
            UnityEngine.Debug.Log("hit!");

            // Crit check
            if (UnityEngine.Random.value < stats.critChance)
            {
                damage *= stats.critDamage;
                UnityEngine.Debug.Log("CRIT!");
            }

            Enemy enemyScript =
    enemy.GetComponentInParent<Enemy>();

            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}