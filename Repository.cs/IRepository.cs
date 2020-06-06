using HealthCatalystBackend.Models;
using System.Collections.Generic;
 
namespace HealthCatalystBackend.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        void CreateAccount(T item);
    }
}