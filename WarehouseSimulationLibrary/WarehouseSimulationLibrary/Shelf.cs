namespace WarehouseSimulation
{
    /// <summary>
    /// Represents the types of goods that can be stored in the warehouse.
    /// </summary>
    public enum GoodsType
    {
        DryGoods,
        Refrigerated,
        Hazardous
    }

    /// <summary>
    /// Represents a shelf in the warehouse.
    /// </summary>
    public class Shelf
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shelf.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the maximum capacity of the shelf.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the type of goods that can be stored on the shelf.
        /// </summary>
        public GoodsType GoodsType { get; set; }

        /// <summary>
        /// Gets or sets the area or location of the shelf in the warehouse.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the time it takes to transfer items from the terminal to the shelf.
        /// </summary>
        public int TerminalToShelfTime { get; set; }

        /// <summary>
        /// Gets or sets the time it takes to transfer items from the shelf to the terminal.
        /// </summary>
        public int ShelfToTerminalTime { get; set; }

        /// <summary>
        /// Gets or sets the list of items currently on the shelf.
        /// </summary>
        public List<Item> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shelf"/> class.
        /// <param name="id">The unique identifier for the shelf.</param>
        /// <param name="capacity">The maximum capacity of the shelf.</param>
        /// <param name="goodsType">The type of goods that can be stored on the shelf.</param>
        /// <param name="terminalToShelfTime">The time it takes to transfer items from the terminal to the shelf.</param>
        /// <param name="shelfToTerminalTime">The time it takes to transfer items from the shelf to the terminal.</param>
        /// </summary>
        public Shelf(string id, int capacity, GoodsType goodsType, int terminalToShelfTime, int shelfToTerminalTime)
        {
            Id = id;
            Capacity = capacity;
            GoodsType = goodsType;
            TerminalToShelfTime = terminalToShelfTime;
            ShelfToTerminalTime = shelfToTerminalTime;
            Items = new List<Item>();
        }

        /// <summary>
        /// Adds an item to the shelf if there is available capacity.
        /// <param name="item">The item to be added to the shelf.</param>
        /// </summary>
        public void AddItem(Item item)
        {
            if (Items.Count < Capacity)
            {
                Items.Add(item);
            }
            else
            {
                Console.WriteLine($"Shelf '{Id}' is at full capacity. Cannot add item '{item.Name}'.");
            }
        }

        /// <summary>
        /// Removes an item from the shelf if it exists on the shelf.
        /// <param name="item">The item to be removed from the shelf.</param>
        /// </summary>
        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
            else
            {
                Console.WriteLine($"Item '{item.Name}' not found on shelf '{Id}'.");
            }
        }

        /// <summary>
        /// Creates a new shelf.
        /// <param name="shelfId">The unique identifier for the new shelf.</param>
        /// <param name="capacity">The maximum capacity of the new shelf.</param>
        /// <param name="goodsType">The type of goods that can be stored on the new shelf.</param>
        /// <param name="terminalToShelfTime">The time it takes to transfer items from the terminal to the new shelf.</param>
        /// <param name="shelfToTerminalTime">The time it takes to transfer items from the new shelf to the terminal.</param>
        /// <returns>The newly created shelf.</returns>
        /// </summary>
        public static Shelf CreateShelf(string shelfId, int capacity, GoodsType goodsType, int terminalToShelfTime, int shelfToTerminalTime)
        {
            return new Shelf(shelfId, capacity, goodsType, terminalToShelfTime, shelfToTerminalTime);
        }

        /// <summary>
        /// Adds a new shelf to the warehouse.
        /// <param name="shelves">The list of shelves in the warehouse.</param>
        /// <param name="newShelf">The new shelf to be added to the warehouse.</param>
        /// </summary>
        public static void AddShelfToWarehouse(List<Shelf> shelves, Shelf newShelf)
        {
            shelves.Add(newShelf);
            Console.WriteLine($"Shelf '{newShelf.Id}' added to the warehouse.");
        }

        /// <summary>
        /// Removes a shelf from the warehouse.
        /// <param name="shelves">The list of shelves in the warehouse.</param>
        /// <param name="shelfId">The unique identifier of the shelf to be removed.</param>
        /// </summary>
        public static void RemoveShelfFromWarehouse(List<Shelf> shelves, string shelfId)
        {
            Shelf shelfToRemove = shelves.FirstOrDefault(shelf => shelf.Id == shelfId);

            if (shelfToRemove != null)
            {
                shelves.Remove(shelfToRemove);
                Console.WriteLine($"Shelf '{shelfToRemove.Id}' removed from the warehouse.");
            }
            else
            {
                Console.WriteLine($"Shelf '{shelfId}' not found in the warehouse.");
            }
        }
    }
}
