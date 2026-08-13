using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f; //移动速度 
    public Rigidbody2D _rb; //
    public Vector2 movement; //移动向量
    public GameObject _visual; //玩家实体
    public Animator _anim; //动画器

    public bool originFaceRight = true; //图片原本 脸的朝向是否为右
    public bool currentFaceRight = true; //当前脸的朝向是否为右
    public Vector2 lastMoveDir; //最后一次移动的方向


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _visual = GameObject.Find("PlayerVisual");
        _anim = _visual.GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WASD/方向键输入
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");
        
        //防止对角线速度比直线快
        movement.Normalize();

        //判断移动动画
        if (movement != Vector2.zero)
        {
            _anim.SetBool("isWalk", true);
        }
        else
        {
            _anim.SetBool("isWalk", false);
        }
        
        //判断人物朝向
        //获取鼠标的世界坐标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //鼠标位置减去玩家位置x
        float dir = mouseWorldPos.x - transform.position.x;
        //如果大于0 , 鼠标在玩家右边
        if (dir > 0 )
        {
            Flip(true);
        }
        else
        {
            Flip(false);
        }

    }
    
    //角色翻转
    public void Flip(bool faceRight)
    {
        currentFaceRight = faceRight;

        Vector3 scale = _visual.transform.localScale;
        scale.x = MathF.Abs(scale.x) * 
                  (currentFaceRight ? 1 : -1) * (originFaceRight ? 1: -1 );
        _visual.transform.localScale = scale;
    }


    //播放移动动画
    public void PlayWalkAnim(bool isWalk)
    {
        if (isWalk)
        {
            _anim.SetBool("isWalk", true);
        }
        else
        {
            _anim.SetBool("isWalk", false);
        }
    }

    //物理移动
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * moveSpeed * Time.fixedDeltaTime );
    }
}
