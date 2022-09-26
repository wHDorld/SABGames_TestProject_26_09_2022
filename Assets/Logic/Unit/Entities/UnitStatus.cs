using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCSharp.Assets.Logic.Unit.Entities
{
    [Serializable]
    public class UnitStatus
    {
        public float Damage = 10;
        public float AttackDistance = 0.3f;
        public float Speed = 0.05f;
        public bool IsDead = false;

        private float health = 100;
        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                if (IsDead)
                    return;
                health = value;
                if (health <= 0)
                {
                    IsDead = true;
                    OnDied?.Invoke();
                    OnDied = null;
                }
            }
        }

        public delegate void UnitStatusDelegate_Empty();
        public event UnitStatusDelegate_Empty OnDied;
    }
}
