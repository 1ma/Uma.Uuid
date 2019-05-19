namespace Uma.Uuid
{
    /// <summary>
    /// This is the contract for classes that generate Uuids.
    /// </summary>
    public interface IUuidGenerator
    {
        /// <summary>
        /// Returns a brand new Uuid.
        /// </summary>
        /// <remarks>
        /// The optional string parameter is for name-based generators,
        /// such as the Version5Generator.
        /// </remarks>
        Uuid NewUuid(string name = null);
    }
}
