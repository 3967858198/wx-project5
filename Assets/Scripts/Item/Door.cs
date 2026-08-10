using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //进入区域
    void OnTriggerEnter2D(Collider2D col)
    {
        string colName = col.gameObject.name;
        if(colName == "Player")
        {
            //Debug.Log("dddddddddd");
            col.gameObject.transform.localPosition = new Vector3(1.676f, 8.0010004f, 0);
        }
    }

    //离开区域
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
