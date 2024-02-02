namespace WarehouseSimulation
{
    /// <summary>
    /// Represents an item in a warehouse with information such as name, goods type, and location history.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of goods associated with the item.
        /// </summary>
        public GoodsType GoodsType { get; set; }

        /// <summary>
        /// Gets the history of locations the item has been through.
        /// </summary>
        public List<string> LocationHistory { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.       
        /// <param name="name">The name of the item.</param>
        /// <param name="goodsType">The type of goods associated with the item.</param>
        /// </summary>
        public Item(string name, GoodsType goodsType)
        {
            Name = name;
            GoodsType = goodsType;
            LocationHistory = new List<string>();
        }

        /// <summary>
        /// Adds a new location to the item's location history.      
        /// <param name="newLocation">The new location to be added.</param>
        /// </summary>
        public void UpdateLocationHistory(string newLocation)
        {
            LocationHistory.Add(newLocation);
        }

        /// <summary>
        /// Prints the location history of the item.
        /// </summary>
        public void PrintHistory()
        {
            Console.WriteLine($"History for item '{Name}':");

            foreach (var location in LocationHistory)
            {
                Console.WriteLine($"  - {location}");
            }
        }

        /// <summary>
        /// Finds an item by its name in the provided list of items.      
        /// <param name="itemHistory">The list of items to search.</param>
        /// <param name="itemName">The name of the item to find.</param>
        /// <returns>The found item or null if not found.</returns>
        /// </summary>
        public static Item FindItemByName(List<Item> itemHistory, string itemName)
        {
            return itemHistory.FirstOrDefault(item => item.Name == itemName);
        }

        /// <summary>
        /// Prints the location history of an item by its name.        
        /// <param name="itemHistory">The list of items to search.</param>
        /// <param name="itemName">The name of the item to print history for.</param>
        /// </summary>
        public static void PrintItemHistory(List<Item> itemHistory, string itemName)
        {
            Item item = FindItemByName(itemHistory, itemName);

            if (item != null)
            {
                item.PrintHistory();
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' not found in the warehouse.");
            }
        }
    }
}
