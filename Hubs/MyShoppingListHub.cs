using System;
using System.Threading.Tasks;
using HomeAutomation.Helpers.Contexts;
using HomeAutomation.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HomeAutomation.Hubs
{
  public class MyShoppingListHub : Hub<IMyShoppingListHub>
  {
    private readonly MyShoppingListContext _context;
    public MyShoppingListHub(MyShoppingListContext context)
    {
      _context = context;
    }

    public async Task ConnectToShoppingGroup(Guid groupName)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, groupName.ToString());
    }

    public async Task DisconectFromShoppingGroup(Guid groupName)
    {
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName.ToString());
    }
  }
}