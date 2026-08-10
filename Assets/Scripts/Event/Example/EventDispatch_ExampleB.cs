using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch_ExampleB : MonoBehaviour
{    
    private void Start()
    {
        EventDispatch.GetInstance().Subscribe(DefineProject_Editor.Event_ID.EDT_System_TestB, OnTestB);
    }

    private void OnDestory()
    {
        EventDispatch.GetInstance().UnSubscribe(DefineProject_Editor.Event_ID.EDT_System_TestB, OnTestB);
    }

    private void OnTestB(object data)
    {
        //Tool_DebugView.GetInstance().Log("EventDispatch_ExampleB-" + "TestB，接收到消息，可在这里执行相关逻辑 , 消息带数据 data = " + data);
        if (data is EventExample_TestB eet_B)
        {
            //Tool_DebugView.GetInstance().Log("EventDispatch_ExampleB-" + $"          ----- 数据：aaa={eet_B.aaa} , bbb={eet_B.bbb} , ccc={eet_B.ccc} , ddd={eet_B.ddd}");
        }

    }
}

public class EventExample_TestB
{
    public EventExample_TestB(int a,float b,string c,Vector3 d)
    {
        aaa =a; bbb = b; ccc = c; ddd = d;  
    }
    public int aaa;
    public float bbb;
    public string ccc;
    public Vector3 ddd;
}