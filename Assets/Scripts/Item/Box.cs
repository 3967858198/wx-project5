using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Animator _anim; //动画器
    public bool isNearPlayer = false; //是否靠近玩家

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //玩家靠近 并且按下了E
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            OpenBox();
        }
    }
    
    //开箱
    private void OpenBox()
    {
        //用来禁止多次打开箱子, 箱子只能打开一次
        if (!isNearPlayer)
        {
            return;
        }
        
        //播放开箱动画
        _anim.enabled = true;
        
        //播放音效
        MusicManager.Get().CreateMusic("openBox");
        
        //关闭碰撞器
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        
        //标记箱子已经开过了
        isNearPlayer = false; 


    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}
