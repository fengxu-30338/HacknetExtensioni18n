# HacknetExtensioni18n

hacknet扩展国际化模组

[English Version](README_en.md)

您需要安装[Pathfinder](https://github.com/Arkhist/Hacknet-Pathfinder)后才可使用本模组本模组需要安装`PathFinder`

# 用法

将dll文件放到扩展的Plugins目录下即可生效。

## 属性国际化

```xml
<Computer
    name="#PLAYERNAME# Workstation" 
    name-zh="#PLAYERNAME# 的工作站"
    name-ko="韩文"
    />
```

对于需要国际化的属性，加上-xx后缀即可，

支持的后缀有：

- en (英文)
- de (德语)
- fr   (法语)
- ru  (俄语)
- es  (西班牙语)
- ko  (韩语)
- ja  (日语)
- zh  (中文简体)

## 内容国际化

```xml
<file path="home" name="test">
        filecontent
        <locale-zh>文件内容</locale-zh>
        <locale-ko>너와 걷다 </locale-ko>
</file>
```

在需要国际化的部分用locale-xx标签表示对应语言的实际内容即可。

以上内容均在Pathfinder读取前替换到实际内容，并会删除掉国际化相关标签/内容，即您在游戏中是获取不到name-zh这类属性的。

## 文件国际化

对于在hacknet中用到的文件提供了两种国际化方式


1. 在文件名加上@locale后缀

```text
// 比如文件叫:
1.txt 
// 英文国际化后的文件名为:
1@en-us.txt
// 中文国际化后的叫
1@zh-cn.txt

// 游戏再读取1.txt时会自动根据系统设置的语言调用对应的国际化文件
```


2. 在文件所在的目录新建Locale/[en-us]目录，在该目录下放置国际化后的文件

```text
// 比如以下文件：
TheCageInside\HackerScripts\CHackK.txt
TheCageInside\HackerScripts\CHackPlayer.txt
TheCageInside\HackerScripts\Locale
TheCageInside\HackerScripts\Locale\en-us
TheCageInside\HackerScripts\Locale\en-us\CHackK.txt
TheCageInside\HackerScripts\Locale\en-us\CHackPlayer.txt

// TheCageInside\HackerScripts\CHackK.txt的英文国际化文件文件在TheCageInside\HackerScripts\Locale\en-us\下的同名文件
```

# Extension跟随系统语言

1.您需要设置扩展支持的所有语言类型

```xml
<!-- 扩展根目录下的ExtensionInfo.xml-->
<HacknetExtension>
	<Language>en-us</Language>
	<Name>ext name</Name>
	<AllowSaves>true</AllowSaves>
    <!-- 必须填写，表示当前可跟随系统语言设置的所有语言类型，多种用','隔开
		English : en-us
        German : de-de
        French : fr-be
        Russian : ru-ru
        Spanish : es-ar
        Korean : ko-kr
        Japanese : ja-jp
        Chinese, simplified : zh-cn
	-->
  	<SupportFollowSystemLanguages>zh-cn,en-us</SupportFollowSystemLanguages>
</HacknetExtension>
```

当您配置了SupportFollowSystemLanguages内容后，当游戏切换语言时，会自动读取扩展内对应的语言部分，不过不存在则会使用Language标签配置的语言。

# AI自动翻译

对于所有xml文件您可以使用AI配合提示词自动翻译，推荐提示词如下：

```text
将Actions文件夹及其子目录(深度遍历)下所有xml所有文件中所有用到中文的地方翻译为英文（只要不包含中文就不用管），忽略xml标签名，属性名，注释，要求：
翻译后的英文不能直接替换原来的中文，你需要识别中文出现的位置，
1.中文如果出现在xml元素的属性中，你应该增加一个属性并加上-en后缀，将翻译后的英文放到该属性中，比如原属性为name那么修改后属性名为name-en
2.中文如果出现在xml元素的内容中，你应该在当前元素的内容末尾处追加一个locale-en元素并把翻译的英文放到该元素中
3.如果元素内容中出现中英等多种语言包含的情况，将他们统一翻译为英文并且保持格式不变在元素内容末尾处追加locale-en元素并把翻译加过放到该元素中
4.重构当前xml文档中翻译过的元素，使得该元素的所有属性独占一行
5.每翻译一个文件需要询问我是否满意才可处理下一个文件
```

# 编辑器提示

为了您的翻译体验，我建议您使用Visual Studio Code编辑器，因为它支持XML文件的语法高亮和智能提示。

您可以在Visual Studio Code中安装以下插件来获得更好的翻译体验：

- XML Tools: 提供XML文件的语法高亮和智能提示
- [HacknetExtensionHelper](https://marketplace.visualstudio.com/items?itemName=fengxu30338.hacknetextensionhelper): 提供Hacknet扩展相关的智能提示

如果您安装的HacknetExtensionHelper插件版本大于等于`0.3.1`，您可以在扩展根目录的Hacknet-EditorHint.xml文件中通过`Include`标签引用本Mod的[提示文件](.CodeHint/Hacknet-i18n.xml)

```xml
<!-- 扩展根目录下的Hacknet-EditorHint.xml-->
<HacknetEditorHint>
    <Include path=".CodeHint/Hacknet-i18n.xml" />
</HacknetEditorHint>
```


