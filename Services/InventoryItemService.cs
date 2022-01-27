using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Data.Enums;

namespace Services
{
    public class InventoryItemService
    {
        private readonly GenericRepository<InventoryItem> _inventoryItemRepository;

        public InventoryItemService()
        {
            _inventoryItemRepository = new GenericRepository<InventoryItem>();
        }

        public List<InventoryItem> GetCurrentInventoryItems()
        {
            //List<InventoryItem> list = new List<InventoryItem>(); 

            //foreach (InventoryItem inventoryItem in _inventoryItemRepository.GetAll())
            //{
            //    if (inventoryItem.DateOfWriteOff == null)
            //    {
            //        list.Add(inventoryItem);
            //    }
            //}
            //return list;

            return _inventoryItemRepository.GetAll().Where(i => i.DateOfWriteOff.Equals(null)).ToList();
        }

        public List<InventoryItem> GetCurrentInventoryItemsSortByTypeAndName()
        {
            return GetCurrentInventoryItems().OrderBy(i => i.Type).ThenBy(i => i.Name).ToList();
        }

        public void RegisterInventoryItem(string name, string type, double price, double priceOfPenalty)
        {
            InventoryItem inventoryItem = new InventoryItem(name, type, price, priceOfPenalty);
            _inventoryItemRepository.Create(inventoryItem);
        }

        public void ChangePriceInvemtoryItem(InventoryItem oldInventoryItem, double price, double priceOfPenalty)
        {
            InventoryItem newInventoryItem = new InventoryItem(oldInventoryItem.Name, oldInventoryItem.Type, price, priceOfPenalty, oldInventoryItem.WorkingСapacity);
            oldInventoryItem.DateOfWriteOff = DateTime.Now;

            _inventoryItemRepository.Create(newInventoryItem);
            _inventoryItemRepository.Update(oldInventoryItem);
        }

        public void WriteOffInventoryItem(InventoryItem inventoryItem)
        {
            inventoryItem.DateOfWriteOff = DateTime.Now;
            inventoryItem.WorkingСapacity = false;
            _inventoryItemRepository.Update(inventoryItem);
        }
    }
}
