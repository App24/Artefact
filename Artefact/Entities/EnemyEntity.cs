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

        public IntRange XPRange
        {
            get
            {
                return new IntRange((int)Math.Floor(GetLevelXP() * 0.45f), (int)Math.Ceiling(GetLevelXP() * 0.55f));
            }
        }

        /* Ascii Art gotten from:
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
                        HitDamage = new IntRange(5, 8);
                        ItemDrops.Add(new ItemDropData(Item.BitItem, 8));
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
                        HitDamage = new IntRange(5, 7);
                        ItemDrops.Add(new ItemDropData(Item.BitItem, 13));
                        ASCIIRepresentation = @"
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
                        HitDamage = new IntRange(7, 10);
                        ItemDrops.Add(new ItemDropData(Item.BitItem, 18, 5));

                        ASCIIRepresentation = @"
     .--------.
    / .------. \
   / /        \ \
   | |        | |
  _| |________| |_
.' |_|        |_| '.
'._____ ____ _____.'
|     .'____'.     |
'.__.'.'    '.'.__.'
'.__  | YALE |  __.'
|   '.'.____.'.'   |
'.____'.____.'____.'
'.________________.'";
                    }
                    break;
            }
            Health = MaxHealth;
        }

        public void Damage(int amount)
        {
            Utils.WriteColor($"You dealt [{ColorConstants.GOOD_COLOR}]{base.Damage(amount)}[/] damage to [{ColorConstants.ENEMY_COLOR}]{EnemyType}[/]");
        }
    }

    [Flags]
    enum EnemyType
    {
        Virus = 1,
        Trojan = 2,
        RansomWare = 4
    }

    [Serializable]
    struct ItemDropData
    {
        public Item Item { get; }
        public float Chance { get; }
        public uint Min { get; }
        public uint Max { get; }

        public ItemDropData(Item item, uint max, uint min = 1, float chance = 1)
        {
            Item = item;
            Min = min;
            Max = max;
            Chance = chance;
        }
    }
}
