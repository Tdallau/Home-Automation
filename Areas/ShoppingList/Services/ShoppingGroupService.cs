using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using HomeAutomation.Areas.ShoppingList.Interfaces;
using HomeAutomation.Areas.ShoppingList.Models;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Models.Database.ShoppingList;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeAutomation.Areas.ShoppingList.Services
{
  public class ShoppingGroupService : IShoppingGroupService
  {
    private readonly MyShoppingListContext _context;
    public ShoppingGroupService(MyShoppingListContext context)
    {
      _context = context;
    }
    public async Task<ShoppingGroup> CreateShoppingGroup(Guid userId, ShoppingGroupRequest shoppingGroupRequest)
    {
      if (String.IsNullOrWhiteSpace(shoppingGroupRequest.Name)) return null;
      var shoppingGroup = new ShoppingGroup()
      {
        Name = shoppingGroupRequest.Name,
        OwnerId = userId,
        Id = Guid.NewGuid(),
      };
      await DeactivateAllShoppingGroup(userId);
      var shoppingGroupUser = new ShoppingGroupUser() 
      {
          Active = true,
          ShoppingGroupId = shoppingGroup.Id,
          UserId = userId
      };
      await _context.AddAsync(shoppingGroup);
      await _context.AddAsync(shoppingGroupUser);
      await _context.SaveChangesAsync();
      return shoppingGroup;
    }

    public async Task<ShoppingGroup> GetActiveShoppingGroupByUserId(Guid userId)
    {
      var shoppingGroupUser = await _context.ShoppingGroupUser.FirstOrDefaultAsync(x => x.UserId == userId && x.Active);
      if (shoppingGroupUser == null) return null;
      return await _context.ShoppingGroup.FirstOrDefaultAsync(x => x.Id == shoppingGroupUser.ShoppingGroupId);
    }

    public async Task<IEnumerable<ShoppingGroup>> GetAllShoppingGroupsByUserId(Guid userId)
    {
      var shoppingGroupsUser = await _context.ShoppingGroup.Where(x => x.OwnerId == userId).ToListAsync();
      return shoppingGroupsUser;
    }

    public async Task SetActiveShoppingGroup(Guid userId, ShoppingGroupRequest shoppingGroupRequest)
    {
      if(shoppingGroupRequest.Id == null || shoppingGroupRequest.Id == Guid.Empty) return;
      await DeactivateAllShoppingGroup(userId);
      var shoppingGroupUser = await _context.ShoppingGroupUser.FirstOrDefaultAsync(x => x.ShoppingGroupId == shoppingGroupRequest.Id && x.UserId == userId);
      if(shoppingGroupUser == null) return;
      shoppingGroupUser.Active = true;
      _context.Update(shoppingGroupUser);
      await _context.SaveChangesAsync();
    }

    private async Task DeactivateAllShoppingGroup(Guid userId)
    {
        IEnumerable<ShoppingGroupUser> shoppingGroupUsers = _context.ShoppingGroupUser.Where(x => x.UserId == userId).Select(x => new ShoppingGroupUser() {
            Id = x.Id,
            ShoppingGroupId = x.ShoppingGroupId,
            UserId = x.UserId,
            Active = false
        });
        _context.UpdateRange(shoppingGroupUsers);
        await _context.SaveChangesAsync();
    }
  }
}