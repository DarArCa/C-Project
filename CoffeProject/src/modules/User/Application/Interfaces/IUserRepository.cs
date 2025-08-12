using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeProject.src.Modules.User.Domain.Entities;

namespace CoffeProject.src.Modules.User.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User?>> GetAllAsync();
    void Add(User entity);
    void Remove(User entity);
    void Update(User entity);
    Task SaveAsync(); 
}
