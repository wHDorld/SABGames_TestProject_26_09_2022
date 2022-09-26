using AssemblyCSharp.Assets.Logic.Unit.Abstract;
using AssemblyCSharp.Assets.Logic.Unit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Logic.Unit.Components
{
    public class UnitBaseComponent : UnitBase
    {
        public override void Start()
        {
            base.Start();
            unitStatus.Damage = UnityEngine.Random.Range(1, 40);
            transform.position += Vector3.up / 2f;
        }

        float interval = 0.4f;
        public override void Attack(UnitBase target)
        {
            base.Attack(target);

            if (interval > 0)
            {
                interval -= Time.deltaTime;
                return;
            }
            interval = UnityEngine.Random.Range(0.1f, 0.4f);
            target.unitStatus.Health -= unitStatus.Damage;
        }

        public override void Movement(Vector3 to)
        {
            base.Movement(to);

            Vector3 direction = (to - transform.position).normalized;

            transform.rotation = Quaternion.LookRotation(direction, transform.up);
            transform.Translate(direction * unitStatus.Speed, Space.World);
        }

        public override void Dying()
        {
            base.Dying();

            transform.position -= Vector3.up / 1.5f;
            transform.localRotation = Quaternion.Euler(90, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}
