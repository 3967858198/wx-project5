using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public bool isEnterGame = false; //是否进入了主菜单
    public GameObject _splashScreen; //启动画面的文字
    public PlayableDirector timeline; //时间轴

    public Button _newGameButton; //新游戏按钮
    
    
    private void Awake()
    {
        _splashScreen = GameObject.Find("SplashScreen");
        timeline = GameObject.Find("Timeline").GetComponent<PlayableDirector>();

        _newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //新游戏按钮点击
        _newGameButton.onClick.AddListener(() =>
        {
            //进入第一个关卡
            SceneManager.LoadScene(0);
            
            //播放音效
            // MusicManager.Instance.CreateMusic("buttonClick");
        });
    }

    // Update is called once per frame
    void Update()
    {
        //监听任意按键点击 并且第一次进入
        if (Input.anyKeyDown && !isEnterGame)
        {
            //播放timeline动画 , 进入到主菜单
            timeline.Play();
            
            //关闭启动画面文字
            _splashScreen.SetActive(false);
            
            //标记已经进入主菜单
            isEnterGame = true;
        }
    }
}
