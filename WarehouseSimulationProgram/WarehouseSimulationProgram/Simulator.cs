using WarehouseSimulation;

namespace Simulator
{
    class Simulator
    {
        /// <summary>
        /// The entry point of the simulator program.
        /// <param name="args">Command-line arguments (not used).</param>
        /// </summary>
        static void Main(string[] args)
        {
            // Configure the warehouse with specific settings
            WarehouseConfiguration warehouseConfig = new WarehouseConfiguration
            {
                IsCoolStorage = true,
                IsDryStorage = true,
                IsHazardous = false,
                TerminalCapacity = 1000
            };

            // Create a warehouse instance based on the configured settings
            Warehouse myWarehouse = new Warehouse(warehouseConfig);

            // Add shelves to the warehouse with specific characteristics
            myWarehouse.AddShelf("A1", 500, GoodsType.DryGoods, 1, 1);
            myWarehouse.AddShelf("A2", 500, GoodsType.DryGoods, 1, 1);
            myWarehouse.AddShelf("B1", 500, GoodsType.Refrigerated, 1, 1);

            // Add items to the warehouse with specific names and goods types
            myWarehouse.AddItem("Rice", GoodsType.DryGoods);
            myWarehouse.AddItem("Milk", GoodsType.Refrigerated);

            // Add one-time deliveries to the warehouse
            myWarehouse.AddDelivery(1, GoodsType.DryGoods, 200, "Rice");
            myWarehouse.AddDelivery(3, GoodsType.Refrigerated, 150, "Milk");

            // Add one-time pickup requests to the warehouse
            myWarehouse.AddPickup(2, GoodsType.DryGoods, 150, "Rice");
            myWarehouse.AddPickup(4, GoodsType.Refrigerated, 140, "Milk");

            // Add recurring weekly deliveries and pickups to the warehouse
            myWarehouse.AddWeeklyDelivery(7, 7, GoodsType.DryGoods, 250, "Rice");
            myWarehouse.AddWeeklyPickup(7, 7, GoodsType.DryGoods, 200, "Rice");
            myWarehouse.AddWeeklyDelivery(7, 7, GoodsType.Refrigerated, 250, "Milk");

            // Run the simulation for a specified number of days
            myWarehouse.SimulationRun(5);

            // Print the location history for a specific item
            myWarehouse.PrintItemHistory("Rice_100");
        }
    }
}
