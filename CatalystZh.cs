using System.Globalization;
using System.Reflection;
using System.Threading;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using static On.Terraria.GameContent.UI.Elements.UIKeybindingListItem;
using static On.Terraria.Localization.LanguageManager;

namespace CatalystZh
{
	public class CatalystZh : Mod
	{
        // From LanguageFix mod and Improve Game
        public override void Load()
        {
            GetFriendlyName += TranslatedFriendlyName;
        }
        public override void Unload()
        {
            GetFriendlyName -= TranslatedFriendlyName;
        }
        private string TranslatedFriendlyName(orig_GetFriendlyName orig, UIKeybindingListItem item)
        {
            string keybindName = item.GetType().GetField("_keybind", (BindingFlags)60).GetValue(item) as string;
            if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) && keybindName == "CatalystMod: Influx Cluster")
            {
                return "灾劫：凝涌星核冲刺";
            }
            else if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) && keybindName == "CatalystMod: Toggle Intergelactic Setbonus Visibility") {
                return "灾劫：开关异宇星凝套星环可见性";
            }
            return orig.Invoke(item);
        }
    }
}