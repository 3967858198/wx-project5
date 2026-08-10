using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour
{
    public bool isNearPlayer = false; //玩家是否靠近
    public GameObject Tip; //提示E

    private void Awake()
    {
        Tip = transform.GetChild(0).gameObject; 
        Tip.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            GoNextLevel();
        }
    }
    
    //去下一关
    private void GoNextLevel()
    {
        Debug.Log("进入下一关");
        SceneManager.LoadScene(3);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isNearPlayer = true;
            Tip.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            Tip.SetActive(false);
        }
    }
    
}
