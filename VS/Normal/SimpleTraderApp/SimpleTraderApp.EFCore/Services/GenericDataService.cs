﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTraderApp.Domain.Models;
using SimpleTraderApp.Domain.Services;
using SimpleTraderApp.EFCore.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTraderApp.EFCore.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly SimpleTraderAppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(SimpleTraderAppDbContextFactory dbContextFactory)
        {
            _contextFactory = dbContextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(_contextFactory);
        }

        public  async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async  Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<T> Get(int id)
        {
            using (SimpleTraderAppDbContext traderAppDbContext = _contextFactory.CreateDbContext())
            {
                T entity = await traderAppDbContext.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (SimpleTraderAppDbContext traderAppDbContext = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entity = await traderAppDbContext.Set<T>().ToListAsync();
                return entity;
            }
        }
    }
}
