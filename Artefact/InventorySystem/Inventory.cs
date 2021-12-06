using Artefact.Items;
using Artefact.Items.Equipables;
using Artefact.Misc;
using System;
using System.Collections.Generic;

namespace Artefact.InventorySystem
{
    [Serializable]
    internal class Inventory
    {
        private List<ItemData> items = new List<ItemData>();

        public ItemData Weapon { get; private set; }
        public ItemData Helmet { get { Armor.TryGetValue(ArmorType.Helmet, out ItemData armorItem); return armorItem; } }
        public ItemData Chestplate { get { Armor.TryGetValue(ArmorType.Chestplate, out ItemData armorItem); return armorItem; } }
        public ItemData Leggings { get { Armor.TryGetValue(ArmorType.Leggings, out ItemData armorItem); return armorItem; } }
        public ItemData Boots { get { Armor.TryGetValue(ArmorType.Boots, out ItemData armorItem); return armorItem; } }
        public Dictionary<ArmorType, ItemData> Armor { get; } = new Dictionary<ArmorType, ItemData>();

        public void AddItem(ItemData item, bool announce = true)
        {
            ItemData itemData = GetItem(item.Item);
            if (itemData == null)
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
                Utils.Type($"Acquired [{ColorConstants.GOOD_COLOR}]{item.Amount}[/] [{ColorConstants.ITEM_COLOR}]{item.Item}[/]");
            }
        }

        public void RemoveItem(ItemData item)
        {
            ItemData itemData = GetItem(item.Item);
            if (itemData != null)
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

        public bool HasItem(ItemData item)
        {
            ItemData itemData = GetItem(item.Item);
            if (itemData == null)
            {
                return false;
            }
            return itemData.Amount >= item.Amount;
        }

        public ItemData GetItem(string name)
        {
            return items.Find(i => i.Item.Name.ToLower() == name.ToLower());
        }

        public ItemData GetItem(Item item)
        {
            return GetItem(item.Name);
        }

        public int GetAmount(Item item)
        {
            ItemData i = GetItem(item.Name);

            return i == null ? 0 : i.Amount;
        }

        public List<ItemData> GetItems()
        {
            return items;
        }

        public void EquipItem(Item item)
        {
            if (!(item is IEquipable equipableItem)) return;

            switch (equipableItem.EquipableType)
            {
                case EquipableType.Weapon:
                    {
                        if (HasItem(new ItemData(item)))
                        {
                            RemoveItem(new ItemData(item));
                            if (Weapon != null)
                            {
                                AddItem(Weapon, false);
                            }
                            Weapon = (ItemData)item;
                        }
                    }
                    break;
                case EquipableType.Armor:
                    {
                        if (HasItem(new ItemData(item)))
                        {
                            ArmorItem armorItem = (ArmorItem)item;
                            RemoveItem(new ItemData(item));
                            if (Armor.TryGetValue(armorItem.ArmorType, out ItemData equippedArmor))
                            {
                                AddItem(equippedArmor, false);
                            }
                            Armor.AddOrReplace(armorItem.ArmorType, (ItemData)armorItem);
                        }
                    }
                    break;
                default:
                    {
                        Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]No equip definition for {equipableItem.EquipableType}");
                    }
                    break;
            }
        }
    }
}
