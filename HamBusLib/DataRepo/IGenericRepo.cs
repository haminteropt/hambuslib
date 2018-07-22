using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HamBusLib.DataRepo
{
    public interface IGenericRepo
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        List<T> GetAllDataBases<T>() where T : class;
        void CreateDataBase<T>(T dbEntity);
        void DropDataBase<T>(T dbEntity) where T : class;
        bool Auth<T>(T auth) where T : class;
        Task<bool> SaveAll();
    }
}
