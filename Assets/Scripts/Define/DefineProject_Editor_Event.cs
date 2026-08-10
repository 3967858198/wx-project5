using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 文件说明：代码间的定义
 */
public partial class DefineProject_Editor
{
    #region ---------------------------------- 事件 ID ---------------------------------------------------------------
    public enum Event_ID
    {
        // ================================ 系统，勿动 ================================
        EDT_System_Begin = 1,
        EDT_System_TestA,
        EDT_System_TestB,
        EDT_System_End = 100,
    
        // ================================ 工程增加 ================================
        EDT_Project_Begin = 101,
        // ------------------------ 网络状态
        EDT_Network_Login_Begin,
        EDT_Network_Login_Ok,
        EDT_Network_DisConnect,
        // ------------------------ 玩家同步
        EDT_PlayerSync_Sync,
        EDT_PlayerSync_Add,
        EDT_PlayerSync_Sub,
        EDT_PlayerSync_DataSetLogin,
        EDT_PlayerSync_DataSetWorldIn,
        EDT_PlayerSync_DataPrint,
        // ------------------------场景切换
        EDT_SceneChange_Begin,
        EDT_SceneChange_End,        
        EDT_SceneChange_ChangeLightmap_AddDataOver,
        //
        // ------------------------ 移动区域
        EDT_SceneArea_In,                           // 移动区域，进入（魂空间 - 隐藏）
        EDT_SceneArea_Boundary,                     // 移动区域，显示边界（魂空间 - 隐藏）
        EDT_SceneArea_Out,                          // 移动区域，离开（魂空间 - 显示）
        // ------------------------ GameProcessDefine
        EDT_GameProcessDefine_Process,
     

    }
    #endregion

    #region ---------------------------------- 事件 结构 ------------------------------------------------------------
    #region ----------------------------------------------------------- 玩家同步
    // EDT_PlayerSync_Sync ， EDT_PlayerSync_Add 见 PlayerInfo
    public class EventData__EDT_PlayerSync_DataSetLogin
    {
        public string   Token                        = string.Empty;    
        public string   Addr                         = string.Empty;    
        public string   NickName                     = string.Empty;    
        public long     Cell                         = -1;      
        public string   ClothID                      = string.Empty;
        public int      Sex                          = -1;
        public int      Color                        = -1;          
        public int      Adult                        = -1;               
    }
    public class EventData__EDT_PlayerSync_DataSetWorldIn
    {
        public int      PID                     = -1;                    
        public string   ClothID                 = string.Empty;
        public int      Sex                     = -1;
        public string   NickName                = string.Empty;    
        public string   SceneID                 = string.Empty;    
        public int      LevelID                 = -1;          
        public int      Color                   = -1;            
        public int      Adult                   = -1;           
    }
    
    #endregion
    
    #region ----------------------------------------------------------- 场景切换
    public class EventData__EDT_SceneChange
    {
    }

    #endregion
    
    #region ----------------------------------------------------------- 魂空间
    public class EventData__EDT_SceneArea
    {
    }

    #endregion

    
    #endregion

}
