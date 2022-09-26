using AssemblyCSharp.Assets.Logic.Unit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Logic.Unit.Abstract
{
    public abstract class UnitBase : MonoBehaviour
    {
        public UnitStatus unitStatus;
        public UnitBase myEnemy;

        new Transform transform;
        float currentMoveTime = 1;

        public virtual void Start()
        {
            transform = gameObject.transform;
            unitStatus.OnDied += OnDied;
            FindEnemy();
        }

        public virtual void FindEnemy()
        {
            if (myEnemy != null)
                return;
            myEnemy = FindObjectsOfType<UnitBase>()
                .Where(x => !x.unitStatus.IsDead)
                .Where(x => x.myEnemy == null)
                .Where(x => x.GetHashCode() != GetHashCode())
                .OrderBy(x => Vector3.Distance(transform.position, x.GetComponent<Transform>().position))
                .FirstOrDefault();
            if (myEnemy == null)
                return;
            myEnemy.myEnemy = this;
            myEnemy.unitStatus.OnDied += OnEnemyDied;
            unitStatus.OnDied += myEnemy.OnEnemyDied;
        }
        public virtual void ResetEnemy()
        {
            unitStatus.OnDied -= myEnemy.OnEnemyDied;
            myEnemy.unitStatus.OnDied -= OnEnemyDied;
            myEnemy.myEnemy = null;
            myEnemy = null;
        }
        public virtual void Attack(UnitBase unitBase)
        {

        }
        public virtual void Movement(Vector3 to)
        {
            currentMoveTime -= Time.deltaTime;
            if (currentMoveTime <= 0)
            {
                currentMoveTime = 1;
                ResetEnemy();
                FindEnemy();
            }
        }
        public virtual void Dying()
        {

        }

        public virtual void Update()
        {
            if (CanAttack(myEnemy))
                Attack(myEnemy);

            if (myEnemy != null 
                && CanMove(myEnemy.transform.position))
                Movement(myEnemy.transform.position);
        }

        public virtual void OnDied()
        {
            Dying();
        }
        public virtual void OnEnemyDied()
        {
            if (unitStatus.IsDead)
                return;
            myEnemy = null;
            FindEnemy();
        }

        public virtual bool CanAttack(UnitBase unitBase)
        {
            if (unitStatus.IsDead) return false;
            if (myEnemy == null) return false;
            if (Vector3.Distance(transform.position, unitBase.transform.position) > unitStatus.AttackDistance)
                return false;
            return true;
        }
        public virtual bool CanMove(Vector3 to)
        {
            if (unitStatus.IsDead) return false;
            if (myEnemy == null) return false;
            if (Vector3.Distance(transform.position, to) <= unitStatus.AttackDistance)
                return false;
            return true;
        }
    }
}
