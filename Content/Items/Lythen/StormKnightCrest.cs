using Terraria.ID;
using Terraria.ModLoader;

namespace EndlessEscapade.Content.Items.Lythen;

[AutoloadEquip(EquipType.Head)]
public class StormKnightCrest : ModItem
{
    public override void SetDefaults() {
        Item.width = 26;
        Item.height = 28;
    }
    
    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient<LythenBar>(3)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
