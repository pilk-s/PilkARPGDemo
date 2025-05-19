# 二次元风格ARPG游戏Demo

本项目是一款使用 Unity3D 开发的 **二次元风格 ARPG 游戏 Demo**，致力于呈现完整的战斗体验与流畅的动画表现，涵盖了从主菜单到战斗系统的多个关键模块，旨在为未来的商业化项目打下技术基础。

---

## 🎮 项目特性

- ✅ 主菜单 UI 动效与交互
- ✅ 过场动画与剧情驱动
- ✅ 第三人称角色控制系统
- ✅ 动作打击感与 Combo 连击
- ✅ 状态机驱动的 AI 行为
- ✅ 高性能运行时资源管理

---

## 🧱 技术栈

| 技术 | 用途 |
|------|------|
| Unity3D | 核心开发引擎 |
| UGUI + DoTween | UI 动效与交互 |
| Input System | 输入控制，支持第三人称控制 |
| Character Controller | 控制角色运动与碰撞 |
| Cinemachine + Timeline | 过场动画与镜头演出 |
| Animator + Blend Tree | 动画系统与状态切换 |
| Animation Rigging | 动作优化（如持武器姿态） |
| Shader Graph | 拖尾与特效开发 |
| Behavior Tree | AI 行为树控制 |
| NavMesh | AI 导航寻路 |

---

## ✨ 项目亮点

### 🖥️ UI系统

- 构建统一 UI 框架，便于拓展与维护
- 主菜单通过 **UGUI + DoTween** 实现动效与按钮动画反馈

### 🎬 动画演出

- 使用 **Timeline + Cinemachine** 制作开场过场动画
- 相机支持 **防穿透功能**，角色视角流畅控制

### 🕹️ 角色控制与战斗系统

- 使用 **Input System + Character Controller** 实现第三人称角色控制器
- 动画系统基于 **Animator + Blend Tree**
- 引入 **Animation Rigging** 实现真实持武动作
- 战斗系统支持：
  - 锁定切换
  - Combo连击配置（基于 ScriptableObject）
  - 攻击冷却由 **计数管理器** 控制

### 💥 战斗反馈与AI系统

- 攻击与受击通过事件中心驱动 `HealthControl` 处理血量逻辑
- AI 系统采用 **Behavior Tree** 实现状态切换（待机、巡逻、追击、攻击等）
- 使用 **NavMesh** 实现路径导航与动态障碍处理

### 🧠 性能优化

- 攻击残影使用 **Skinned Mesh 快照** 捕捉生成拖尾特效
- 脚步声、攻击音效等运行时资源通过 **对象池管理**，有效减少 GC 开销

---
