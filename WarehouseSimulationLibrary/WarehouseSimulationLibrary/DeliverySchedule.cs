namespace WarehouseSimulation
{
    /// <summary>
    /// Represents a schedule for managing deliveries in a warehouse simulation.
    /// </summary>
    public class DeliverySchedule
    {
        private List<Delivery> deliveries = new List<Delivery>();

        /// <summary>
        /// Adds a single delivery to the schedule.        
        /// <param name="delivery">The delivery to be added.</param>
        /// </summary>
        public void AddDelivery(Delivery delivery)
        {
            deliveries.Add(delivery);
        }

        /// <summary>
        /// Adds weekly recurring deliveries to the schedule.      
        /// <param name="daysBetweenDeliveries">The number of days between each weekly delivery.</param>
        /// <param name="delivery">The base delivery to be scheduled weekly.</param>
        /// </summary>
        public void AddWeeklyDelivery(int daysBetweenDeliveries, Delivery delivery)
        {
            for (int day = delivery.SimulationDay; day <= 7 * 1000; day += daysBetweenDeliveries)
            {
                deliveries.Add(new Delivery(day, delivery.GoodsType, delivery.Quantity, delivery.Item));
            }
        }

        /// <summary>
        /// Retrieves the list of deliveries in the schedule.        
        /// <returns>The list of deliveries in the schedule.</returns>
        /// </summary>
        public List<Delivery> GetDeliveries()
        {
            return deliveries;
        }
    }
}
