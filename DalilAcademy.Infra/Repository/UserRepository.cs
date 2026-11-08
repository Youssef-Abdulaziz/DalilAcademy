using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Repository
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email) => db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
  

    }
}
