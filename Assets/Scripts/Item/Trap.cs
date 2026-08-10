using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator _anim; //动画器
    public bool isNearPlayer = false; //玩家是否靠近

    public bool canHurt = false; //是否可以伤害玩家
    public int damage = 1; //伤害值
    
    

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator  StartAnim()
    {
        yield return new WaitForSeconds(1);
        canHurt = true; //可以伤害人
        
        
        while (true)
        {
            yield return new WaitForSeconds(2); //2秒内 - 刺在最高点
            
            
            _anim.SetBool("isOpen", false); //降下刺
            canHurt = false; //不能伤害人
            yield return new WaitForSeconds(1f);  // 1秒内- 刺降到最低点
            yield return new WaitForSeconds(2f);  //2秒 无伤通过
            

            _anim.SetBool("isOpen", true); //升起刺
            yield return new WaitForSeconds(1f); //1秒内 - 等待刺升到最高点
            canHurt = true; //能伤害人

        }
    }

    // Update is called once per frame
    void Update()
    {
        //对玩家造成伤害
        if (isNearPlayer && canHurt)
        {
            Player.Instance.Hurt(damage);
            // Debug.Log("刺伤害");
        }
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
