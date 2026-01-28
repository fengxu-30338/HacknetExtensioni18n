# HacknetExtensioni18n

Internationalization module for Hacknet extensions

You need to install [Pathfinder](https://github.com/Arkhist/Hacknet-Pathfinder) before using this module.

# Usage

Simply place the DLL file in the extension's Plugins directory to activate it.

## Property Internationalization

```xml
<Computer
    name="#PLAYERNAME# Workstation" 
    name-zh="#PLAYERNAME# 的工作站"
    name-ko="너와"
    />
```

For properties that need internationalization, just add the -xx suffix.

Supported suffixes:

- en (English)
- de (German)
- fr (French)
- ru (Russian)
- es (Spanish)
- ko (Korean)
- ja (Japanese)
- zh (Simplified Chinese)

## Content Internationalization

```xml
<file path="home" name="test">
        filecontent
        <locale-zh>文件内容</locale-zh>
        <locale-ko>너와 걷다 </locale-ko>
</file>
```

Use the locale-xx tag to represent the actual content in the corresponding language for parts that need internationalization.

All the above content is replaced with the actual content before Pathfinder reads it, and internationalization-related tags/content are removed. That means you won't be able to get attributes like name-zh in the game.

## File Internationalization

Two methods of internationalization are provided for files used in Hacknet:

1. Add @locale suffix to the filename

```text
// For example, if the file is called:
1.txt 
// The English internationalized filename would be:
1@en-us.txt
// The Chinese internationalized filename would be:
1@zh-cn.txt

// When the game reads 1.txt, it will automatically call the corresponding internationalized file according to the system language setting
```

2. Create a Locale/[en-us] directory in the file's directory and place the internationalized files there

```text
// For example, the following files:
TheCageInside\HackerScripts\CHackK.txt
TheCageInside\HackerScripts\CHackPlayer.txt
TheCageInside\HackerScripts\Locale
TheCageInside\HackerScripts\Locale\en-us
TheCageInside\HackerScripts\Locale\en-us\CHackK.txt
TheCageInside\HackerScripts\Locale\en-us\CHackPlayer.txt

// The English internationalized file for TheCageInside\HackerScripts\CHackK.txt is in the same filename under TheCageInside\HackerScripts\Locale\en-us\
```

# Extension Following System Language

1. You need to set all language types supported by the extension

```xml
<!-- ExtensionInfo.xml in the extension root directory-->
<HacknetExtension>
	<Language>en-us</Language>
	<Name>ext name</Name>
	<AllowSaves>true</AllowSaves>
    <!-- Must be filled in, indicating all language types that can follow the system language settings, separated by commas
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

After you configure SupportFollowSystemLanguages, when the game switches languages, it will automatically read the corresponding language part in the extension. If it doesn't exist, it will use the language configured in the Language tag.

# AI automatic translation

For all XML files, you can use AI to automatically translate them with prompt words. The recommended prompt words are as follows:

```text
Translate all instances of Chinese used in all xml files within the Actions folder and its subdirectories (deep traversal) into English (ignore any text that does not contain Chinese), disregarding xml tag names, attribute names, and comments. Requirements:
The translated English cannot directly replace the original Chinese. You need to identify the position where the Chinese appears,
If Chinese appears in the attributes of an XML element, you should add a new attribute with the "-en" suffix and place the translated English text within that attribute. For example, if the original attribute is "name", the modified attribute name would be "name-en"
2. If Chinese appears in the content of an XML element, you should append a locale-en element at the end of the current element's content and place the translated English within that element
3. If multiple languages such as Chinese and English appear in the element content, translate them all into English and keep the format unchanged. Append the locale-en element at the end of the element content and place the translation within that element
4. Refactor the translated elements in the current XML document so that all attributes of the element are on a separate line
5. Before moving on to the next file, you need to ask me if I am satisfied with the translation of each file
```

# Editor Tips

For a better translation experience, I recommend using Visual Studio Code editor, as it supports syntax highlighting and intelligent suggestions for XML files.

You can install the following plugins in Visual Studio Code to get a better translation experience:

- XML Tools: Provides syntax highlighting and intelligent suggestions for XML files
- [HacknetExtensionHelper](https://marketplace.visualstudio.com/items?itemName=fengxu30338.hacknetextensionhelper): Provides intelligent suggestions related to Hacknet extensions

If you have installed HacknetExtensionHelper plugin version greater than or equal to `0.3.1`, you can reference this Mod's [hint file](.CodeHint/Hacknet-i18n.xml) in the Hacknet-EditorHint.xml file in the extension root directory using the `Include` tag

```xml
<!-- Hacknet-EditorHint.xml in the extension root directory-->
<HacknetEditorHint>
    <Include path=".CodeHint/Hacknet-i18n.xml" />
</HacknetEditorHint>
```