using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03 : EnemyBase
{
    // public GameObject bulletPrefab; //子弹预制体
    // public Transform firePoint; //射击位置

    public Bullet03 bullet; //子弹脚本 
    

    protected override void Awake()
    {
        base.Awake();

        damage = 1;
        moveSpeed = 0.5f;
        chaseRange = 1.3f;
        attackRange = 0.3f;
        attackCooldown = 2.5f;
        maxHealth = 1000;
        isFaceRight = false;
        isBoss = true;
        health = maxHealth;

        bullet = GetComponentInChildren<Bullet03>();
        
    }



    //攻击
    protected override void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position, attackRange, playerLayer);
        
        //触发攻击
        if (hit != null)
        {
            //播放攻击动画
            _anim.SetTrigger("Attack");

            //展示子弹
            bullet.OpenCollier();
            
            //播放音效
            MusicManager.Get().CreateMusic("enemy03Attack");
            
            //记录最后攻击时间
            lastAttackTime = Time.time;

        }




    }


    private void OnDrawGizmosSelected()
    {
        //红色攻击范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        //视线范围
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
    }
}
