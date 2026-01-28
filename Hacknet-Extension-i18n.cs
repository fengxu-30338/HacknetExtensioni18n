using BepInEx;
using BepInEx.Hacknet;
using BepInEx.Logging;
using Hacknet;
using Hacknet.Extensions;
using Hacknet_Extension_i18n.Patch;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hacknet_Extension_i18n
{
    [BepInPlugin(ModGUID, ModName, ModVer)]
    public class HacknetExtensioni18n : HacknetPlugin
    {
        public const string ModGUID = "HacknetExtensioni18n";
        public const string ModName = "Hacknet-Extension-i18n";
        public const string ModVer = "1.0.1";

        public static ManualLogSource Logger { get; private set; }

        public override bool Load()
        {
            Logger = Log;
            FollowSystemLanguagePatch.Init();
            HarmonyInstance.PatchAll(typeof(HacknetExtensioni18n).Assembly);
            ReloadCurrentExtensionInfo();
            return true;
        }

        public override bool Unload()
        {
            FollowSystemLanguagePatch.DeInit();
            return base.Unload();
        }


        private async void ReloadCurrentExtensionInfo()
        {
            Logger.LogInfo("Start Reload Current ExtensionInfo.");
            var mainMenu = Game1.getSingleton().sman.GetScreens().OfType<MainMenu>().FirstOrDefault();
            if (mainMenu == null)
            {
                Log.LogError("Get MainMenu Failed.");
                return;
            }

            var extensions = mainMenu.extensionsScreen.Extensions;
            var actIdx = extensions.FindIndex(extInfo => extInfo == ExtensionLoader.ActiveExtensionInfo);
            if (actIdx < 0)
            {
                Log.LogError("Find ExtensionLoader from extensionsScreen failed.");
                return;
            }

            var newExt = ExtensionInfo.ReadExtensionInfo(extensions[actIdx].FolderPath);
            extensions[actIdx] = newExt;
            ExtensionLoader.ActiveExtensionInfo = newExt;
            await Task.Delay(50).ConfigureAwait(true);
            mainMenu.extensionsScreen.ExtensionInfoToShow = newExt;
            Log.LogInfo("Reload Current ExtensionInfo Success.");
        }

    }
}
