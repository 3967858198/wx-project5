using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch_ExampleA : MonoBehaviour
{
    private void Start()
    {
        EventDispatch.GetInstance().Subscribe(DefineProject_Editor.Event_ID.EDT_System_TestA, OnTestA);
    }

    private void OnDestory()
    {
        EventDispatch.GetInstance().UnSubscribe(DefineProject_Editor.Event_ID.EDT_System_TestA, OnTestA);
    }

    private void OnTestA(object data)
    {
        //Tool_DebugView.GetInstance().Log("EventDispatch_ExampleA-" + "TestA，接收到消息，可在这里执行相关逻辑");
    }

}
