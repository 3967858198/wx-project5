using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    public static ShopPanel Instance;

    public bool isShow = false; //商店面板是否打开
    public Button _byeButton; //关闭按钮
    public Button _buyBottleButton; //购买血瓶
    public Button _buyKeyButton; //购买钥匙

    public int keyMoney = 20; //购买钥匙的钱
    public int bottleMoney = 20; //购买血瓶的钱

    private void Awake()
    {
        Instance = this;

        Init(); //父类方法 初始化canvasgroup
        
        
        _byeButton = GameObject.Find("ByeButton").GetComponent<Button>();
        _buyBottleButton = GameObject.Find("BuyBottleButton").GetComponent<Button>();
        _buyKeyButton = GameObject.Find("BuyKeyButton").GetComponent<Button>();

    }
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        //监听关闭按钮
        _byeButton.onClick.AddListener(() =>
        {
            CloseShop();
        });
        
        //监听购买钥匙
        _buyKeyButton.onClick.AddListener(() =>
        {
            Player.Instance.AddKey(1); //钥匙增加
            Player.Instance.AddCoin(-keyMoney); //金币减少
        });
        
        //购买血瓶
        _buyBottleButton.onClick.AddListener(() =>
        {
            Player.Instance.AddHp(2); //血量增加
            Player.Instance.AddCoin(-bottleMoney); //金币减少
        });
        
        
    }
    
    //打开商店
    public void OpenShop()
    {
        if (isShow)
        {
            return;
        }
        
        
        isShow = true;
        OpenPanel();
        
        //判断是否有钱购买
        if (Player.Instance.currentCoin < keyMoney)
        {
            _buyKeyButton.interactable = false; //没钱禁止交互
        }
        else
        {
            _buyKeyButton.interactable = true; //有钱可以交互
        }
        if (Player.Instance.currentCoin < bottleMoney)
        {
            _buyBottleButton.interactable = false; //没钱禁止交互
        }
        else
        {
            _buyBottleButton.interactable = true; //有钱可以交互
        }
        
        
    }
    
    //关闭商店
    private void CloseShop()
    {
        isShow = false;
        ClosePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
