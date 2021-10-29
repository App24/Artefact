using Artefact.Items;
using Artefact.Misc;
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

        public EnemyEntity(EnemyType enemyType) : base(1)
        {
            EnemyType = enemyType;
            switch (enemyType)
            {
                case EnemyType.Virus:
                    {
                        MaxHealth = 50;
                        Defense = 1;
                        HitDamage = new HitDamageRange(5, 8);
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
                        MaxHealth = 60;
                        Defense = 0;
                        HitDamage = new HitDamageRange(5, 7);
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
                        MaxHealth = 30;
                        Defense = 3;
                        HitDamage = new HitDamageRange(7, 10);
                        ItemDrops.Add(new ItemDropData(Item.BinaryItem, 12, 3));
                    }
                    break;
            }
            Health = MaxHealth;
        }

        public new void Damage(int amount)
        {
            Utils.WriteColor($"You dealt [green]{amount}[/] damage to [blue]{EnemyType}[/]");
            base.Damage(amount);
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
