using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    
    public int hp = 2;//回血数量
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var player = col.GetComponent<Player>();
            
            //判断是否可以吃血瓶
            if ( player.CanPotion())
            {
                
                col.GetComponent<Player>().AddHp(hp);
                
                //播放音效
                MusicManager.Get().CreateMusic("eatBottle");
                
                Destroy(gameObject);
            }
           
           
        }
    }
    
}
