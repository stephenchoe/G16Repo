using System;

namespace G16_2013.DAL.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        G16MemberEntities Get();
    }
}
