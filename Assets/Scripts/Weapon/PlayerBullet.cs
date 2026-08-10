using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 2f; //子弹的速度
    public Vector2 direction; //子弹的方向

    public float lifeTime = 2f; //生存时间
    public float damage; //伤害值 
    public bool isCritical = false; //是否暴击
    
    //初始化方向 
    public void Init(Vector2 dir, float damage, bool isCritical)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        direction = dir; 
        
        
        //朝射击方向旋转
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0,angle );
        
        
        // 2秒后自动销毁
        Destroy(gameObject, lifeTime);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //子弹移动
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        
        
    }

    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        //子弹撞到敌人
        if (col.CompareTag("Enemy"))
        {
            //调用敌人的受伤函数
            col.GetComponent<EnemyBase>()?.Hurt(damage);
            
            //创建伤害文字 
            DamageTextSpawner.Instance.SpawnDamageText(damage, transform.position, isCritical );
            
            //创建特效
            BulletEffectManager.Instance.Hit_Enemy(transform.position);
            
            
            //销毁
            Destroy(gameObject);
            
        }

        //子弹撞墙
        if (col.CompareTag("Wall"))
        {
            //创建特效
            BulletEffectManager.Instance.Wall_Smoke(transform.position);
            
            //销毁
            Destroy(gameObject);

        }
        
        
    }
}
