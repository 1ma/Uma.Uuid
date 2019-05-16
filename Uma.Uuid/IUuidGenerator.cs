namespace Uma.Uuid
{
    public interface IUuidGenerator
    {
        Uuid NewUuid(string name = null);
    }
}
