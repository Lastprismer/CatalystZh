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
            SetLanguage_GameCulture += Fix;
            GetFriendlyName += TranslatedFriendlyName;
            Main.QueueMainThreadAction(() =>
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            });
            if (ModLoader.TryGetMod("CatalystMod", out Mod Catalyst))
            {
                if(Catalyst.TryFind("AstrageldonRelic",out ModItem AstrageldonRelic))
                {
                    AstrageldonRelic.DisplayName.SetDefault("末世星史莱姆圣物");
                }
            }
        }
        public override void Unload()
        {
            Main.QueueMainThreadAction(() =>
            {
                Thread.CurrentThread.CurrentCulture = Language.ActiveCulture.CultureInfo;
            });
            if (ModLoader.TryGetMod("CatalystMod", out Mod Catalyst))
            {
                if (Catalyst.TryFind("AstrageldonRelic", out ModItem AstrageldonRelic))
                {
                    AstrageldonRelic.DisplayName.SetDefault("Astrageldon Relic");
                }
            }
            SetLanguage_GameCulture -= Fix;
            GetFriendlyName -= TranslatedFriendlyName;
        }

        private void Fix(orig_SetLanguage_GameCulture orig, LanguageManager self, GameCulture culture)
        {
            orig.Invoke(self, culture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        private string TranslatedFriendlyName(orig_GetFriendlyName orig, UIKeybindingListItem item)
        {
            string keybindName = item.GetType().GetField("_keybind", (BindingFlags)60).GetValue(item) as string;
            if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) && keybindName == "CatalystMod: Influx Cluster")
            {
                return "灾劫：凝涌星核冲刺";
            }
            return orig.Invoke(item);
        }
    }
}