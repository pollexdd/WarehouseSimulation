using System;
using System.Collections.Generic;

namespace WarehouseSimulation
{
    /// <summary>
    /// Represents the terminal area in a warehouse where items are temporarily stored.
    /// </summary>
    public class Terminal
    {
        /// <summary>
        /// Gets the list of items currently in the terminal.
        /// </summary>
        public List<Item> Items { get; private set; }

        /// <summary>
        /// Gets or sets the maximum capacity of the terminal.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Terminal"/> class.
        /// <param name="capacity">The maximum capacity of the terminal.</param>
        /// </summary>
        public Terminal(int capacity)
        {
            Capacity = capacity;
            Items = new List<Item>();
        }

        /// <summary>
        /// Adds an item to the terminal if there is available capacity.
        /// <param name="item">The item to be added to the terminal.</param>
        /// /// </summary>
        public void AddItem(Item item)
        {
            if (Items.Count < Capacity)
            {
                Items.Add(item);
            }
            else
            {
                Console.WriteLine($"Terminal is at full capacity. Cannot add item '{item.Name}'.");
            }
        }

        /// <summary>
        /// Removes an item from the terminal if it exists in the terminal.
        /// <param name="item">The item to be removed from the terminal.</param>
        /// </summary>
        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
            else
            {
                Console.WriteLine($"Item '{item.Name}' not found in the terminal.");
            }
        }

        /// <summary>
        /// Retrieves a copy of the list of items currently in the terminal.
        /// <returns>A list of items in the terminal.</returns>
        /// </summary>
        public List<Item> GetItems()
        {
            return new List<Item>(Items);
        }

        /// <summary>
        /// Configures the capacity of the terminal.
        /// <param name="capacity">The new maximum capacity of the terminal.</param>
        /// </summary>
        public void ConfigureTerminal(int capacity)
        {
            Capacity = capacity;
            Console.WriteLine($"Terminal capacity has been configured to {capacity}.");
        }
    }
}
