
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picpay_01.Data;
using Picpay_01.Models;
using Picpay_01.Models.Enums;
using Picpay_01.ViewModels;

namespace Picpay_01.Services;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public void ValidateTransaction(Users sender, double amount)
    {
        if (sender.UserType == UserType.Merchant)
            throw new Exception("Lojistas não podem realizar transações.");


        if (sender.Balance.CompareTo(amount) < 0)
            throw new Exception("Saldo insuficiente para realizar a transação");
    }

    public async Task<Users> FindUserById(int id)
    {
        var usersById = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (usersById == null)
            throw new Exception("Usuário não encontrado ou não existe!");

        return usersById;
    }

    public async Task<Users> CreatedUserAsync(UserViewModel data)
    {
        var newUser = new Users
        {
            //Id = data.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Document = data.Document,
            Balance = data.Balance,
            Email = data.Email,
            Password = data.Password,
            UserType = data.UserType
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return newUser;
        
    }

    public async Task<List<Users>>GetAllUsersAsync(
        [FromServices]DataContext context,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 25)
    {
        var count = await context
            .Transactions
            .AsNoTracking()
            .CountAsync();
        
        var allUsers = await context 
            .Users
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip(skip * take)
            .Take(take)
            .ToListAsync();

        if (allUsers == null)
            throw new Exception("Não foi possível buscar os usuários.");

        return allUsers;

    }

    public async Task<Users> DeleteUserAsync(int id)
    {
        var userId = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        _context.Users.Remove(userId);
        await _context.SaveChangesAsync();

        return userId;
    }
}