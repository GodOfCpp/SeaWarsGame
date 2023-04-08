using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWarsGame
{
    public interface IShip
    {
        void Ability(Point location);
        void Shoot(Point location);
    }

    public class ShipTile
    {
        private int health;
        public bool isDead()
        {
            return health <= 0;
        }

        public void Damage(int value)
        {
            health -= value;
        }
        
        public ShipTile()
        {
            health = 100;
        }
    }
    public abstract class Ship
    {
        public Ship(int size, int dmgValue)
        {
            ship = new ShipTile[size];
            damageValue = dmgValue;
        }

        public bool HasAbility()
        {
            return abilitiesLeft > 0;
        }
        protected ShipTile[]? ship;
        readonly int damageValue;
        protected int abilitiesLeft;
    }

    public class BattleShip : Ship, IShip
    {
        public BattleShip() : base(4, 100)
        {
            
        }
        public void Shoot(Point lcoation)
        {

        }
        public void Ability(Point location)
        {
            throw new NotImplementedException();
        }
    }

    public class Cruiser : Ship, IShip
    {
        public Cruiser() : base(3, 75)
        {

        }
        public void Shoot(Point lcoation)
        {

        }
        public void Ability(Point location)
        {
            throw new NotImplementedException();
        }
    }

    public class Destroyer : Ship, IShip
    {
        public Destroyer() : base(2, 50)
        {

        }
        public void Shoot(Point lcoation)
        {

        }
        public void Ability(Point location)
        {
            throw new NotImplementedException();
        }
    }

    public class Boat : Ship, IShip
    {
        public Boat(): base(1, 25)
        {

        }
        public void Shoot(Point lcoation)
        {

        }
        public void Ability(Point location)
        {
            throw new NotImplementedException();
        }
    }
}
