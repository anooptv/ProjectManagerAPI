﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private WebAPIDBEntities context;
        private DbSet<T> dbSet;
        public Repository()
        {
            context = new WebAPIDBEntities();
            dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }
        public T Insert(T obj)
        {
            dbSet.Add(obj);
            Save();
            return obj;
        }
        public void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);

            }
            dbSet.Remove(entityToDelete);
            Save();
        }
        public T Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;

            Save();
            return obj;
        }
        public void Save()
        {
            try
            {
                context.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    }
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
