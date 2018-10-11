using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Business.Interfaces;
using System.Data.Entity;
using Odigo.Model.Translator;
using System.Linq.Expressions;
using System.Transactions;

namespace Odigo.Data
{
    public class Repository : IRepository
    {
        private OdigoEntities _context;

        private const string NoItemModified = "No item modified!";
        private const string NoItemFound = "No item found to modified!";
        private const string DuplicateKeyDetected = "Cannot insert duplicate key";
        private const string UpdateException = "Operation failed due to update exception!";
        private const string AlreadyInUse = "The DELETE statement conflicted with the REFERENCE constraint";
        private const string ArgumentNullException = "Null object argument. Please contact your system administartor";
        private const string AlreadyInUseMessage = "This item is currently associated with other items in the system and cannot be removed at this time. Kindly remove all associated items prior to its removal.";
        private const string DuplicateKeyDetectedMessage = "Duplicate ID detected! Record already exist with the same ID! kindly correct and try again.";
        private const string TranslatorNullException = "Translator cannot be null! Please contact your system administrator.";
        private const string UniqueKeyDetectedMessage = "Violation of UNIQUE KEY constraint. Cannot insert duplicate key in object. The statement has been terminated.";

        public Repository() : this(new OdigoEntities()) { }
        public Repository(OdigoEntities context)
        {
            _context = context;
        }

        public OdigoEntities DbContext
        {
            get { return _context; }
        }
        
        public int GetMaxValueBy<E>(Func<E, int> match) where E : class
        {
            try
            {
                int maximum = 0;
                DbSet<E> es = _context.Set<E>();

                if (es != null && es.Count() > 0)
                {
                    maximum = _context.Set<E>().Max(match);
                }

                return maximum;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long GetMaxValueBy<E>(Func<E, long> match) where E : class
        {
            try
            {
                long maximum = 0;
                DbSet<E> es = _context.Set<E>();

                if (es != null && es.Count() > 0)
                {
                    maximum = _context.Set<E>().Max(match);
                }

                return maximum;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Count<E>() where E : class
        {
            try
            {
                return _context.Set<E>().Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Count<E>(Func<E, bool> match) where E : class
        {
            try
            {
                int totalCount = 0;
                List<E> es = FindAllBy<E>(match);
                if (es != null)
                {
                    totalCount = es.Count();
                }

                return totalCount;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<T>> GetAllAsync<T, E>() where E : class
        {
            try
            {
                var entities = await _context.Set<E>().ToListAsync();
                if (entities == null || entities.Count <= 0)
                {
                    entities = new List<E>();
                }

                Type type = entities[0].GetType().BaseType;
                if (type.Equals(typeof(object)))
                {
                    type = entities[0].GetType();
                }

                dynamic translator = TranslatorFactory.Create(type.Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<T> GetAll<T, E>() where E : class
        {
            try
            {
                var entities = _context.Set<E>().ToList();
                if (entities == null || entities.Count <= 0)
                {
                    return default(List<T>);
                }

                Type type = entities[0].GetType().BaseType;
                if (type.Equals(typeof(object)))
                {
                    type = entities[0].GetType();
                }

                dynamic translator = TranslatorFactory.Create(type.Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public T GetModelBy<T, E>(Func<E, bool> selector = null) where E : class
        {
            try
            {
                if (selector == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                E entity = GetSingleBy(selector);
                if (entity == null)
                {
                    return default(T);
                }

                string typeName = selector.GetType().GenericTypeArguments[0].Name;
                dynamic translator = TranslatorFactory.Create(typeName);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public async Task<T> GetModelByAsync<T, E>(Func<E, bool> selector = null) where E : class
        //{
        //    try
        //    {
        //        if (selector == null)
        //        {
        //            throw new ArgumentNullException(ArgumentNullException);
        //        }

        //        E entity = await GetSingleByAsync(selector);
        //        if (entity == null)
        //        {
        //            return default(T);
        //        }

        //        string typeName = selector.GetType().GenericTypeArguments[0].Name;
        //        dynamic translator = TranslatorFactory.Create(typeName);
        //        if (translator == null)
        //        {
        //            throw new Exception(TranslatorNullException);
        //        }

        //        return translator.Translate(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public E GetSingleBy<E>(Func<E, bool> match) where E : class
        {
            try
            {
                return _context.Set<E>().SingleOrDefault(match);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public async Task<E> GetSingleByAsync<E>(Func<E, bool> match) where E : class
        //{
        //    try
        //    {
        //        return await _context.Set<E>().SingleOrDefault(match);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<E> FindAllBy<E>(Func<E, bool> match) where E : class
        {
            try
            {
                return _context.Set<E>().Where(match).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> GetModelsBy<T, E>(Expression<Func<E, bool>> selector = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                if (selector == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                List<E> entities = GetBy(selector, orderBy, includeProperties);
                if (entities == null)
                {
                    return default(List<T>);
                }

                string typeName = selector.Parameters[0].Type.Name;
                dynamic translator = TranslatorFactory.Create(typeName);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<T>> GetModelsByAsync<T, E>(Expression<Func<E, bool>> selector = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                if (selector == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                List<E> entities = await GetByAsync(selector, orderBy, includeProperties);
                if (entities == null)
                {
                    return default(List<T>);
                }

                string typeName = selector.Parameters[0].Type.Name;
                dynamic translator = TranslatorFactory.Create(typeName);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<T> GetTopBy<T, E>(int top, Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                List<E> list = null;
                List<E> entities = GetBy<E>(filter, orderBy, includeProperties);
                if (entities != null && entities.Count() > 0)
                {
                    list = entities.Take(top).ToList();
                }

                string typeName = filter.Parameters[0].Type.Name;
                dynamic translator = TranslatorFactory.Create(typeName);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<T>> GetTopByAsync<T, E>(int top, Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                List<E> list = null;
                List<E> entities = await GetByAsync<E>(filter, orderBy, includeProperties);
                if (entities != null && entities.Count() > 0)
                {
                    list = entities.Take(top).ToList();
                }

                string typeName = filter.Parameters[0].Type.Name;
                dynamic translator = TranslatorFactory.Create(typeName);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                return translator.Translate(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<E> GetTopByHelper<E>(IQueryable<E> query) where E : class
        //{
        //    try
        //    {

        //        //if (filter != null)
        //        //{
        //        //    query = query.Where(filter);
        //        //}

        //        //foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        //{
        //        //    query = query.Include(includeProperty);
        //        //}

        //        //if (orderBy != null)
        //        //{
        //        //    return orderBy(query).ToList();
        //        //}
        //        //else
        //        //{
        //        //    return query.ToList();
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public List<E> GetBy<E>(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                IQueryable<E> query = _context.Set<E>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<List<E>> GetByAsync<E>(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                IQueryable<E> query = _context.Set<E>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private E AddEntity<E>(E e) where E : class
        {
            try
            {
                return _context.Set<E>().Add(e);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private int AddEntities<E>(List<E> es) where E : class
        {
            try
            {
                //foreach (E e in es)
                //{
                //    _context.Set<E>().Add(e);
                //}

                IEnumerable<E> entities = _context.Set<E>().AddRange(es);

                return entities != null && entities.Count() > 0 ? entities.Count() : 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Add<T>(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                dynamic translator = TranslatorFactory.Create(model.GetType().Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                var entity = translator.Translate(model);
                var addedEntity = AddEntity(entity);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int Add<T>(List<T> models)
        {
            try
            {
                if (models == null || models.Count <= 0)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                dynamic translator = TranslatorFactory.Create(models[0].GetType().Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                var entities = translator.Translate(models);

                return AddEntities(entities);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual T Create<T>(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                dynamic translator = TranslatorFactory.Create(model.GetType().Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                var entity = translator.Translate(model);
                var addedEntity = AddEntity(entity);

                Save();

                return translator.Translate(addedEntity);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (System.Data.UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(DuplicateKeyDetected))
                {
                    throw new Exception(DuplicateKeyDetectedMessage);
                }

                throw;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Create<T>(List<T> models)
        {
            try
            {
                if (models == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                dynamic translator = TranslatorFactory.Create(models[0].GetType().Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                var entities = translator.Translate(models);

                AddEntities(entities);

                return Save();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (System.Data.UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(DuplicateKeyDetected))
                {
                    throw new Exception(DuplicateKeyDetectedMessage);
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemoveEntity<E>(E e) where E : class
        {
            try
            {
                DbSet<E> dbSet = _context.Set<E>();
                if (_context.Entry(e).State == EntityState.Detached)
                {
                    dbSet.Attach(e);
                }

                dbSet.Remove(e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete<E>(Func<E, bool> selector) where E : class
        {
            try
            {
                DbSet<E> dbSet = _context.Set<E>();
                IEnumerable<E> records = from x in dbSet.Where<E>(selector) select x;

                foreach (E e in records)
                {
                    if (_context.Entry(e).State == EntityState.Detached)
                    {
                        dbSet.Attach(e);
                    }

                    dbSet.Remove(e);
                }

                return Save() > 0 ? true : false;
            }
            catch (System.Data.UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(AlreadyInUse))
                {
                    throw new Exception(AlreadyInUseMessage);
                }

                throw new System.Data.UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public bool Remove<T>(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                dynamic translator = TranslatorFactory.Create(model.GetType().Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                var entity = translator.Translate(model);

                RemoveEntity(entity);

                return Save() > 0 ? true : false;
            }
            catch (System.Data.UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(AlreadyInUse))
                {
                    throw new Exception(AlreadyInUseMessage);
                }

                throw new System.Data.UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        //private void Delete<E>(List<E> es) where E : class
        //{
        //    try
        //    {
        //        DbSet<E> dbSet = _context.Set<E>();
        //        foreach (E e in es)
        //        {
        //            if (_context.Entry(e).State == EntityState.Detached)
        //            {
        //                dbSet.Attach(e);
        //            }

        //            dbSet.Remove(e);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool Remove<T>(List<T> models)
        {
            try
            {
                if (models == null || models.Count <= 0)
                {
                    throw new Exception(NoItemFound);
                }

                using (TransactionScope transaction = new TransactionScope())
                {
                    foreach (T t in models)
                    {
                        bool deleted = Remove(t);
                        if (deleted == false)
                        {
                            throw new Exception("Error occured during object deletion! Please try again, and contact your system administrator after three unsuccessful trial.");
                        }
                    }

                    transaction.Complete();
                }

                return true;
            }
            catch (System.Data.UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(AlreadyInUse))
                {
                    throw new Exception(AlreadyInUseMessage);
                }

                throw new System.Data.UpdateException(UpdateException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify<T>(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(ArgumentNullException);
                }

                dynamic translator = TranslatorFactory.Create(model.GetType().Name);
                if (translator == null)
                {
                    throw new Exception(TranslatorNullException);
                }

                var entity = translator.Translate(model);
                if (entity == null)
                {
                    throw new Exception(NoItemFound);
                }

                return Update(entity);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (System.Data.UpdateException)
            {
                throw new System.Data.UpdateException(UpdateException);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Modify<T>(List<T> models)
        {
            try
            {
                if (models == null || models.Count <= 0)
                {
                    throw new Exception(NoItemFound);
                }

                using (TransactionScope transaction = new TransactionScope())
                {
                    foreach (T t in models)
                    {
                        bool modified = Modify(t);
                        if (modified == false)
                        {
                            throw new Exception("Error occured during object modification! Please try again, and contact your system administrator after three unsuccessful trial.");
                        }
                    }

                    transaction.Complete();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update<E>(E e) where E : class
        {
            try
            {
                if (e == null)
                {
                    throw new NullReferenceException(ArgumentNullException);
                }

                DbSet<E> dbSet = _context.Set<E>();
                if (_context.Entry(e).State == EntityState.Detached)
                {
                    dbSet.Attach(e);
                }

                _context.Entry(e).State = EntityState.Modified;

                int modifiedRecordCount = Save();
                if (modifiedRecordCount <= 0)
                {
                    throw new Exception(NoItemModified);
                }

                return true;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (System.Data.UpdateException)
            {
                throw new System.Data.UpdateException(UpdateException);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateException(UniqueKeyDetectedMessage);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        public void Update<E>(List<E> es) where E : class
        {
            try
            {
                DbSet<E> dbSet = _context.Set<E>();
                foreach (E e in es)
                {
                    dbSet.Attach(e);
                    _context.Entry(e).State = EntityState.Modified;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

      






    }
}
