using Base.Defs;
using Base.Entities.Abilities;
using Code.PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleReload
{
    internal class Reload
    {
        private static readonly SharedData Shared = VehicleReloadMain.Shared;
        private static readonly DefRepository Repo = VehicleReloadMain.Repo;
    
        public static void Allow_Reload()
        {
            GroundVehicleWeaponDef GTHailStorm = Repo.GetAllDefs<GroundVehicleWeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Armadillo_Gauss_Turret_GroundVehicleWeaponDef"));
            TacticalItemDef GLAmmo = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_GrenadeLauncher_AmmoClip_ItemDef"));
            ReloadAbilityDef Reload = Repo.GetAllDefs<ReloadAbilityDef>().FirstOrDefault(a => a.name.Equals("Reload_AbilityDef"));

           // GTHailStorm.CompatibleAmmunition = new TacticalItemDef[]
           // {
           //     //GTHailStorm.CompatibleAmmunition[0],
           //     PXAssRifleAmmo,
           // };
           //
           // GTHailStorm.Abilities = new AbilityDef[]
           // {
           //     GTHailStorm.Abilities[0],
           //     Reload,
           // };

            foreach(GroundVehicleWeaponDef weapon in Repo.GetAllDefs<GroundVehicleWeaponDef>())
            {
                if(weapon.Abilities != null && !weapon.Abilities.Contains(Reload))
                {
                    weapon.Abilities = weapon.Abilities.Append(Reload).ToArray();
                }
            }

            foreach (GroundVehicleWeaponDef weapon in Repo.GetAllDefs<GroundVehicleWeaponDef>())
            {   
                if (weapon.CompatibleAmmunition == null)
                {
                    weapon.CompatibleAmmunition = new TacticalItemDef[]
                    {
                        GLAmmo,
                    };
                }
                else
                weapon.CompatibleAmmunition = weapon.CompatibleAmmunition.Append(GLAmmo).ToArray();                
            }
        }
    }
}
