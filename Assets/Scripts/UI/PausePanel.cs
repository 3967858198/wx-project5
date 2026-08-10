using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    public bool isPaused = false; //游戏是否暂停

    public Button _continueButton;   //继续按钮
    public Button _exitButton;   //退出按钮

    private void Awake()
    {

        Init();
        
        _continueButton = transform.Find("ContinueButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //点击了继续游戏
        _continueButton.onClick.AddListener(() =>
        {
            ContinueGame();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    //暂停游戏
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; //停止游戏
        OpenPanel(); //打开面板
    }

    //继续游戏
    public void ContinueGame()
    {
        Time.timeScale = 1; //开始游戏
        ClosePanel(); //打开面板

        isPaused = false;
    }

}
