using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.DAL.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private G16MemberEntities dataContext;
        public G16MemberEntities Get()
        {
            return dataContext ?? (dataContext = new G16MemberEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
