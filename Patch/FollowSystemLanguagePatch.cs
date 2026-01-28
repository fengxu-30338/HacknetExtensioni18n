using Hacknet.Localization;
using HarmonyLib;
using Pathfinder.Replacements;
using Pathfinder.Util.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hacknet;
using Hacknet.Extensions;
using static Pathfinder.Replacements.ExtensionInfoLoader;

namespace Hacknet_Extension_i18n.Patch
{
    [HarmonyPatch]
    public static class FollowSystemLanguagePatch
    {
        private static readonly List<string> supportFollowSystemLanguages = new List<string>();

        public static void Init()
        {
            ExtensionInfoLoader.RegisterExecutor<CustomerExtensionInfoExecutor>("HacknetExtension.SupportFollowSystemLanguages", ParseOption.ParseInterior);
        }

        public static void DeInit()
        {
            supportFollowSystemLanguages.Clear();
        }

        [HarmonyPatch(typeof(LocalizedFileLoader), nameof(LocalizedFileLoader.GetLocalizedFilepath))]
        [HarmonyPostfix]
        private static void FixGetLocalizedFilepath(ref string __result)
        {
            if (ExtensionLoader.ActiveExtensionInfo == null)
            {
                return;
            }

            var dir = Path.GetDirectoryName(__result);
            var filename = Path.GetFileNameWithoutExtension(__result);
            var ext = Path.GetExtension(__result);
            var localeFile = $"{dir}/{filename}@{Settings.ActiveLocale}{ext}";
            if (File.Exists(localeFile))
            {
                HacknetExtensioni18n.Logger.LogInfo($"Get File[{__result}] locale version -> {localeFile}");
                __result = localeFile;
                return;
            }

            var absFolder = Path.GetDirectoryName(__result);
            if (absFolder == null)
            {
                return;
            }
            localeFile = Path.Combine(absFolder, "Locale", Settings.ActiveLocale, Path.GetFileName(__result));
            if (File.Exists(localeFile))
            {
                HacknetExtensioni18n.Logger.LogInfo($"Get File[{__result}] locale version -> {localeFile}");
                __result = localeFile;
                return;
            }

        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(LocaleActivator), nameof(LocaleActivator.ActivateLocale))]
        private static bool PrefixActivateLocale(ref string localeCode)
        {
            if (supportFollowSystemLanguages.Any(Settings.ActiveLocale.StartsWith))
            {
                localeCode = Settings.ActiveLocale;
                HacknetExtensioni18n.Logger.LogInfo($"Change Extension Languages to System: {localeCode}");
            }

            return true;
        }

        private class CustomerExtensionInfoExecutor : ExtensionInfoExecutor
        {
            public override void Execute(EventExecutor exec, ElementInfo info)
            {
                if (ExtensionInfo.FolderPath != ExtensionLoader.ActiveExtensionInfo.FolderPath)
                {
                    return;
                }
                supportFollowSystemLanguages.Clear();
                supportFollowSystemLanguages.AddRange(info.Content.Split(Hacknet.Utils.commaDelim, StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }

}
