using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    class EnemyEntity : Entity
    {
        public EnemyEntity(EnemyType enemyType) : base(1, 1)
        {
            switch (enemyType)
            {
                case EnemyType.Virus:
                    {
                        MaxHealth = 10;
                        HitDamage = 1;
                    }break;
            }
            Health = MaxHealth;
        }
    }

    enum EnemyType
    {
        Virus
    }
}
