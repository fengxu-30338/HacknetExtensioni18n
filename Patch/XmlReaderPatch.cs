using Hacknet;
using Hacknet.Localization;
using HarmonyLib;
using Pathfinder.Util.XML;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hacknet.Extensions;

namespace Hacknet_Extension_i18n.Patch
{
    [HarmonyPatch]
    public class XmlReaderPatch
    {

        private static readonly HashSet<string> supportLocales = new HashSet<string>()
        {
            "en","de","fr","ru","es","ko","ja","zh"
        };

        private static void HandleLocaleAttribute(Dictionary<string, string> attributes)
        {
            foreach (var attributesKey in attributes.Keys.ToList())
            {
                var idx = attributesKey.LastIndexOf("-", StringComparison.Ordinal);
                if (idx < 0)
                {
                    continue;
                }

                var sourceName = attributesKey.Substring(0, idx);
                var locale = attributesKey.Substring(idx + 1);

                if (!supportLocales.Contains(locale))
                {
                    continue;
                }


                if (Settings.ActiveLocale.StartsWith(locale))
                {
                    attributes[sourceName] = attributes[attributesKey];
                }

                attributes.Remove(attributesKey);
            }
        }

        private static void HandleContentLocale(ElementInfo element)
        {
            for (var i = 0; i < element.Children.Count; i++)
            {
                var child = element.Children[i];
                if (!child.Name.StartsWith("locale-"))
                {
                    continue;
                }

                var locale = child.Name.Substring("locale-".Length);

                if (!supportLocales.Contains(locale))
                {
                    continue;
                }

                if (Settings.ActiveLocale.StartsWith(locale))
                {
                    element.Content = child.Content;
                }

                element.Children.RemoveAt(i--);
            }
        }

        private static void HandleLocale(ElementInfo element)
        {
            HandleLocaleAttribute(element.Attributes);
            HandleContentLocale(element);

            foreach (var elementChild in element.Children)
            {
                if (elementChild.Name.StartsWith("locale-"))
                {
                    continue;
                }

                HandleLocale(elementChild);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EventExecutor), "ReadElement")]
        private static bool PostfixReadElement(EventExecutor __instance, Dictionary<string, string> attributes)
        {
            var field = AccessTools.Field(typeof(EventExecutor), "currentElementStack");
            Stack<ElementInfo> stack = (Stack<ElementInfo>)field.GetValue(__instance);
            HandleLocaleAttribute(attributes);

            foreach (var elementInfo in stack)
            {
                HandleContentLocale(elementInfo);
            }
            return true;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(EventExecutor), "ReadEndElement")]
        private static bool PostfixReadEndElement(EventExecutor __instance)
        {
            var field = AccessTools.Field(typeof(EventExecutor), "currentElementStack");
            Stack<ElementInfo> stack = (Stack<ElementInfo>)field.GetValue(__instance);

            foreach (var elementInfo in stack)
            {
                HandleLocale(elementInfo);
            }

            return true;
        }
    }
}
