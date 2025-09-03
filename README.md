# HacknetExtensioni18n

hacknet扩展国际化工具

您需要安装[Pathfinder](https://github.com/Arkhist/Hacknet-Pathfinder)后才可使用本模组本模组需要安装`PathFinder`

# 用法

> 属性国际化

```xml
<Computer
    name="#PLAYERNAME# Workstation" 
    name-zh="#PLAYERNAME# 的工作站"
    name-ko="韩文"
    />
```

对于需要国际化的属性，加上-xx后缀即可，

支持的后缀有：

* en (英文)
* de (德语)
* fr   (法语)
* ru  (俄语)
* es  (西班牙语)
* ko  (韩语)
* ja  (日语)
* zh  (中文简体)


> 内容国际化

```xml
<file path="home" name="test">
        filecontent
        <locale-zh>
            文件内容
        </locale-zh>
        <locale-ko>
            韩文
        </locale-ko>
</file>
```

在需要国际化的部分用locale-xx标签表示对应语言的实际内容即可。



以上内容均在Pathfinder读取前替换到实际内容，并会删除掉国际化相关标签/内容，即您在游戏中是获取不到name-zh这类属性的。
