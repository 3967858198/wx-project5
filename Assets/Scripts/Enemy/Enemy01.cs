using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : EnemyBase
{
    
    
    protected override void Awake()
    {
     
        
        base.Awake();
        
        
        damage = 1; //攻击力
        moveSpeed = 0.5f;
        chaseRange = 1f;
        attackRange = 0.2f;
        attackCooldown = 2.5f;
        maxHealth = 1000;

        health = maxHealth;

        isFaceRight = false; 


    }

    // protected override void Update()
    // {
    //     base.Update();
    //     CheckFace();
    // }
    
    //攻击
    protected override void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
       
        //检测到玩家
        if (hit != null)
        {
            //触发攻击动画
            _anim.SetTrigger("Attack");
            
            //调用玩家受伤函数
            hit.GetComponent<Player>().Hurt(damage);
            
            //播放音效
            MusicManager.Get().CreateMusic("enemy01Attack");

            //记录最后的攻击时间
            lastAttackTime = Time.time;

        }
        
        
    }
    
    
    
    
}
