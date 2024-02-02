namespace WarehouseSimulation
{
    /// <summary>
    /// Represents a schedule for managing pickups in a warehouse simulation.
    /// </summary>
    public class PickupSchedule
    {
        private List<Pickup> pickups = new List<Pickup>();

        /// <summary>
        /// Adds a single pickup to the schedule.
        /// <param name="pickup">The pickup to be added.</param>
        /// </summary>
        public void AddPickup(Pickup pickup)
        {
            pickups.Add(pickup);
        }

        /// <summary>
        /// Retrieves the list of pickups in the schedule.     
        /// <returns>The list of pickups in the schedule.</returns>
        /// </summary>
        public List<Pickup> GetPickups()
        {
            return pickups;
        }

        /// <summary>
        /// Adds weekly recurring pickups to the schedule.
        /// <param name="daysBetweenPickups">The number of days between each weekly pickup.</param>
        /// <param name="pickup">The base pickup to be scheduled weekly.</param>
        /// </summary>
        public void AddWeeklyPickup(int daysBetweenPickups, Pickup pickup)
        {
            for (int day = pickup.SimulationDay; day <= 7 * 1000; day += daysBetweenPickups)
            {
                pickups.Add(new Pickup(day, pickup.GoodsType, pickup.Quantity, pickup.Item));
            }
        }
    }
}
