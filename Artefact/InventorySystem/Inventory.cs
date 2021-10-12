using Artefact.Items;
using Artefact.Items.Equipables;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.InventorySystem
{
    [Serializable]
    class Inventory
    {
        Dictionary<string, int> items = new Dictionary<string, int>();

        public WeaponItem Weapon { get; private set; }
        public ArmorItem Helmet { get { Armor.TryGetValue(ArmorType.Helmet, out ArmorItem armorItem); return armorItem; } }
        public ArmorItem Chestplate { get { Armor.TryGetValue(ArmorType.Chestplate, out ArmorItem armorItem); return armorItem; } }
        public ArmorItem Leggings { get { Armor.TryGetValue(ArmorType.Leggings, out ArmorItem armorItem); return armorItem; } }
        public ArmorItem Boots { get { Armor.TryGetValue(ArmorType.Boots, out ArmorItem armorItem); return armorItem; } }
        public Dictionary<ArmorType, ArmorItem> Armor { get; } = new Dictionary<ArmorType, ArmorItem>();
        public int Defense
        {
            get
            {
                int defense = 0;
                foreach (KeyValuePair<ArmorType, ArmorItem> keyValuePair in Armor)
                {
                    defense += keyValuePair.Value.Defense;
                }
                return defense;
            }
        }
        public int AttackDamage
        {
            get
            {
                return Weapon == null ? 1 : Weapon.Damage;
            }
        }

        public void AddItem(ItemData itemData, bool announce = false)
        {
            if(!items.TryGetValue(itemData.Item.Name, out int amount))
            {
                amount = 0;
            }
            amount += itemData.Amount;
            items.AddOrReplace(itemData.Item.Name, amount);

            if (announce)
            {
                Utils.Type($"Acquired [green]{itemData.Amount}[/] [magenta]{itemData.Item}[/]");
            }
        }

        public void RemoveItem(ItemData itemData)
        {
            if(items.TryGetValue(itemData.Item.Name, out int amount))
            {
                amount -= itemData.Amount;
                if (amount <= 0)
                {
                    items.Remove(itemData.Item.Name);
                }
                else
                {
                    items.AddOrReplace(itemData.Item.Name, amount);
                }
            }
        }

        public bool HasItem(ItemData item, bool checkAmount = false)
        {
            if(!checkAmount)
                return items.ContainsKey(item.Item.Name);
            if(!items.TryGetValue(item.Item.Name, out int amount))
            {
                return false;
            }
            return amount >= item.Amount;
        }

        public ItemData GetItem(string name)
        {
            if(items.TryGetValue(name, out int amount))
            {
                return new ItemData(Item.GetItem(name), amount);
            }
            return default(ItemData);
        }

        public int GetAmount(Item item)
        {
            ItemData i = GetItem(item.Name);

            return i.Equals(default(ItemData))? 0 : i.Amount;
        }

        public List<ItemData> GetItems()
        {
            return items.Map(i => new ItemData(Item.GetItem(i.Key), i.Value));
        }

        public void EquipItem(Item item)
        {
            if (!(item is EquipableItem)) return;

            if(item is WeaponItem)
            {
                if (HasItem(new ItemData(item)))
                {
                    RemoveItem(new ItemData(item));
                    if (Weapon != null)
                    {
                        AddItem(new ItemData(Weapon));
                    }
                    Weapon = (WeaponItem)item;
                }
            }else if(item is ArmorItem)
            {
                ArmorItem armorItem = (ArmorItem)item;
                if(HasItem(new ItemData(item)))
                {
                    RemoveItem(new ItemData(item));
                    if (Armor.ContainsKey(armorItem.ArmorType))
                    {
                        AddItem(new ItemData(item));
                    }
                    Armor.AddOrReplace(armorItem.ArmorType, armorItem);
                }
            }
        }
    }
}
