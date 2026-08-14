using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float moveSpeed; //移动速度
    public float chaseRange; //视线范围


    public float attackRange; //攻击范围
    public float attackCooldown; //攻击冷却
    public int damage; //攻击伤害
    public float lastAttackTime; //最后一次攻击时间


    public int maxHealth; //最大血量
    public float health; // 当前血量
    public bool isDead = false; //是否死亡

    public Transform player; //玩家
    public Rigidbody2D _rb; //刚体
    public LayerMask playerLayer; //玩家层
    public Animator _anim; //动画器

    public bool isFaceRight; //当前脸的朝向 是否朝右

    public bool isBoss = false; //是否为最终boss

   
    protected virtual void Awake()
    {
        player = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //如果死亡直接返回
        if (isDead) return;

        //获取与玩家的距离
        float dist = Vector2.Distance(transform.position,
            player.position);

        //判断是否进入视线
        if (dist <= chaseRange)
        {
            //进入视线, 追逐玩家
            MoveToPlayer();

            //是否进入攻击范围
            if (dist <= attackRange &&
                (Time.time - lastAttackTime) > attackCooldown)
            {
                Attack();
            }
        }
        else
        {
            //超出视线, 无反应
            _rb.velocity = Vector2.zero;
        }
        
        
        //转向
        CheckFace();

    }

    private void CheckFace()
    {
        float dir = player.position.x - transform.position.x;
       
        //如果玩家在右边
        if (dir > 0 && !isFaceRight)
        {
            Flip();
        }
        //玩家在左边 敌人在右边
        else if (dir < 0 && isFaceRight)
        {
            Flip();
        }
    }
    
    //翻转
    private void Flip()
    {
        //x轴取反
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
        
        //标记反转朝向
        isFaceRight = !isFaceRight;

    }


    //攻击
    protected abstract void Attack();


    //移动
    private void MoveToPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        _rb.velocity = dir * moveSpeed;
    }


    //受伤
    public void Hurt(float damage)
    {
        //扣血
        health = Math.Max(health - damage, 0);

        //播放音效
        MusicManager.Get().CreateMusic("enemyHurt");
        
        //判断是否死亡
        if (health <= 0)
        {
            Dead();
        }
    }

    //死亡
    private void Dead()
    {
        if (isDead)
        {
            return;
        }

        //标记死亡
        isDead = true;
        //播放死亡动画(按控制器参数选择: Enemy01用Die, Enemy03用Dead)
        if (System.Array.Exists(_anim.parameters, p => p.name == "Die"))
        {
            _anim.SetTrigger("Die");
        }
        else if (System.Array.Exists(_anim.parameters, p => p.name == "Dead"))
        {
            _anim.SetTrigger("Dead");
        }
        //速度为0
        _rb.velocity = Vector2.zero;
        //关闭碰撞体
        GetComponent<Collider2D>().enabled = false;
        //销毁实体
        Destroy(gameObject, 3f);

        //如果是最终boss
        if (isBoss)
        {
            GameWinPanel.Instance.GameWin();
        }
        
    }
}