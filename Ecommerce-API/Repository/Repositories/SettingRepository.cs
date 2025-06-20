using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SettingRepository : BaseRepository<Setting>, ISettingRepository
    {
        private readonly AppDbContext _context;
        public SettingRepository(AppDbContext context) : base(context)
        {
            _context = context;   
        }
        public async Task DeleteByKey(string key)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(mbox=>mbox.Key==key);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
        }
    }
}
