using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HudPanel : MonoBehaviour
{
    public static HudPanel Instance; 
    
    //金币
    public Text _coinText;

    //钥匙
    public Text _keyText;
    
    //血量
    public GameObject heart_full;
    public GameObject heart_half;
    public GameObject heart_empty;
    public Transform _health; //布局容器

    //子弹
    public Text _gunText;
    
    
    //枪的震动
    public RectTransform guiUI;
    public float shakeDuration = 0.1f; //震动时间0.1f
    public float shakeStrength = 3f; //震动强度
    private float currentShakeTimer = 0; //当前震动时间定时器
    public Vector2 originalGunPos; //ui的初始位置
   
    

    private void Awake()
    {
        Instance = this; 
        _coinText = GameObject.Find("CoinText").GetComponent<Text>();
        _keyText = GameObject.Find("KeyText").GetComponent<Text>();
        _gunText = GameObject.Find("GunText").GetComponent<Text>();

        _health = GameObject.Find("Health").transform;
        
        guiUI = GameObject.Find("GunIcon").transform as RectTransform;
        originalGunPos = guiUI.anchoredPosition;


    }

    void Start()
    {
        //开局就要初始化ui
        UpdateMoneyUI(Player.Instance.currentCoin);
        //开局就要初始化ui
        UpdateKeyUI(Player.Instance.currentKeyCount);
        //开局就要初始化ui
        UpdateHealthUI(Player.Instance.health);
        //开局就要初始化ui
        UpdateBulletUI( Weapon.Instance.currentAmmo,  Weapon.Instance.clipSize );
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentShakeTimer > 0 )
        {
            Vector2 offset = Random.insideUnitCircle * shakeStrength;
            guiUI.anchoredPosition = originalGunPos + offset;


                currentShakeTimer -= Time.deltaTime;
        }
        
        
    }
    
    
    
    //更新子弹UI
    public void UpdateBulletUI(int currentAmount, int maxAmount, bool isShake = false)
    {
        _gunText.text = currentAmount + "/" + maxAmount;

        //需要震动
        if (isShake)
        {
            currentShakeTimer = shakeDuration;
        }
    }

    //更新血条
    public void UpdateHealthUI(int hp)
    {
        //满 满 满 满->半  
        
        //清空所有心
        if (_health.childCount > 0)
        {
            //存放子对象的列表
            List<Transform> childs = new();
            //遍历 布局中的所有心
            for (int i = 0; i < _health.childCount; i++)
            {
                childs.Add(_health.GetChild(i));
            }
            
            //删除所有心
            foreach (var t in childs)
            {
                Destroy(t.gameObject);
            }
            
            childs.Clear();
            
        }
        
        
        //计算心的数量
        int count = Player.Instance.maxHealth / 2;

        int full = hp / 2;  //满心数量
        int half = hp % 2;  //半心
        int empty = count - full - half; //空心数量

        
        for (int i = 0; i < full; i++)
        {
            Instantiate(heart_full, _health);
        }
        
        for (int i = 0; i < half; i++)
        {
            Instantiate(heart_half, _health);
        }
        
        for (int i = 0; i < empty; i++)
        {
            Instantiate(heart_empty, _health);
        }

    }

    //更新钥匙UI
    public void UpdateKeyUI(int key)
    {
        _keyText.text = key.ToString();
    }

    //更新金币ui
    public void UpdateMoneyUI(int coin)
    {
        _coinText.text = coin.ToString();
    }

 
}