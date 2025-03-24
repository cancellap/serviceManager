﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domaiin.Interfaces
{
    public interface IBaseLocalRepository<TEntity> where TEntity : IEntityLocal
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> FindOneId(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> deleteAsync(TEntity entity);
    }
}
