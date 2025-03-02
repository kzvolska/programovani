using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_game
{
    internal class Vehicles
    {
        private int gas;
        
        public virtual void SetGas (int value) 
        { 
            gas = value; 
        }
        public virtual int GetGas() { return gas;}
    }

    class Car : Vehicles
    {
         public Car(int gas)
         {
            SetGas(gas);
         }
        public override int GetGas() 
        {
            return base.GetGas();
        }
    }

    class Motorcycle : Vehicles 
    {
        public Motorcycle(int gas)
        {
            SetGas(gas);
        }
        public override int GetGas()
        {
            return base.GetGas();
        }
    }

    class Plane : Vehicles
    {
        public Plane(int gas)
        {
            SetGas(gas);
        }
        public override int GetGas()
        {
            return base.GetGas();
        }
    }
}
