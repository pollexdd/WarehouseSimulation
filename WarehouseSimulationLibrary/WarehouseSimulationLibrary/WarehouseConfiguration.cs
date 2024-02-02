namespace WarehouseSimulation
{
    /// <summary>
    /// Represents the configuration settings for a warehouse.
    /// </summary>
    public class WarehouseConfiguration
    {
        /// <summary>
        /// Gets or sets whether the warehouse has cool storage capabilities.
        /// </summary>
        public bool IsCoolStorage { get; set; }

        /// <summary>
        /// Gets or sets whether the warehouse has dry storage capabilities.
        /// </summary>
        public bool IsDryStorage { get; set; }

        /// <summary>
        /// Gets or sets whether the warehouse handles hazardous goods.
        /// </summary>
        public bool IsHazardous { get; set; }

        /// <summary>
        /// Gets or sets the maximum capacity of the warehouse's terminal for temporary storage.
        /// </summary>
        public int TerminalCapacity { get; set; }
    }
}
