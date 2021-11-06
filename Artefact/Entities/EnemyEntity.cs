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
                case EnemyType.AdWare:
                    {
                        MaxHealth = 20;
                        Defense = 1;
                        HitDamage = new IntRange(3, 4);
                        ItemDrops.Add(new ItemDropData(Item.BitItem, 6, 3));

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
                        ItemDrops.Add(new ItemDropData(Item.BitItem, 20, 10));

                        ASCIIRepresentation = @"
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

                        ASCIIRepresentation = @"
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
            }
            Health = MaxHealth;
        }

        public void Damage(int amount, bool ignoreDefense = false)
        {
            Utils.WriteColor($"You dealt [{ColorConstants.GOOD_COLOR}]{base.Damage(amount, ignoreDefense)}[/] damage to [{ColorConstants.ENEMY_COLOR}]{EnemyType}[/]");
        }
    }

    [Flags]
    enum EnemyType
    {
        Virus = 1,
        Trojan = 2,
        RansomWare = 4,
        AdWare = 8,
        SpyWare = 16,
        Electricity=32
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
