using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using YanakApp.Contracts;
using YanakApp.DTOs.Product;
using YanakApp.DTOs.User;
using YanakApp.Models;

namespace YanakApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly ShopDbContext _context;
        public UsersService(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetUsersDto>> GetUsersAsync()
        {
            return await _context.Users
                .Select(us => new GetUsersDto(
                    us.FirstName,
                    us.LastName,
                    us.Email
                    ))
                .ToListAsync();

        }

        public async Task<GetUserDto?>GetUserAsync(int id)
        {
            var user = await _context.Users
                .Where(us => us.Id == id)
                .Select(el => new GetUserDto(
                    el.Id,
                    el.FirstName,
                    el.LastName,
                    el.Email,
                    el.Products.Select(pr => new GetProductSlimDto(
                        pr.Name,
                        pr.Description
                        )).ToList()
                    ))
                .FirstOrDefaultAsync();

            return user ?? null;

        }


        public async Task<GetUserDto> CreateUserAsync(CreateUserDto createDto)
        {
            var user = new User
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new GetUserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                []
            );


        }


        public async Task UpdateUserAsync(int id,UpdateUserDto updateDto)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException("User not found");

            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.Email = updateDto.Email;
            _context.Entry(user).State = EntityState.Modified;
             await _context.SaveChangesAsync();
    

        }


        public async Task UserBuyAsync(int userId,int productId)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException("User not found");
            var product = await _context.Products.FindAsync(productId) ?? throw new KeyNotFoundException("Product not found");

            user.Products.Add(product);
            await _context.SaveChangesAsync();


        }


        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException("User not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }


    }
}
