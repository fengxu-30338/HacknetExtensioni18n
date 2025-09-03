using BepInEx;
using BepInEx.Hacknet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacknet_Extension_i18n
{
    [BepInPlugin(ModGUID, ModName, ModVer)]
    public class HacknetExtensioni18n : HacknetPlugin
    {
        public const string ModGUID = "HacknetExtensioni18n";
        public const string ModName = "Hacknet-Extension-i18n";
        public const string ModVer = "1.0.0";

        public override bool Load()
        {
            HarmonyInstance.PatchAll(typeof(HacknetExtensioni18n).Assembly);
            return true;
        }

    }
}
