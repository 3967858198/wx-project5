using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : EnemyBase
{
    public GameObject bulletPrefab; //子弹预制体
    public Transform firePoint; //发射点


    protected override void Awake()
    {
        base.Awake();

        damage = 1;
        moveSpeed = 0.5f;
        chaseRange = 1f;
        attackRange = 0.8f;
        attackCooldown = 2.5f;
        maxHealth = 80;
        isFaceRight = true;

        firePoint = transform.Find("EnemyFirePoint");

        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet02");

    }

  
    protected override void Attack()
    {
        //检测范围内是否有玩家
        Collider2D hit = Physics2D.OverlapCircle(transform.position,
            attackRange, playerLayer);

        if (hit != null)
        {
            
            _anim.SetTrigger("Attack"); //播放攻击动画
            
            //获取此刻玩家和敌人的方向
            Vector2 dir = (player.position - transform.position).normalized;
            //生成子弹对象
            GameObject bullet = Instantiate(bulletPrefab,
                firePoint.position, Quaternion.identity);
            //初始化子弹
            bullet.GetComponent<Bullet02>().Init(dir, 1);
            
            //播放音效
            MusicManager.Get().CreateMusic("enemy02Attack");
            
            //记录最后一次攻击时间
            lastAttackTime = Time.time;
            
        }
        
        
    }
}
