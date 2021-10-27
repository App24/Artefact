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
        List<ItemData> items = new List<ItemData>();

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

        public void AddItem(ItemData item, bool announce = false)
        {
            ItemData itemData = GetItem(item.Item);
            if (itemData.Equals(default(ItemData)))
            {
                itemData = item;
            }
            else
            {
                itemData.Amount += item.Amount;
            }
            items.AddOrInsert(itemData);

            if (announce)
            {
                Utils.Type($"Acquired [green]{item.Amount}[/] [magenta]{item.Item}[/]");
            }
        }

        public void RemoveItem(ItemData item)
        {
            ItemData itemData = GetItem(item.Item);
            if (!itemData.Equals(default(ItemData)))
            {
                itemData.Amount -= item.Amount;
                if (itemData.Amount <= 0)
                {
                    items.Remove(itemData);
                }
                else
                {
                    items.AddOrInsert(itemData);
                }
            }
        }

        public bool HasItem(ItemData item, bool checkAmount = false)
        {
            ItemData itemData = GetItem(item.Item);
            if (itemData.Equals(default(ItemData)))
            {
                return false;
            }
            if (checkAmount)
            {
                return itemData.Amount >= item.Amount;
            }
            return true;
        }

        public ItemData GetItem(string name)
        {
            return items.Find(i=>i.Item.Name.ToLower()==name.ToLower());
        }

        public ItemData GetItem(Item item)
        {
            return GetItem(item.Name);
        }

        public int GetAmount(Item item)
        {
            ItemData i = GetItem(item.Name);

            return i.Equals(default(ItemData))? 0 : i.Amount;
        }

        public List<ItemData> GetItems()
        {
            return items;
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
