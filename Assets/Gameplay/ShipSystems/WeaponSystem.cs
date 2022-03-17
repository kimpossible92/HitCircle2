using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;
using System.Linq;

namespace Gameplay.ShipSystems
{
    public class WeaponSystem : MonoBehaviour
    {

        [SerializeField]
        private List<knifeWeapon> _weapons;
        public List<knifeWeapon> myWeapons => _weapons;
        [SerializeField] private GameObject goParent;
        bool ll1 = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ll1 = true;
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                ll1 = false;
            }
        }
        public void setRocketOrBeam() {  }
        public void setBeam() { }
        public void Init(UnitBattleIdentity battleIdentity)
        {
        }


        public void TriggerFire()
        {
            if (tag == "Player")
            {
                if (ll1 == true) {}
                if (ll1 == false)
                {
                }
            }
            else if (tag == "bonus") { }
            else { }
        }
        public void SetNewSpeed()
        {
            
        }
    }
}
