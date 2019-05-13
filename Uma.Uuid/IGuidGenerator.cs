using System;

namespace Uma.Uuid
{
    public interface IGuidGenerator
    {
        Guid generate(string name = null);
    }
}
