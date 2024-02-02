namespace WarehouseSimulation
{
    /// <summary>
    /// Represents a pickup of goods in a warehouse simulation.
    /// </summary>
    public class Pickup
    {
        /// <summary>
        /// Gets or sets the simulation day for the pickup.
        /// </summary>
        public int SimulationDay { get; set; }

        /// <summary>
        /// Gets or sets the type of goods being picked up.
        /// </summary>
        public GoodsType GoodsType { get; set; }

        /// <summary>
        /// Gets or sets the quantity of goods being picked up.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the specific item being picked up.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pickup"/> class.       
        /// <param name="simulationDay">The simulation day for the pickup.</param>
        /// <param name="goodsType">The type of goods being picked up.</param>
        /// <param name="quantity">The quantity of goods being picked up.</param>
        /// <param name="item">The specific item being picked up.</param>
        /// </summary>
        public Pickup(int simulationDay, GoodsType goodsType, int quantity, Item item)
        {
            SimulationDay = simulationDay;
            GoodsType = goodsType;
            Quantity = quantity;
            Item = item;
        }

        /// <summary>
        /// Calculates the pickup time based on the simulation start date.       
        /// <param name="simulationStartDate">The start date of the simulation.</param>
        /// <returns>The calculated pickup time.</returns>
        /// </summary>
        public DateTime GetPickupTime(DateTime simulationStartDate)
        {
            return simulationStartDate.AddDays(SimulationDay);
        }

        /// <summary>
        /// Schedules a one-time pickup in the warehouse.     
        /// <param name="warehouse">The warehouse where the pickup is scheduled.</param>
        /// </summary>
        public void SchedulePickup(Warehouse warehouse)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Pickup newPickup = new Pickup(SimulationDay, GoodsType, Quantity, item);
            warehouse.PickupSchedule.AddPickup(newPickup);
            Console.WriteLine($"Pickup scheduled for '{item.Name}' on simulation day {SimulationDay}.");
        }

        /// <summary>
        /// Schedules a weekly recurring pickup in the warehouse.
        /// <param name="warehouse">The warehouse where the pickup is scheduled.</param>
        /// <param name="daysBetweenPickups">The number of days between each pickup.</param>
        /// </summary>
        public void ScheduleWeeklyPickup(Warehouse warehouse, int daysBetweenPickups)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Pickup newWeeklyPickup = new Pickup(SimulationDay, GoodsType, Quantity, item);
            warehouse.PickupSchedule.AddWeeklyPickup(daysBetweenPickups, newWeeklyPickup);
            Console.WriteLine($"Weekly pickup scheduled for '{item.Name}' every {daysBetweenPickups} days starting on simulation day {SimulationDay}.");
        }

        /// <summary>
        /// Processes the pickup by moving items from shelves to the terminal and then out of the warehouse.
        /// <param name="warehouse">The warehouse where the pickup is processed.</param>
        /// </summary>
        public void Process(Warehouse warehouse)
        {
            int remainingQuantity = Quantity;
            Console.WriteLine($"Now processing pickup item: {Item.Name} Count: {Quantity}");

            foreach (var shelf in warehouse.Shelves)
            {
                if (shelf.GoodsType == GoodsType)
                {
                    var shelfItems = shelf.Items.Where(item => item.GoodsType == GoodsType).ToList();
                    foreach (var item in shelfItems.Take(remainingQuantity))
                    {
                        shelf.RemoveItem(item);
                        remainingQuantity--;

                        warehouse.Terminal.AddItem(item);
                        item.UpdateLocationHistory($"{item.Name} Moved to Terminal from Shelf: {shelf.Id}");

                        System.Threading.Thread.Sleep(shelf.ShelfToTerminalTime);
                    }

                    if (remainingQuantity == 0)
                    {
                        break;
                    }
                }
            }

            foreach (var item in warehouse.Terminal.GetItems().Take(Quantity))
            {
                warehouse.Terminal.RemoveItem(item);
                item.UpdateLocationHistory($"{item.Name} has been sent out of the warehouse");
            }

            if (remainingQuantity == 0)
            {
                Console.WriteLine($"Pickup processed successfully.");
            }
            else
            {
                Console.WriteLine($"Insufficient items in the shelves to fulfill the pickup request. Items will not be removed from the terminal.");
            }
        }
    }
}
