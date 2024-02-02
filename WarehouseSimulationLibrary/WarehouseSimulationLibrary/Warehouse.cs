using System;
using System.Collections.Generic;

namespace WarehouseSimulation
{
    /// <summary>
    /// Represents a warehouse with shelves, a delivery schedule, a pickup schedule, a terminal, and simulation functionality.
    /// </summary>
    public class Warehouse
    {
        /// <summary>
        /// Gets the list of shelves in the warehouse.
        /// </summary>
        public List<Shelf> Shelves { get; private set; }

        /// <summary>
        /// Gets the configuration settings for the warehouse.
        /// </summary>
        public WarehouseConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets the schedule for incoming deliveries.
        /// </summary>
        public DeliverySchedule DeliverySchedule { get; private set; }

        /// <summary>
        /// Gets the schedule for item pickups from the warehouse.
        /// </summary>
        public PickupSchedule PickupSchedule { get; private set; }

        /// <summary>
        /// Gets the terminal in the warehouse for temporary item storage.
        /// </summary>
        public Terminal Terminal { get; private set; }

        /// <summary>
        /// Gets or sets the current day of the simulation.
        /// </summary>
        public int CurrentSimulationDay { get; private set; }

        /// <summary>
        /// Gets or sets the list of historical items in the warehouse.
        /// </summary>
        public List<Item> ItemHistory { get; set; } = new List<Item>();

        private Simulation simulation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Warehouse"/> class with the specified configuration.
        /// <param name="configuration">The configuration settings for the warehouse.</param>
        /// </summary>
        public Warehouse(WarehouseConfiguration configuration)
        {
            Configuration = configuration;
            Shelves = new List<Shelf>();
            DeliverySchedule = new DeliverySchedule();
            PickupSchedule = new PickupSchedule();
            Terminal = new Terminal(configuration.TerminalCapacity);
            CurrentSimulationDay = 0;
            simulation = new Simulation();
        }

        /// <summary>
        /// Adds a new item to the warehouse with the specified name and goods type.
        /// <param name="itemName">The name of the new item.</param>
        /// <param name="goodsType">The goods type of the new item.</param>
        /// </summary>
        public void AddItem(string itemName, GoodsType goodsType)
        {
            Item newItem = new Item(itemName, goodsType);
            Console.WriteLine($"Item '{newItem.Name}' added to the warehouse.");
        }

        /// <summary>
        /// Adds a new shelf to the warehouse.
        /// <param name="shelfId">The unique identifier for the new shelf.</param>
        /// <param name="capacity">The maximum capacity of the new shelf.</param>
        /// <param name="goodsType">The type of goods that can be stored on the new shelf.</param>
        /// <param name="terminalToShelfTime">The time it takes to transfer items from the terminal to the new shelf.</param>
        /// <param name="shelfToTerminalTime">The time it takes to transfer items from the new shelf to the terminal.</param>
        /// </summary>
        public void AddShelf(string shelfId, int capacity, GoodsType goodsType, int terminalToShelfTime, int shelfToTerminalTime)
        {
            Shelf newShelf = Shelf.CreateShelf(shelfId, capacity, goodsType, terminalToShelfTime, shelfToTerminalTime);
            Shelf.AddShelfToWarehouse(Shelves, newShelf);
        }

        /// <summary>
        /// Removes a shelf from the warehouse based on its unique identifier.
        /// <param name="shelfId">The unique identifier of the shelf to be removed.</param>
        /// </summary>
        public void RemoveShelf(string shelfId)
        {
            Shelf.RemoveShelfFromWarehouse(Shelves, shelfId);
        }

        /// <summary>
        /// Adds a one-time delivery to the warehouse.
        /// <param name="simulationDay">The simulation day on which the delivery should occur.</param>
        /// <param name="goodsType">The type of goods in the delivery.</param>
        /// <param name="quantity">The quantity of goods in the delivery.</param>
        /// <param name="itemName">The name of the item in the delivery.</param>
        /// </summary>
        public void AddDelivery(int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Delivery newDelivery = new Delivery(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newDelivery.ScheduleDelivery(this);
        }

        /// <summary>
        /// Adds a recurring weekly delivery to the warehouse.
        /// <param name="daysBetweenDeliveries">The number of days between each weekly delivery.</param>
        /// <param name="simulationDay">The simulation day on which the first delivery should occur.</param>
        /// <param name="goodsType">The type of goods in the delivery.</param>
        /// <param name="quantity">The quantity of goods in the delivery.</param>
        /// <param name="itemName">The name of the item in the delivery.</param>
        /// </summary>
        public void AddWeeklyDelivery(int daysBetweenDeliveries, int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Delivery newWeeklyDelivery = new Delivery(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newWeeklyDelivery.ScheduleWeeklyDelivery(this, daysBetweenDeliveries);
        }

        /// <summary>
        /// Adds a one-time pickup request to the warehouse.
        /// <param name="simulationDay">The simulation day on which the pickup should occur.</param>
        /// <param name="goodsType">The type of goods in the pickup request.</param>
        /// <param name="quantity">The quantity of goods in the pickup request.</param>
        /// <param name="itemName">The name of the item in the pickup request.</param>
        /// </summary>
        public void AddPickup(int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Pickup newPickup = new Pickup(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newPickup.SchedulePickup(this);
        }

        /// <summary>
        /// Adds a recurring weekly pickup request to the warehouse.
        /// <param name="daysBetweenPickups">The number of days between each weekly pickup request.</param>
        /// <param name="simulationDay">The simulation day on which the first pickup request should occur.</param>
        /// <param name="goodsType">The type of goods in the pickup request.</param>
        /// <param name="quantity">The quantity of goods in the pickup request.</param>
        /// </summary>
        public void AddWeeklyPickup(int daysBetweenPickups, int simulationDay, GoodsType goodsType, int quantity, string itemName)
        {
            Pickup newWeeklyPickup = new Pickup(simulationDay, goodsType, quantity, new Item(itemName, goodsType));
            newWeeklyPickup.ScheduleWeeklyPickup(this, daysBetweenPickups);
        }

        /// <summary>
        /// Processes a specific delivery by transferring items from the terminal to the shelves.
        /// <param name="delivery">The delivery to be processed.</param>
        /// </summary>
        public void ProcessDelivery(Delivery delivery)
        {
            delivery.Process(this);
        }

        /// <summary>
        /// Processes a specific pickup request by transferring items from the shelves to the terminal.
        /// <param name="pickup">The pickup request to be processed.</param>
        /// </summary>
        public void ProcessPickup(Pickup pickup)
        {
            pickup.Process(this);
        }

        /// <summary>
        /// Prints the location history of a specific item.
        /// <param name="itemName">The name of the item to retrieve and print the history for.</param>
        /// </summary>
        public void PrintItemHistory(string itemName)
        {
            Item.PrintItemHistory(ItemHistory, itemName);
        }

        /// <summary>
        /// Runs the simulation for a specified number of days.
        /// <param name="daysToSimulate">The number of days to simulate.</param>
        /// </summary>
        public void SimulationRun(int daysToSimulate)
        {
            simulation.SimulationRun(this, daysToSimulate);
        }

        /// <summary>
        /// Finds or creates an item with the specified name and goods type in the warehouse.
        /// <param name="itemName">The name of the item to find or create.</param>
        /// <param name="goodsType">The goods type of the item to find or create.</param>
        /// <returns>The existing or newly created item.</returns>
        /// </summary>
        public Item FindOrCreateItem(string itemName, GoodsType goodsType)
        {
            Item existingItem = Shelves
                .SelectMany(shelf => shelf.Items)
                .FirstOrDefault(item => item.Name == itemName && item.GoodsType == goodsType);

            if (existingItem != null)
            {
                return existingItem;
            }

            Item newItem = new Item(itemName, goodsType);
            return newItem;
        }
    }
}
