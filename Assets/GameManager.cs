using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    
    public Texture2D uiCursor; //光标纹理, 菜单界面使用
    public Texture2D shootCursor; //准心纹理, 游戏界面使用
    public Vector2 hotspot = new Vector2(7.5f, 7.5f); //偏移量

    //确保实例存在: 场景中没有GameManager时自动创建, 便于单独打开任意关卡场景测试
    public static GameManager Get()
    {
        if (Instance == null)
        {
            var go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
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
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUICursor();

    }
    
    //设置为ui光标
    public void SetUICursor()
    {
        Cursor.SetCursor(uiCursor, Vector2.zero, CursorMode.Auto);
    }
    
    //设置为射击光标
    public void SetShootCursor()
    {
        Cursor.SetCursor(shootCursor, hotspot, CursorMode.Auto);
    }
    
}
