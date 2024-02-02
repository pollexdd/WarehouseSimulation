namespace WarehouseSimulation
{
    /// <summary>
    /// Represents a delivery of goods in a warehouse simulation.
    /// </summary>
    public class Delivery
    {
        /// <summary>
        /// Gets or sets the simulation day for the delivery.
        /// </summary>
        public int SimulationDay { get; set; }

        /// <summary>
        /// Gets or sets the type of goods being delivered.
        /// </summary>
        public GoodsType GoodsType { get; set; }

        /// <summary>
        /// Gets or sets the quantity of goods being delivered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the specific item being delivered.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Delivery"/> class.        
        /// <param name="simulationDay">The simulation day for the delivery.</param>
        /// <param name="goodsType">The type of goods being delivered.</param>
        /// <param name="quantity">The quantity of goods being delivered.</param>
        /// <param name="item">The specific item being delivered.</param>
        /// /// </summary>
        public Delivery(int simulationDay, GoodsType goodsType, int quantity, Item item)
        {
            SimulationDay = simulationDay;
            GoodsType = goodsType;
            Quantity = quantity;
            Item = item;
        }

        /// <summary>
        /// Calculates the delivery time based on the simulation start date.      
        /// <param name="simulationStartDate">The start date of the simulation.</param>
        /// <returns>The calculated delivery time.</returns>
        /// </summary>
        public DateTime GetDeliveryTime(DateTime simulationStartDate)
        {
            return simulationStartDate.AddDays(SimulationDay);
        }

        /// <summary>
        /// Schedules a one-time delivery in the warehouse.       
        /// <param name="warehouse">The warehouse where the delivery is scheduled.</param>
        /// </summary>
        public void ScheduleDelivery(Warehouse warehouse)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Delivery newDelivery = new Delivery(SimulationDay, GoodsType, Quantity, item);
            warehouse.DeliverySchedule.AddDelivery(newDelivery);
            Console.WriteLine($"Delivery scheduled for '{item.Name}' on simulation day {SimulationDay}.");
        }

        /// <summary>
        /// Schedules a weekly recurring delivery in the warehouse.      
        /// <param name="warehouse">The warehouse where the delivery is scheduled.</param>
        /// <param name="daysBetweenDeliveries">The number of days between each delivery.</param>
        /// </summary>
        public void ScheduleWeeklyDelivery(Warehouse warehouse, int daysBetweenDeliveries)
        {
            Item item = warehouse.FindOrCreateItem(Item.Name, GoodsType);
            Delivery newWeeklyDelivery = new Delivery(SimulationDay, GoodsType, Quantity, item);
            warehouse.DeliverySchedule.AddWeeklyDelivery(daysBetweenDeliveries, newWeeklyDelivery);
            Console.WriteLine($"Weekly delivery scheduled for '{item.Name}' every {daysBetweenDeliveries} days starting on simulation day {SimulationDay}.");
        }

        /// <summary>
        /// Processes the delivery by adding items to the terminal and transferring them to shelves.     
        /// <param name="warehouse">The warehouse where the delivery is processed.</param>
        /// </summary>
        public void Process(Warehouse warehouse)
        {
            Console.WriteLine($"Now processing delivery item: {Item.Name} Count: {Quantity}");
            int remainingQuantity = Quantity;
            int itemid = 1;

            for (int i = 0; i < remainingQuantity; i++)
            {
                string uniqueItemName = $"{Item.Name}_{itemid}";

                Item newItem = new Item(uniqueItemName, GoodsType);

                warehouse.Terminal.AddItem(newItem);

                newItem.UpdateLocationHistory($"{newItem.Name} added to Terminal");
                warehouse.ItemHistory.Add(newItem);

                int terminalToShelfTime = warehouse.Shelves
                    .FirstOrDefault(shelf => shelf.GoodsType == GoodsType)
                    ?.TerminalToShelfTime ?? 0;
                itemid++;
            }

            foreach (var shelf in warehouse.Shelves)
            {
                if (shelf.GoodsType == GoodsType)
                {
                    int availableSpace = shelf.Capacity - shelf.Items.Count;
                    int itemsToTransfer = Math.Min(remainingQuantity, availableSpace);

                    for (int i = 0; i < itemsToTransfer; i++)
                    {
                        Item itemToTransfer = warehouse.Terminal.GetItems().FirstOrDefault();

                        if (itemToTransfer != null)
                        {
                            warehouse.Terminal.RemoveItem(itemToTransfer);

                            shelf.AddItem(itemToTransfer);

                            itemToTransfer.UpdateLocationHistory($"{itemToTransfer.Name} moved from terminal to Shelf: {shelf.Id}");

                            int terminalToShelfTime = shelf.TerminalToShelfTime;

                            System.Threading.Thread.Sleep(terminalToShelfTime);
                            remainingQuantity--;
                        }
                        else
                        {
                            Console.WriteLine($"Insufficient items in the terminal to fulfill the delivery request. Items will not be transferred to the shelves.");
                            return;
                        }
                    }

                    if (remainingQuantity == 0)
                    {
                        break;
                    }
                }
            }

            if (remainingQuantity > 0)
            {
                Console.WriteLine($"Insufficient shelf space of the correct GoodsType to process the entire delivery. Only a partial quantity has been added to the shelves.");
            }
            else
            {
                Console.WriteLine($"Delivery processed successfully.");
            }
        }
    }
}
