using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Items.Materials
{
    public class BrokenDuelistsBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Duelist's Blade");
            ItemID.Sets.SortingPriorityMaterials[item.type] = 59; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 0, 18, 0);
            item.rare = ItemRarityID.LightRed;
            item.useAnimation = 15;
            item.material = true;
        }
    }
}