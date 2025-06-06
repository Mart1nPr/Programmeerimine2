﻿using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            _context.Users.Add(user);  
            await _context.SaveChangesAsync();  
        }

        public async Task<PagedResult<User>> List(int page, int pageSize, UsersSearch search = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Keyword))
            {
                query = query.Where(user => user.Email.Contains(search.Keyword) || user.Name.Contains(search.Keyword));
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(User user)
        {
            if (user.Id == 0)
            {
                _context.Add(user);
            }
            else
            {
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser != null)
                {
                    // Update properties explicitly
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;
                    existingUser.Registration_Time = user.Registration_Time;
                }
                else
                {
                    // If not found, add as new or throw
                    _context.Add(user);
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
