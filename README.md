
# WechatBakTool
基于C#开发的微信聊天记录备份分析工具，努力做最好用的微信备份工具。

- 理论支持64位版本所有微信，支持两种方式非直接地址获取Key[1]
- 工作区概念，支持多微信切换操作。
- 支持导出Html文件，TXT文件，支持批量导出
- 支持聊天频率分析，全消息库内容搜索
- 目前支持以下类型消息解析
- [x] 文本消息
- [x] 图片
- [x] 语音
- [x] 分享链接
- [x] 群聊
- [x] 系统消息
- [x] 文件
- [x] 表情(需要预下载)

如果有什么好的建议或意见，或者遇到什么问题，欢迎提issue，看到会回。

> [!NOTE]
> 反馈群：815054692<br/>
> 如果觉得不错，欢迎右上角点个star！这是对作者的鼓励，谢谢！<br/>
<br/>

### 免责声明
**本项目仅供学习使用,严禁商业使用**<br/>
**本项目完全免费，问你要钱的都是骗子**<br/>
**使用本项目初衷是作者研究微信数据库的运行使用，您使用本软件导致的后果，包含但不限于数据损坏，记录丢失等问题，作者不承担相关责任。**<br/>
**因软件特殊性质，请在使用时获得微信账号所有人授权。**<br/>
<br/>

### 隐私声明
**本项目不会上传任何你的数据至任何第三方系统**<br/>
**如果发生任何回传行为，请检查是否为第三方修改版本**<br/>
<br/>

### 近期开发规划
本项目技术栈为：
C# + .NET6.0 + WPF <br/>
- [x] ~~新版本UI界面开发~~
- [x] 完善各类消息支持（已经初步完成）
- [x] ~~词云~~
- [ ] 性能优化
- [ ] 打包资源文件夹
- [ ] 手动模式(合适离线分析)
<br/>

### 部分问题
Q：支持手机端吗<br/>
A：<b>在手机端</b>使用迁移功能即可，路径：我->设置->聊天->聊天记录迁移与备份->迁移<br/>
<br/>
Q：怎么导出全部的记录<br/>
A：工作区->右键->管理，就见了。<br/>
<br/>

### 使用说明
**本说明为新版本说明，即将发版**<br/>
0.安装.NET Desktop Runtime(如已经安装忽略)<br/>
1.打开微信，并登录。<br/>
2.在软件左侧下方点击**新建工作区**，<br/>
3.在**新建工作区界面**，选择要创建工作区的微信进程，并**确认下方微信号是否正确**<br/>
4.解密方式**推荐选择用户名推断查找**！该方式理论支持所有64位版本微信。**但该模式需要确保微信账号正确**<br/>
5.新手请忽略其他选项，直接**点击创建工作区**，程序会自动进行工作区创建、解密。<br/><br/>
**工作区创建完毕，点击左侧工作区，尽情使用吧！**<br/>
<br/>

### 参考/引用
项目在开发过程中参考了以下项目或资料，有引用相关代码，如有需要，推荐您可以去参考下相关资料：

1. C#使用OpenSSL解密微信数据库，这里注意一下64位适配问题，注意dll引用： [Mr0x01/WXDBDecrypt.NET](https://github.com/Mr0x01/WXDBDecrypt.NET)<br/>
2. C#使用地址获取微信Key： [AdminTest0/SharpWxDump](https://github.com/AdminTest0/SharpWxDump)
3. 解密微信语音，我是直接调用解密，反正都要ffmpeg，多一个也是多，多两个也是多，懒得头铁实现： [kn007/silk-v3-decoder](https://github.com/kn007/silk-v3-decoder)
4. 解密微信图片 [吾爱破解chenhahacjl/微信 DAT 图片解密 （C#）](https://www.52pojie.cn/forum.php?mod=viewthread&tid=1507922)
5. 参考了句柄名称实现，注意获取句柄别看这里，#10 这个issue就是血泪 [huiyadanli/RevokeMsgPatcher](https://github.com/huiyadanli/RevokeMsgPatcher)
6. 参考了句柄获取 [FuzzySecurity/Sharp-Suite](https://github.com/FuzzySecurity/Sharp-Suite)

### 其他声明
[1] 理论支持所有64位版本指用户名推断和公钥头推断，地址直接获取方式需要version.json支持，更新不是很及时。
