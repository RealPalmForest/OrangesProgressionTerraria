using Microsoft.Xna.Framework;
using OrangesProgression.Content.Projectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OrangesProgression.Content.Items.Weapons
{
	public class ShroomStaff : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.OrangesProgression.hjson file.

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 34;

            Item.damage = 11;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = 5;
            Item.knockBack = 1;
            Item.value = 982;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SpinMushroomProjectile>();
            Item.noMelee = true;
            Item.shootSpeed = 60;
            Item.mana = 3;
            Item.staff[Type] = true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Mushroom, 23);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}


        // Even Arc style: Multiple Projectile, Even Spread
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(30);

			position += Vector2.Normalize(velocity) * 30f;

			for (int i = 0; i < numberProjectiles; i++) {
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position, perturbedSpeed * 3, type, damage, knockback, player.whoAmI);
			}

            //int dust = Dust.NewDust(position, 40, 38, DustID.Lava, 0f, 0f, 0, default(Color), 1f);
            //Main.dust[dust].velocity *= player.direction;

            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
		}
    }
}