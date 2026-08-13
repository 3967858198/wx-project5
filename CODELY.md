

## Codely Structured Memories

### User
- [2026-08-13 05:51:02] MainMenu UI 风格偏好：正文/按钮文字用思源黑体粗体（SourceHanSansSC-Bold SDF，Assets/Fonts/），标题「地牢」用像素风图片（Assets/Sucai/DiyuTitle.png）而非普通字体；用户认为 NotoSansSC-Regular 细体不美观。


### Feedback
- [2026-08-13 05:58:42] 用户要求：每次修改完场景/资源文件后，触发 Unity 编辑器自动重新加载以便直接预览效果。实现机制：Assets/Editor/SceneAutoReload.cs（InitializeOnLoad）监听 .codely-cli/scene_reload_trigger 文件内容变化，变化时 AssetDatabase.Refresh + 重新打开当前场景。触发命令：Set-Content -Path ".codely-cli\scene_reload_trigger" -Value (Get-Date -Format "yyyyMMddHHmmssfff")。注意：TriggerPath 必须用 Application.dataPath 父目录定位项目根（Directory.GetCurrentDirectory 在 Tuanjie 1.9.3 中不是项目根目录）；场景有未保存修改（isDirty）时自动跳过。

### Project
- [2026-08-13 04:59:13] alagard_by_pix3m-d6awiwp.ttf 格式不被 Tuanjie/Unity FreeType 支持，无法导入生成字形纹理。项目中改用 TextMeshProUGUI + alagard_by_pix3m-d6awiwp SDF.asset 替代原始 UI Text 组件。中文回退字体为 NotoSansSC-Regular SDF（Assets/Codely/Fonts/），已加入全局 TMP fallback 列表。
- [2026-08-13 05:48:06] wx-project5 项目用户明确要求用 Tuanjie 1.9.3 (2022.3.62t11) 打开（已成功打开运行）。注意：若出现编译错误（com.unity.instantgame AutoStreamingSettings API 变更）或 TMP 字体加载失败，检查是否需改用 2022.3.61t5。
- [2026-08-13 09:32:35] 关卡流程（Build Settings 索引）：0=Game(第1关) 1=Loading 2=MainMenu 3=Game_1(第2关) 4=Game_2(第3关/Boss通关)。新游戏→Game；Ladder 按场景名串联 Game→Game_1→Game_2（Game_2 梯子触发胜利）；GameFail 重开回 MainMenu(2)。操作键位：WASD 移动、R 换弹、鼠标左键/空格开火、E 互动。
- [2026-08-13 09:32:35] Tuanjie 1.9.3 调试经验：1) 场景中 prefab 实例的 guid 是加密格式（meta 里 base64 与场景引用不一致），不能靠搜 m_Name/guid 判断 prefab 实例是否存在，用编辑器脚本 GameObject.Find/transform.Find 验证；2) SceneAutoReload 触发 CMD 命令（如 CMD:COPY_FIRE_BUTTON）时需先写中间值（时间戳）再写 CMD，因为脚本编译域重载会重置静态 _lastContent；3) 开火按钮跨场景复用方案：FireButton.prefab(Assets/Resources/Prefabs/) + FireButton.cs 运行时绑定 Weapon.Instance，避免场景持久化 onClick 引用断裂。

### Reference

