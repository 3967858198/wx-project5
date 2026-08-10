using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //音效片段
    public AudioClip enemy01Attack; //敌人1攻击音效
    public AudioClip enemy02Attack;  //敌人2攻击音效
    public AudioClip enemy03Attack; //敌人3攻击音效
    public AudioClip enemyHurt;  //敌人受伤
    public AudioClip playerHurt;  //玩家受伤
    public AudioClip playerAttack; //玩家射击 
    public AudioClip playerReload; //玩家换弹 
    public AudioClip eatBottle; //吃血瓶 
    public AudioClip openBox; //开箱子 
    public AudioClip buttonClick; //按钮点击 
    public AudioClip buttonHover;  //按钮移动 

    public Dictionary<string, AudioClip> dic = 
        new Dictionary<string, AudioClip>();

    
    
    public static MusicManager Instance;
    
    private void Awake()
    {
        Instance = this; 
        
        DontDestroyOnLoad(gameObject);
        
        
        dic.Add("enemy01Attack", enemy01Attack);
        dic.Add("enemy02Attack", enemy02Attack);
        dic.Add("enemy03Attack", enemy03Attack);
        dic.Add("enemyHurt", enemyHurt);
        dic.Add("playerHurt", playerHurt);
        dic.Add("playerAttack", playerAttack);
        dic.Add("playerReload", playerReload);
        dic.Add("eatBottle", eatBottle);
        dic.Add("openBox", openBox);
        dic.Add("buttonClick", buttonClick);
        dic.Add("buttonHover", buttonHover);
        
        
        
    }

    //创建音效
    public void CreateMusic(string name)
    {
        //创建对象
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);
        
        //添加播放器组件
        var au = go.AddComponent<AudioSource>();
        
        //尝试 play, 默认打开唤醒时播放, clip是空的, 就不播放了
        
        //设置播放的音效
        au.clip = dic[name];
        //手动播放音效
        au.Play(); 
       
        
        //播放后销毁
        StartCoroutine(DestroyWhenFinished(au));
        
        
    }
    
    //定时销毁音效对象
    private IEnumerator DestroyWhenFinished(AudioSource au)
    {
        //等待au播放完成
        while (au.isPlaying)
        {
            yield return null;
        }
        
        Destroy(au.gameObject);
        
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
