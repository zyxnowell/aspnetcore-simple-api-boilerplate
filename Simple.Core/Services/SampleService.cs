using Microsoft.EntityFrameworkCore;
using Simple.Core.Interfaces;
using Simple.Intrastructure.Context;
using Simple.Intrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Core.Services
{
    public class SampleService : ISampleService
    {
        private readonly AppDBContext _dbContext;
        public SampleService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SampleEntity>> GetAll()
        {
            return await _dbContext.SampleEntity.ToListAsync();
        }
    }
}
