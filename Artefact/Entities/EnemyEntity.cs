using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using System;
using System.Collections.Generic;

namespace Artefact.Entities
{
    [Serializable]
    internal class EnemyEntity : Entity
    {
        public EnemyType EnemyType { get; }

        public List<ItemDropData> ItemDrops { get; } = new List<ItemDropData>();

        public string ASCIIRepresentation { get; }

        public IntRange XPRange
        {
            get
            {
                return new IntRange((int)Math.Floor(GetLevelXP() * 0.3f), (int)Math.Ceiling(GetLevelXP() * 0.45f));
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
                        ItemDrops.Add(new ItemDropData(Item.ByteItem, 8));
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
                        ItemDrops.Add(new ItemDropData(Item.ByteItem, 13));
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
                        ItemDrops.Add(new ItemDropData(Item.ByteItem, 18, 5));

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
                case EnemyType.AdWare:
                    {
                        MaxHealth = 20;
                        Defense = 1;
                        HitDamage = new IntRange(3, 4);
                        ItemDrops.Add(new ItemDropData(Item.ByteItem, 6, 3));

                        ASCIIRepresentation = @"
  _____             _                 
 |  __ \           | |                
 | |  | | ___   ___| |_ ___  _ __ ___ 
 | |  | |/ _ \ / __| __/ _ \| '__/ __|
 | |__| | (_) | (__| || (_) | |  \__ \
 |_____/ \___/_\___|\__\___/|_|  |___/
        | |  | |     | |              
        | |__| | __ _| |_ ___         
        |  __  |/ _` | __/ _ \        
        | |  | | (_| | ||  __/        
     ___|_|_ |_|\__,_|\__\___|        
    |__   __| |                       
       | |  | |__   ___ _ __ ___      
       | |  | '_ \ / _ | '_ ` _ \     
       | |  | | | |  __| | | | | |    
       |_|  |_| |_|\___|_| |_| |_|    
                                      
                                      ";
                    }
                    break;
                /*
                 * https://textart.sh/topic/spy
                 */
                case EnemyType.SpyWare:
                    {
                        MaxHealth = 30;
                        Defense = 7;
                        HitDamage = new IntRange(2, 4);
                        ItemDrops.Add(new ItemDropData(Item.ByteItem, 20, 10));

                        ASCIIRepresentation = @"[red]
                ████████████              
              ██▓▓▓▓▓▓▓▓▓▓▓▓██            
            ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██          
            ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██        
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██        
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██        
          ██▓▓▓▓▓▓░░  ████░░██  ██        
            ██▓▓▓▓░░  ████░░██  ██        
          ████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██        
      ████▓▓▓▓██▓▓░░████████░░██████      
    ██▓▓▓▓▓▓▓▓▓▓██░░░░░░░░░░██▓▓▓▓▓▓██    
    ██▓▓▓▓▓▓▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▓██    
  ██▓▓▓▓▓▓▓▓██▓▓▓▓  ██  ▓▓▓▓██▓▓▓▓▓▓▓▓██  
  ██▓▓▓▓██████▓▓▓▓▓▓██  ▓▓▓▓██████▓▓▓▓██  
  ██▓▓▓▓▓▓████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████▓▓▓▓▓▓██  
  ██▓▓▓▓▓▓████▓▓▓▓▓▓▓▓▓▓▓▓▓▓████▓▓▓▓▓▓██  
    ██████  ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██  ██████    
          ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██          
        ██▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓▓▓██        
      ████▓▓▓▓▓▓▓▓██  ██▓▓▓▓▓▓▓▓████      
  ████▓▓▓▓▓▓▓▓▓▓██      ██▓▓▓▓▓▓▓▓▓▓████  
██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██      ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓██
██████████████████      ██████████████████
";
                    }
                    break;
                case EnemyType.Electricity:
                    {
                        MaxHealth = 15;
                        Defense = 3;
                        HitDamage = new IntRange(10, 12);
                        ItemDrops.Add(new ItemDropData(Item.ElectronItem, 5, 3));

                        ASCIIRepresentation = @"[yellow]
        ,/
      ,'/
    ,' /
  ,'  /_____,
.'____    ,'
     /  ,'
    / ,'
   /,'
  /'";
                    }
                    break;
                case EnemyType.Clippy:
                    {
                        MaxHealth = 300;
                        Defense = 10;
                        HitDamage = new IntRange(40, 50);

                        // https://textart.io/cowsay/clippy
                        ASCIIRepresentation = @"[darkred]
     __
    /  \
    |  |
    @  @
    |  |
    || |/
    || ||
    |\_/|
    \___/";
                    }break;
            }
            Health = MaxHealth;
        }

        public void Damage(int amount, bool ignoreDefense = false)
        {
            Utils.WriteColor($"You dealt [{ColorConstants.GOOD_COLOR}]{base.Damage(amount, ignoreDefense)}[/] damage to [{ColorConstants.ENEMY_COLOR}]{EnemyType}[/]");
        }

        public override void Kill()
        {
            Utils.WriteColor($"Killed [{ColorConstants.ENEMY_COLOR}]{EnemyType}");
            Random random = new Random();
            ItemDrops.ForEach(itemDropData =>
            {
                float per = (float)random.NextDouble();
                if (per < itemDropData.Chance)
                {
                    int amount = random.Next((int)itemDropData.Min, (int)itemDropData.Max + 1);
                    Map.Player.Inventory.AddItem(new ItemData(itemDropData.Item, amount));
                }
            });
            int xpAmount = random.Next(XPRange.Min, XPRange.Min + 1);
            Map.Player.AddXP(xpAmount);
        }
    }

    [Flags]
    internal enum EnemyType
    {
        Virus = 1,
        Trojan = 2,
        RansomWare = 4,
        AdWare = 8,
        SpyWare = 16,
        Electricity = 32,
        Clippy = 64
    }

    [Serializable]
    internal struct ItemDropData
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
