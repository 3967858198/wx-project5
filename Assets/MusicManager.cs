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
    
    //确保实例存在: 场景中没有MusicManager时自动创建, 便于单独打开任意关卡场景测试
    public static MusicManager Get()
    {
        if (Instance == null)
        {
            var go = new GameObject("MusicManager");
            go.AddComponent<MusicManager>();
        }
        return Instance;
    }
    
    private void Awake()
    {
        //单例去重: 场景切换时已有实例则销毁自身
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this; 
        
        DontDestroyOnLoad(gameObject);
        
        //Inspector已配置的clip优先, 否则从Resources自动加载(Assets/Resources/Prefabs/Music/)
        dic.Clear();
        dic.Add("enemy01Attack", ResolveClip(enemy01Attack, "Prefabs/Music/砍人"));
        dic.Add("enemy02Attack", ResolveClip(enemy02Attack, "Prefabs/Music/魔法弹"));
        dic.Add("enemy03Attack", ResolveClip(enemy03Attack, "Prefabs/Music/喷火"));
        dic.Add("enemyHurt", ResolveClip(enemyHurt, "Prefabs/Music/怪物受伤"));
        dic.Add("playerHurt", ResolveClip(playerHurt, "Prefabs/Music/玩家受伤"));
        dic.Add("playerAttack", ResolveClip(playerAttack, "Prefabs/Music/玩家射击"));
        dic.Add("playerReload", ResolveClip(playerReload, "Prefabs/Music/玩家换弹"));
        dic.Add("eatBottle", ResolveClip(eatBottle, "Prefabs/Music/回血"));
        dic.Add("openBox", ResolveClip(openBox, "Prefabs/Music/开箱子"));
        dic.Add("buttonClick", ResolveClip(buttonClick, "Prefabs/Music/按钮"));
        dic.Add("buttonHover", ResolveClip(buttonHover, "Prefabs/Music/菜单"));
    }
    
    //Inspector的clip为空时从Resources加载
    private AudioClip ResolveClip(AudioClip inspectorClip, string resourcePath)
    {
        if (inspectorClip != null)
        {
            return inspectorClip;
        }
        return Resources.Load<AudioClip>(resourcePath);
    }

    //创建音效
    public void CreateMusic(string name)
    {
        //音效未配置时直接返回, 避免空引用
        if (string.IsNullOrEmpty(name) || !dic.ContainsKey(name) || dic[name] == null)
        {
            return;
        }
        
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
