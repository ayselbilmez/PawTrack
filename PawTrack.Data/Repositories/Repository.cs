﻿using Microsoft.EntityFrameworkCore;
using PawTrack.Data.Context;
using PawTrack.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly PawTrackDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(PawTrackDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
            
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            if (softDelete)
            {
               entity.ModifiedDate = DateTime.Now;
               entity.IsActive = false;
               _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
         
            
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            entity.IsActive = false;
            _dbSet.Update(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate); ;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbSet : _dbSet.Where(predicate);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            
        }
    }
}
