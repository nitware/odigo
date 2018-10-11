using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;

namespace Odigo.Business.Interfaces
{
    public interface IRepository
    {
        OdigoEntities DbContext { get; }

        

        int GetMaxValueBy<E>(Func<E, int> match) where E : class;
        long GetMaxValueBy<E>(Func<E, long> match) where E : class;
        int Count<E>() where E : class;
        int Count<E>(Func<E, bool> match) where E : class;
        E GetSingleBy<E>(Func<E, bool> match) where E : class;
        List<T> GetAll<T, E>() where E : class;
        T GetModelBy<T, E>(Func<E, bool> selector = null) where E : class;
        List<E> FindAllBy<E>(Func<E, bool> match) where E : class;
        List<T> GetModelsBy<T, E>(Expression<Func<E, bool>> selector = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;
        List<E> GetBy<E>(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;
        List<T> GetTopBy<T, E>(int top, Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;

        Task<List<T>> GetAllAsync<T, E>() where E : class;
        //Task<E> GetSingleByAsync<E>(Func<E, bool> match) where E : class;
        //Task<T> GetModelByAsync<T, E>(Func<E, bool> selector = null) where E : class;
        Task<List<T>> GetModelsByAsync<T, E>(Expression<Func<E, bool>> selector = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;
        Task<List<T>> GetTopByAsync<T, E>(int top, Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;
        Task<List<E>> GetByAsync<E>(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;
        
        void Add<T>(T model);
        int Add<T>(List<T> models);
        T Create<T>(T model);
        int Create<T>(List<T> models);
        bool Delete<E>(Func<E, bool> selector) where E : class;
        bool Remove<T>(T model);
        bool Remove<T>(List<T> models);
        bool Modify<T>(T model);
        bool Modify<T>(List<T> models);
        bool Update<E>(E e) where E : class;
        void Update<E>(List<E> es) where E : class;

        int Save();


    }


}
