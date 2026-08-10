using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinPanel : BasePanel
{
    public static GameWinPanel Instance;

    public Button _menuButton; //菜单按钮
    public Button _exitButton; //退出按钮
    
    
    
    private void Awake()
    {
        Instance = this;

        Init();

        _menuButton = transform.Find("MenuButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //回到主菜单
        _menuButton.onClick.AddListener(() =>
        {
            
            SceneManager.LoadScene(0); 
            //切换指针为ui
            GameManager.Instance.SetUICursor();
        });
        
        //退出游戏
        _exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //游戏胜利
    public void GameWin()
    {
        OpenPanel();
        Time.timeScale = 0;
        
        
    }
    
    
}
