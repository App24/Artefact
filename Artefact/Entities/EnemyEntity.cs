using Artefact.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Entities
{
    [Serializable]
    class EnemyEntity : Entity
    {
        public EnemyType EnemyType { get; }

        public List<ItemDropData> ItemDrops { get; } = new List<ItemDropData>();

        public string ASCIIRepresentation { get; }

        /* Ascii Art got from:
         * https://ascii.co.uk/
         */

        public EnemyEntity(EnemyType enemyType) : base(1, 1)
        {
            EnemyType = enemyType;
            switch (enemyType)
            {
                case EnemyType.Virus:
                    {
                        MaxHealth = 10;
                        HitDamage = 2;
                        ItemDrops.Add(new ItemDropData(Item.BinaryItem, 5));
                        ASCIIRepresentation = @"
     ,-^-.
     |\/\|
     `-V-'
       H
       H
       H
    .-;"":-.
   ,'|  `; \";
                    }
                    break;
                case EnemyType.Trojan:
                    {
                        MaxHealth = 8;
                        HitDamage = 1;
                        ItemDrops.Add(new ItemDropData(Item.BinaryItem, 8));
                        ASCIIRepresentation= @"
               _(\
      _____   /  .|
 >==.'|  | 'TK  \_|
   /  |  |  | \/
   |_ |  |  |__|
  /  \|__|__/  \
  \__/      \__/";
                    }
                    break;
                case EnemyType.RansomWare:
                    {
                        MaxHealth = 15;
                        HitDamage = 5;
                        ItemDrops.Add(new ItemDropData(Item.BinaryItem, 12, 3));
                    }
                    break;
            }
            Health = MaxHealth;
        }
    }

    [Flags] enum EnemyType
    {
        Virus=1,
        Trojan=2,
        RansomWare=4
    }

    struct ItemDropData
    {
        public Item Item { get; }
        public float Chance { get; }
        public uint Min { get; }
        public uint Max { get; }

        public ItemDropData(Item item, uint max, uint min=1, float chance=1)
        {
            Item = item;
            Min = min;
            Max = max;
            Chance = chance;
        }
    }
}
