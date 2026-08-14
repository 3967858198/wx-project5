using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    
    public int health; //血量值
    public int maxHealth = 8; //最大生命值

    public bool isDead = false; //是否死亡

    public float invincibleTime = 2; //受伤后无敌时间
    // public float invincibleTimer = 0; //受伤计时器
    public bool isInvincible = false; //当前是否无敌

    private void Awake()
    {
        Instance = this; 
        health = maxHealth;
    }

    private void Update()

    {
        if (isDead)
        {
            return;
        }
    }


    //攻击

    //受伤
    public void Hurt(int damage)
    {
        //如果当前是无敌
        if (isInvincible)
        {
            return;
        }
        
        
        //扣血
        health = Math.Max(health - damage, 0);
        //更新血量ui
        HudPanel.Instance.UpdateHealthUI(health); 
        //红晕
        FindObjectOfType<BloodFlashEffect>()?.Flash();
        //开始闪烁
        GetComponent<FlickFX>().TriggerFlash();
        
        //播放音效
        MusicManager.Get().CreateMusic("playerHurt");
        
        //判断是否死亡
        if (health <= 0)
        {
            Dead();
            return;
        }


        isInvincible = true;
        StartCoroutine(Invincible());


    }
    
    //无敌冷却
    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    //死亡
    private void Dead()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        
        
        GameFailPanel.Instance.GameFail();
        
        
    }
    
    public int currentKeyCount = 0; //钥匙数量
    
    //添加钥匙
    public void AddKey(int i)
    {
        currentKeyCount += i;
        
        HudPanel.Instance.UpdateKeyUI(currentKeyCount);
        
    }

    public int currentCoin = 100 ; //当前金币值
    
    //添加金币
    public void AddCoin(int i)
    {
        currentCoin += i;
        HudPanel.Instance.UpdateMoneyUI(currentCoin);
    }
    
    
    
    //回复血量
    public void AddHp(int hp)
    {
        health = Math.Min(health+hp, maxHealth);

        HudPanel.Instance.UpdateHealthUI(health);

    }
    
    //能否吃血瓶
    public bool CanPotion()
    {
        return health < maxHealth;
    }
}