using G16_2013.DAL.Infrastructure;
using G16_2013.Models.MemberModel;

namespace G16_2013.DAL.Repository
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }
    }
   public interface IPersonRepository : IRepository<Person>
   {

   }

   public class PersonContactInfoRepository : RepositoryBase<PersonContactInfo>, IPersonContactInfoRepository
   {
       public PersonContactInfoRepository(IDatabaseFactory databaseFactory)
           : base(databaseFactory)
       {
       }
   }
   public interface IPersonContactInfoRepository : IRepository<PersonContactInfo>
   {

   }

}
