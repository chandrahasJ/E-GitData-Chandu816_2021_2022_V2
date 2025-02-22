﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTraderApp.Domain.Models;
using SimpleTraderApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTraderApp.EFCore.Services.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly SimpleTraderAppDbContextFactory _contextFactory;

        public NonQueryDataService(SimpleTraderAppDbContextFactory dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (SimpleTraderAppDbContext traderAppDbContext = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await traderAppDbContext.Set<T>().AddAsync(entity);
                await traderAppDbContext.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (SimpleTraderAppDbContext traderAppDbContext = _contextFactory.CreateDbContext())
            {

                T entity = await traderAppDbContext.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                traderAppDbContext.Set<T>().Remove(entity);
                await traderAppDbContext.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (SimpleTraderAppDbContext traderAppDbContext = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                traderAppDbContext.Set<T>().Update(entity);
                await traderAppDbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
}
