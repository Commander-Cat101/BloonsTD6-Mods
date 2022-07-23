using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Simulation.Towers.Behaviors.Attack;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Assets.Scripts.Unity.Display;
using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Unity.Towers.Behaviors.Attack;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DartMonkey;
using MelonLoader;
using System.Collections.Generic;
using System.Linq;
using Lazy_Monkey.Displays.Projectiles;
using Assets.Scripts.Unity;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Filters;

[assembly: MelonInfo(typeof(TemplateMod.Main), "Snore Monkey", "1.1.0", "Commander__Cat")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace TemplateMod
{
    public class Main : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("The Boomerang Monkey Fell Asleep Again!");
        }
    }

}
namespace Lazy_Monkey.Displays.Projectiles
{
    public class SnoreAttackDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "SnoreAttackDisplay");
        }
    }
}


namespace Lazy_Monkey.Displays
{
    public class SnoreMonkeyBaseDisplay : ModTowerDisplay<LazyMonkey>
    {
        // Copy the Boomerang Monkey display
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey);

        public override bool UseForTower(int[] tiers)
        {
            return tiers.Max() < 5;
        }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            // Print info about the node in order to edit it easier

            node.SaveMeshTexture();


            // Set our custom texture

            // Make it not hold the Boomerang
            node.RemoveBone("SuperMonkeyRig:Dart");

            SetMeshTexture(node, "SnoreMonkeyBaseDisplay");
        }
    }
}
namespace Lazy_Monkey.Displays.Tier5
{
    public class SnoreMonkey005Display : ModTowerDisplay<LazyMonkey>
    {
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey, 0, 0, 0);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[2] == 5;
        }

        public override float Scale => 1.1f;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            //node.SaveMeshTexture(); used this to get the texture to edit
            //node.PrintInfo(); used this to get the bone names and other info

            node.RemoveBone("SuperMonkeyRig:Dart");  // remove the boomerang from his hand

            SetMeshTexture(node, "SnoreMonkey005Display");  // Name in this case is just 'TFDisplay' so it will find 'TFDiplay.png'
        }
    }
    public class SnoreMonkey050 : ModTowerDisplay<LazyMonkey>
    {
        // Copy the Boomerang Monkey display
        public override string BaseDisplay => GetDisplay(TowerType.DartlingGunner, 0, 4, 0);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[1] == 5;
        }

        public override float Scale => 1.1f;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            // Print info about the node in order to edit it easier

            node.SaveMeshTexture();


            // Set our custom texture

            // Make it not hold the Boomerang
            SetMeshTexture(node, "SnoreMonkey050Display");


        }
    }
    public class SnoreMonkey500Display : ModTowerDisplay<LazyMonkey>
    {
        // Copy the Boomerang Monkey display
        public override string BaseDisplay => GetDisplay(TowerType.BoomerangMonkey, 5, 0, 0);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[0] == 5;
        }

        public override float Scale => 1.0f;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            // Print info about the node in order to edit it easier

            node.SaveMeshTexture();


            // Set our custom texture

            // Make it not hold the Boomerang
            node.RemoveBone("SuperMonkeyRig:Dart");

            SetMeshTexture(node, "SnoreMonkey500Display", 2);
            SetMeshTexture(node, "SnoreMonkey500Display", 1);
        }
    }
    public class LazyMonkeyParagonDisplay : ModTowerDisplay<LazyMonkey>
    {
        public override string BaseDisplay => GetDisplay(TowerType.DartlingGunner, 0, 5, 0);
        public override float Scale => 0.8f + ParagonDisplayIndex * .05f;  // Higher degree Paragon displays will be bigger

        public override bool UseForTower(int[] tiers)
        {
            return IsParagon(tiers);
        }

        /// <summary>
        /// All classes that derive from ModContent MUST have a zero argument constructor to work
        /// </summary>
        public LazyMonkeyParagonDisplay()
        {
        }

        public LazyMonkeyParagonDisplay(int i)
        {
            ParagonDisplayIndex = i;
        }

        public override int ParagonDisplayIndex { get; }  // Overriding in this way lets us set it in the constructor

        /// <summary>
        /// Create a display for each possible ParagonDisplayIndex
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ModContent> Load()
        {
            for (var i = 0; i < TotalParagonDisplays; i++)
            {
                yield return new LazyMonkeyParagonDisplay(i);
            }
        }


        public override string Name => nameof(LazyMonkeyParagonDisplay) + ParagonDisplayIndex;  // make sure each instance has its own name

        /// <summary>
        /// Could use the ParagonDisplayIndex property to use different effects based on the paragon strength
        /// </summary>
        /// <param name="node"></param>
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            //node.PrintInfo();
            //node.SaveMeshTexture();
            SetMeshTexture(node, nameof(LazyMonkeyParagonDisplay));
        }
    }
}
namespace DartMonkey
{
    public class LazyMonkey : ModTower
    {

        public override string TowerSet => PRIMARY;
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 360;

        public override ParagonMode ParagonMode => ParagonMode.Base555;
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 5;
        public override string Description => "The Boomerang monkey decided to take a nap. Little do the bloons know he's a heavy snorer";

        public override string DisplayName => "Snore Monkey";
        public override IEnumerable<int[]> TowerTiers()
        {
            if (MelonHandler.Mods.OfType<BloonsTD6Mod>().Any(m => m.GetModName() == "UltimateCrosspathing"))
            {
                for (var top = 0; top <= TopPathUpgrades; top++)
                {
                    for (var mid = 0; mid <= MiddlePathUpgrades; mid++)
                    {
                        for (var bot = 0; bot <= BottomPathUpgrades; bot++)
                        {
                            yield return new[] { top, mid, bot };
                        }
                    }
                }
            }
            else
            {
                foreach (var towerTier in base.TowerTiers())
                {
                    yield return towerTier;
                }
            }
        }
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            
            towerModel.range += 7;
            var attackmodel = towerModel.GetAttackModel();
            attackmodel.range += 7;
            var projectile = attackmodel.weapons[0].projectile;
            attackmodel.weapons[0].Rate *= 0.90f;
            attackmodel.weapons[0].projectile.display = "";
            projectile.pierce += 2;
            projectile.GetBehavior<TravelStraitModel>().Lifespan = 5.0f;
            projectile.GetBehavior<TravelStraitModel>().Speed = 125;
            projectile.ApplyDisplay<SnoreAttackDisplay>();
            projectile.scale += 1;
            towerModel.towerSize += 2;
            towerModel.doesntRotate = false;
            projectile.canCollisionBeBlockedByMapLos = false;
            

        }

    }


}
namespace Lazy_MonkeyParagon
{
    public class SonOfTheSnoreGod : ModParagonUpgrade<LazyMonkey>
    {
        public override int Cost => 1780000;
        public override string Description => "WHO GAVE THE SLEEPING BOOMERANG MONKEY A M.A.D SUIT";
        public override string DisplayName => "Son Of The Snore God";

        public override void ApplyUpgrade(TowerModel tower)
        {


            foreach (var attackmodel in tower.GetWeapons())
            {
                attackmodel.useAttackPosition = true;
                var startOfRoundRateBuffModel = Game.instance.model.GetTower(TowerType.SpikeFactory, 0, 0, 2)
                    .GetBehavior<StartOfRoundRateBuffModel>().Duplicate();
                startOfRoundRateBuffModel.modifier = .001f;
                startOfRoundRateBuffModel.duration = 5;
                tower.AddBehavior(startOfRoundRateBuffModel);
                foreach (var towerModel in tower.GetWeapons())
                {
                    tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 5, 0, 180, null, false);
                }
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.GetDamageModel().damage =+ 10000;
                    
                }
                attackmodel.Rate = 0.03f;
            }
            foreach (var towerModel in tower.GetWeapons())
            {
                
            }
        }
    }
}
namespace Lazy_Monkey.Upgrades.TopPath
{
    public class LargerSnoreLocation : ModUpgrade<LazyMonkey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 190;

        public override string Description => "Bigger snore radius";

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


        public override void ApplyUpgrade(TowerModel tower)
        {
            foreach (var attackmodel in tower.GetWeapons())
            {
                tower.GetAttackModel().range += 12;
                tower.range += 12;
                
            }
            foreach (var projectileModel in tower.GetWeapons())
            {
                
            }
            foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                


            }

        }
    }
}
namespace Lazy_Monkey.Upgrades.TopPath
{
    public class WallPiercingSnore : ModUpgrade<LazyMonkey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 480;

        public override string Description => "The projectiles can now pass through walls";

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


        public override void ApplyUpgrade(TowerModel tower)
        {
            foreach (var towerModel in tower.GetWeapons())
            {
                tower.GetAttackModel().attackThroughWalls = true;
                
                tower.GetAttackModel().range += 5;
                tower.range += 5;
                
            }
            foreach (var projectileModel in tower.GetWeapons())
            {
                

            }
            foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                projectile.ignoreBlockers = true;


            }

        }
    }
}
namespace Lazy_Monkey.Upgrades.MiddlePath
{
    public class SwiftySnore : ModUpgrade<LazyMonkey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 260;

        public override string Description => "Snoring becomes faster";

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


        public override void ApplyUpgrade(TowerModel tower)
        {
            foreach (var attackModel in tower.GetWeapons())
            {
                attackModel.Rate = 0.65f;

            }
        }
    }
}
namespace Lazy_Monkey.Upgrades.MiddlePath
{
    public class SuperSwiftySnore : ModUpgrade<LazyMonkey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 460;

        public override string Description => "Snoring becomes faster";

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


        public override void ApplyUpgrade(TowerModel tower)
        {
            foreach (var attackModel in tower.GetWeapons())
            {
                attackModel.Rate = 0.40f;

            }
        }

    }
    namespace Lazy_Monkey.Upgrades.BottomPath
    {
        public class PiercingSnore : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => BOTTOM;
            public override int Tier => 1;
            public override int Cost => 240;

            public override string Description => "Snores get +1 pierce";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.pierce += 1;
                }
            }
        }

    }
    namespace Lazy_Monkey.Upgrades.BottomPath
    {
        public class DamagingSoundWaves : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => BOTTOM;
            public override int Tier => 2;
            public override int Cost => 880;

            public override string Description => "Snores get +1 pierce and some more damage, can now damage leads";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.GetDamageModel().damage++;
                    projectile.pierce += 1;
                    
                }
                foreach (var weaponModel in tower.GetWeapons())
                {
                    weaponModel.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                }
            }
        }

    }
    namespace Lazy_Monkey.Upgrades.MiddlePath
    {
        public class DoubleSnore : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override int Cost => 1660;

            public override string Description => "The laziness increases, snoring now shoots out 2 snores";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = 0.40f;
                    tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 2, 0, 15, null, false);
                }
            }
        }
    }
    namespace Lazy_Monkey.Upgrades.MiddlePath
    {
        public class TripleSnore : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override int Cost => 3570;

            public override string Description => "Snoring now shoots out 3 snores at a faster rate";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = 0.20f;
                    tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 20, null, false);

                }
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.GetDamageModel().damage += 1;
                    projectile.pierce += 1;
                }
            }
        }
    }
    namespace Lazy_Monkey.Upgrades.MiddlePath
    {
        public class SnoreMachineGun : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override int Cost => 124960;

            public override string Description => "Snore galore";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.GetDamageModel().damage += 4;
                    tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 7, 0, 25, null, false);
                }
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = 0.02f;

                }

            }
        }
    }
    namespace Lazy_Monkey.Upgrades.MiddlePath
    {
        public class Binoculars : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => TOP;
            public override int Tier => 3;
            public override int Cost => 1670;

            public override string Description => "Range is much improved and can see camo";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {

                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    tower.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
                    projectile.pierce += 2;
                    tower.GetAttackModel().range += 35;
                    tower.range += 35;
                }
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .35f;

                }
                foreach (var towerModel in tower.GetWeapons())
                {
                    

                }

            }
        }
    }
    namespace Lazy_Monkey.Upgrades.MiddlePath
    {
        public class EchoLocation : ModUpgrade<LazyMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => TOP;
            public override int Tier => 4;
            public override int Cost => 5140;

            public override string Description => "Gains map wide range, attacks gain more pierce, and attacks now seek";

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


            public override void ApplyUpgrade(TowerModel tower)
            {

                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.GetDamageModel().damage += 2;
                    projectile.pierce += 2;

                    tower.GetAttackModel().range += 999;
                    tower.range += 999;
                    projectile.AddBehavior(new TrackTargetModel("Testname", 9999999, true, false, 144, false, 300, false, true));
                }
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .35f;

                }
                

            }
        }
        namespace Lazy_Monkey.Upgrades.MiddlePath
        {
            public class AllSeeingSnore : ModUpgrade<LazyMonkey>
            {
                // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
                // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

                public override int Path => TOP;
                public override int Tier => 5;
                public override int Cost => 87100;

                public override string Description => "Can see every corner";

                // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


                public override void ApplyUpgrade(TowerModel tower)
                {

                    foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                    {
                        projectile.GetDamageModel().damage += 30;
                        projectile.pierce += 100;

                        tower.GetAttackModel().range += 999;
                        tower.range += 999;
                        tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 25, null, false);
                        

                    }
                    foreach (var attackModel in tower.GetWeapons())
                    {
                        attackModel.Rate = .20f;

                    }
                    foreach (var projectileModel in tower.GetWeapons())
                    {
                        
                        ;
                    }

                }
            }
        }
        namespace Lazy_Monkey.Upgrades.MiddlePath
        {
            public class SnoreSniper : ModUpgrade<LazyMonkey>
            {
                // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
                // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

                public override int Path => BOTTOM;
                public override int Tier => 3;
                public override int Cost => 2150;

                public override string Description => "Deals A LOT more damage but sacrifices pierce";

                // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


                public override void ApplyUpgrade(TowerModel tower)
                {

                    foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                    {
                        projectile.GetDamageModel().damage += 20;
                        projectile.pierce = 1;
                        
                    }
                    foreach (var attackModel in tower.GetWeapons())
                    {
                        attackModel.Rate = 0.50f;
                        tower.GetAttackModel().range += 5;
                        tower.range += 5;
                    }
                    foreach (var towerModel in tower.GetWeapons())
                    {
                        
                    }

                }
            }
        }
        namespace Lazy_Monkey.Upgrades.MiddlePath
        {
            public class StunningSoundwaves : ModUpgrade<LazyMonkey>
            {
                // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
                // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

                public override int Path => BOTTOM;
                public override int Tier => 4;
                public override int Cost => 6560;

                public override string Description => "Snoring becomes much more deadly";

                // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"


                public override void ApplyUpgrade(TowerModel tower)
                {

                    foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                    {
                        projectile.GetDamageModel().damage += 58;
                        projectile.pierce = 1;

                    }
                    foreach (var attackModel in tower.GetWeapons())
                    {
                        attackModel.Rate = 0.60f;
                        tower.GetAttackModel().range += 10;
                        tower.range += 10; 
                    }
                    foreach (var towerModel in tower.GetWeapons())
                    {

                    }

                }
            }
        }
        namespace Lazy_Monkey.Upgrades.MiddlePath
        {
            public class SnoringRayOfDOOM : ModUpgrade<LazyMonkey>
            {
                // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
                // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

                public override int Path => BOTTOM;
                public override int Tier => 5;
                public override int Cost => 257890;

                public override string Description => "The condensed build of snoring can wipe out any bloon";

                public override string DisplayName => "Snoring Ray Of DOOM";


                public override void ApplyUpgrade(TowerModel tower)
                {

                    foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                    {
                        projectile.GetDamageModel().damage += 500;
                        projectile.pierce = 1;

                    }
                    foreach (var attackModel in tower.GetWeapons())
                    {
                        attackModel.Rate = 0.50f;
                        tower.GetAttackModel().range += 20;
                        tower.range += 20;

                    }
                    foreach (var towerModel in tower.GetWeapons())
                    {
                        tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 20, 0, 3, null, false);
                    }

                }
            }
        }
    }
}