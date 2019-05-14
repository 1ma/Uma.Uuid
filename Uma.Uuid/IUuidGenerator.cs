namespace Uma.Uuid
{
    public interface IUuidGenerator
    {
        Uuid Generate(string name = null);
    }
}
