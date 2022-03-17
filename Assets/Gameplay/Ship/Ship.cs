using Gameplay.ShipSystems;
using Gameplay.Spaceships;
using UnityEngine;

namespace Gameplay.ShipName
{
    public abstract class Ship : MonoBehaviour
    {
        
        private ISpaceship _spaceship;

        public void Init(ISpaceship spaceship)
        {
            _spaceship = spaceship;
        }

        private void Start(){

        }
        public void PlayerHandle()
        {
            if (_spaceship.MovementSystem != null) ProcessHandling(_spaceship.MovementSystem);
            if (_spaceship.WeaponSystem != null) ProcessFire(_spaceship.WeaponSystem);
        }

        protected abstract void ProcessHandling(MovementSystem movementSystem);
        protected abstract void ProcessFire(WeaponSystem fireSystem);
    }
}
