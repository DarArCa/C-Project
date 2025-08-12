using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeProject.src.modules.User.Application.Interfaces;
using CoffeProject.src.modules.User.Domain.Entities;
using CoffeProject.src.shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeProject.src.modules.User.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetTaskAsync(int id)
    {
        return await _context.Users
            .FirstOrDefault(u => u.Id == id);
    }

    public async Task<Inumerable<User?>> GetTaskAsync() =>
        await _context.User.TolistAsync();
    public void Add(User entity) =>
        _context.Users.Add(entity);
    public void Remove(User entity) =>
        _context.Users.Remove(entity);
    public void Update(User entity) =>
        _context.SaveChanges();
    public async Task SaveAsync() =>
    await _context.SaveChangesAsync();
}
