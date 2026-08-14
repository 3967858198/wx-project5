using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFailPanel : BasePanel
{
    public static GameFailPanel Instance;

    public Button _againButton; //重来按钮
    public Button _exitButton; //退出按钮

    private void Awake()
    {
        Instance = this;

        Init();

        _againButton = transform.Find("AgainButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //重开
        _againButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2);
            //切换指针为ui
            GameManager.Get().SetShootCursor();
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

    //游戏失败
    public void GameFail()
    {
        OpenPanel();
        Time.timeScale = 0;

    }
}
