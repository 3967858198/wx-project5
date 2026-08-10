using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayWnd : WindowRoot
{
    public Transform testObjTrans;   //要被轮盘移动的物体
    public float speedMultipler;     //移动的速度
    public float pointDis = 140;     //轮盘的中心点可以移动的距离

    public Image imgTouch;           //轮盘点击的区域 超过这个区域将无法触发轮盘
    public Image imgDirBG;           //方向轮盘的背景
    public Image imgDirPoint;        //轮盘中心的小点
    public Transform ArrowRoot;      //箭头的父级

    private Vector2 startPos = Vector2.zero;      //计算轮盘的位置
    private Vector2 defaultPos = Vector2.zero;    //轮盘的初始位置
    private Vector2 dir;                          //玩家移动的方向（2D）

    private void Start()
    {
        SetActive(ArrowRoot, false);              //关闭箭头
        defaultPos = imgDirBG.transform.position; //记录轮盘的初始位置
        RegisterMoveEvts();                       //调用事件方法
    }

    /// <summary>
    /// 调用 点击   抬起  拖拽的事件方法
    /// </summary>
    void RegisterMoveEvts()
    {
        SetActive(ArrowRoot, false);              //再次关闭箭头

        //按下
        OnClickDown(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            startPos = evt.position;              //存储点击的位置
            GetComponent<CanvasGroup>().alpha = 1;//让轮盘显示出来
            imgDirBG.transform.position = evt.position; //把轮盘移动过去
        });

        //抬起
        OnClickUp(imgTouch.gameObject, (PointerEventData evt, object[] args) =>
        {
            GetComponent<CanvasGroup>().alpha = 0;     //让轮盘消失
            imgDirBG.transform.position = defaultPos;  //把轮盘的位置还原
            SetActive(ArrowRoot, false);               //隐藏箭头
            imgDirPoint.transform.localPosition = Vector2.zero; //把轮盘上的小点的位置清空
            dir = Vector2.zero;                        //清空移动的向量
        });

        OnDrag(imgTouch.gameObject, (PointerEventData evt, object[] args) =>
        {
            Vector2 dir = evt.position - startPos;     //拖拽的位置减去点击的位置等于小点的方向信息
            float len = dir.magnitude;                 //获得小点可以移动的距离

            if (len > pointDis)
            {
                //如果小点可以移动的距离大于了140 
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis); //把移动范围限制在140
                imgDirPoint.transform.position = startPos + clampDir;     //小点的位置等于点击的位置+计算出来的位置
            }
            else
            {
                //如果小点可以移动的距离小于140
                imgDirPoint.transform.position = evt.position; //我们的拖拽的位置如果在范围内，点就移动到对应的位置
            }

            if (dir != Vector2.zero)
            {
                //如果不等于0，那么就代表当前还处于拖拽状态
                SetActive(ArrowRoot, true);    //激活箭头

                //计算位置A和位置B之间的夹角（角度）
                float angle = Vector2.SignedAngle(Vector2.left, dir);

                //设置箭头旋转（2D旋转）
                ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);
            }

            this.dir = dir.normalized;     //把方向规一化，返回的向量值为1
        });
    }

    private void Update()
    {
        PlayerMovement pv = testObjTrans.GetComponent<PlayerMovement>();

        //如果当前有移动方向
        if (dir != Vector2.zero && testObjTrans != null)
        {
            //记录最后一次移动的方向
            pv.lastMoveDir = dir;

            //如果大于0 , 鼠标在玩家右边
            if (dir.x > 0)
            {
                pv.Flip(true);
            }
            else
            {
                pv.Flip(false);
            }

            //播放走路动画
            pv.PlayWalkAnim(true);

            //计算移动距离
            Vector2 movement = dir * Time.deltaTime * speedMultipler;

            //2D移动 - 直接修改Transform的position
            testObjTrans.position += new Vector3(movement.x, movement.y, 0);
        }
        else
        {
            pv.PlayWalkAnim(false);
        }
    }
}