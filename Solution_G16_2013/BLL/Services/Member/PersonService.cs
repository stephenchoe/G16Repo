using System.Collections.Generic;
using System.Linq;
using G16_2013.DAL.Infrastructure;
using G16_2013.DAL.Repository;
using G16_2013.Models.MemberModel;

using System;

namespace G16_2013.BLL.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAllPeople();
        Person GetPersonById(int id);
        IEnumerable<Person> GetPeopleByKeyWord(int searchWay, string keyWord);

        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void RemovePerson(int id);
        void SavePerson();
    }
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
       
        private readonly IUnitOfWork unitOfWork;
        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            this.personRepository = personRepository;         
            this.unitOfWork = unitOfWork;
        }


        #region IPersonService Members
        public IEnumerable<Person> GetAllPeople()
        {
            var people = personRepository.GetAll();
            return people.Where(p => !p.Removed);            
        }

        public Person GetPersonById(int id)
        {
            var person = personRepository.GetById(id);
            if (person.Removed) return null;
            return person;
        }

        public IEnumerable<Person> GetPeopleByKeyWord(int searchWay, string keyWord)
        {
            return null;
            //var people = GetAllPeople();
            //switch (searchWay)
            //{
            //    case 0:
            //        return people.Where(m => !m.Removed && m.Name.Contains(keyWord)).ToList();

            //    case 1:
            //        return people.Where(m => !m.Removed && m.ContactInfo.Phone.Contains(keyWord)).ToList();

            //    case 2:
            //        return people.Where(m => !m.Removed && m.ContactInfo.Email.Contains(keyWord)).ToList();

            //    case 3:

            //        var allAccounts = GetAllAccounts();
            //        var accounts = allAccounts.Where(a => !a.Removed && a.AccountNumber.Contains(keyWord)).ToList();
            //        if (accounts == null) return null;

            //        var accountOwners = (from a in accounts select a.Owner).Distinct();
            //        var activeOwners = accountOwners.Where(p => !p.Removed);
            //        return accountOwners.ToList();

            //    default:
            //        return null;
            //}
        }

        public void CreatePerson(Person person)
        {
            personRepository.Add(person);
            SavePerson();           
        }
        public void UpdatePerson(Person person)
        {
            personRepository.Update(person);
            SavePerson();
        }
        public void RemovePerson(int id)
        {
            var person = personRepository.GetById(id);
            if (person == null)
            { 
              //do something
            }
            if (!person.Removed)
            {
                person.Removed = true;
            }

            UpdatePerson(person);
           
        }

        public void SavePerson()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
