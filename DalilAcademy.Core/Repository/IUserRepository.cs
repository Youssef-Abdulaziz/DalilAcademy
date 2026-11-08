using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
