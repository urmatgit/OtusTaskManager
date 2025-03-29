using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Data
{
    public class TaskboardDbInitializer: IDbInitializer
    {
        private readonly TaskboardDbContext _dataContext;
        

        public TaskboardDbInitializer(TaskboardDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task InitializeDb()
        {
            //_dataContext.Database.EnsureDeleted();
            //_dataContext.Database.EnsureCreated();
            // если есть миграция 
            if (_dataContext.Database.GetPendingMigrations().Any())
            {
                await _dataContext.Database.MigrateAsync();
            }
        
        }
    }
}
