using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet03 : MonoBehaviour
{
    public int damage = 2; //攻击力
    public BoxCollider2D box; //攻击触发器
    public Animator _anim; //动画器

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;

        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    //攻击完成后 关闭动画器
    
    
    //打开触发器
    public void OpenCollier()
    {
        box.enabled = true;
        _anim.enabled = true;

        
    }
    
    //攻击完成, 关闭动画器, 用于动画事件
    public void CloseAnim()
    {
        _anim.enabled = false;
    }
 
    
    //关闭触发器
    public void CloseCollier()
    {
        box.enabled = false;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //伤害玩家
            col.GetComponent<Player>().Hurt(damage);

            //伤害之后 关闭碰撞器
            CloseCollier();
        }
    }
    
    
    
}
