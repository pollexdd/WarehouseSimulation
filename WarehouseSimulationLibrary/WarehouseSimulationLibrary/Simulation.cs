using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseSimulation
{
    /// <summary>
    /// Represents the simulation of warehouse operations over a period of time.
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// Gets or sets the current day of the simulation.
        /// </summary>
        public int CurrentSimulationDay { get; private set; }

        /// <summary>
        /// Simulates a single day of warehouse operations, including processing deliveries, pickups, and generating daily reports.
        /// <param name="warehouse">The warehouse to be simulated.</param>
        /// </summary>
        public void SimulateDay(Warehouse warehouse)
        {
            CurrentSimulationDay++;

            var deliveriesForToday = warehouse.DeliverySchedule.GetDeliveries().Where(delivery => delivery.SimulationDay == CurrentSimulationDay).ToList();
            foreach (var delivery in deliveriesForToday)
            {
                warehouse.ProcessDelivery(delivery);
            }

            var pickupsForToday = warehouse.PickupSchedule.GetPickups().Where(pickup => pickup.SimulationDay == CurrentSimulationDay).ToList();
            foreach (var pickup in pickupsForToday)
            {
                warehouse.ProcessPickup(pickup);
            }

            Console.WriteLine($"Simulation Day: {CurrentSimulationDay}");
            GenerateDailyShelfReport(warehouse);
        }

        /// <summary>
        /// Generates a daily report of the items on each shelf in the warehouse.
        /// <param name="warehouse">The warehouse for which the report is generated.</param>
        /// </summary>
        public void GenerateDailyShelfReport(Warehouse warehouse)
        {
            Console.WriteLine("Daily Shelf Report:");

            foreach (var shelf in warehouse.Shelves)
            {
                Console.WriteLine($"Shelf '{shelf.Id}' - Capacity: {shelf.Capacity}, Goods Type: {shelf.GoodsType}");

                if (shelf.Items.Any())
                {
                    Console.WriteLine("Items:");

                    Dictionary<string, int> itemCounts = new Dictionary<string, int>();

                    foreach (var item in shelf.Items)
                    {
                        if (itemCounts.ContainsKey(item.Name))
                        {
                            itemCounts[item.Name]++;
                        }
                        else
                        {
                            itemCounts[item.Name] = 1;
                        }
                    }

                    foreach (var itemName in itemCounts.Keys)
                    {
                        int itemCount = itemCounts[itemName];
                        Console.WriteLine($"  - {itemName}");
                    }
                }
                else
                {
                    Console.WriteLine("No items in the shelf.");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Runs the simulation for a specified number of days.
        /// <param name="warehouse">The warehouse to be simulated.</param>
        /// <param name="daysToSimulate">The number of days to simulate.</param>
        /// </summary>
        public void SimulationRun(Warehouse warehouse, int daysToSimulate)
        {
            for (int day = 1; day <= daysToSimulate; day++)
            {
                SimulateDay(warehouse);
            }
        }
    }
}
