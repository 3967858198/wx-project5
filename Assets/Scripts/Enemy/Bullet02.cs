using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet02 : MonoBehaviour
{
    public float speed = 1.5f; //速度
    public float lifeTime = 2f;  //存活时间
    public int damage = 1; //伤害值
    public Vector2 dir;  //方向

    public void Init(Vector2 dir, int damage)
    {
        this.damage = damage;

        this.dir = dir.normalized;
        
        //设置子弹朝向
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
        
        //自动销毁
        Destroy(gameObject, lifeTime);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //如果碰到玩家
        if (col.CompareTag("Player"))
        {
            //给玩家造成伤害
            col.GetComponent<Player>().Hurt(damage);
            
            //直接销毁
            Destroy(gameObject);
        }
    }
}
