using Data.DBContext;
using Data.Entities;
using MongoDB.Driver;

namespace Data.Seeds
{
    public partial class Seeder
    {
        private async static Task SeedTasks(DbContext _context)
        {
            if (!_context.UserTasks.AsQueryable().Any())
            {
                var tasks = new List<UserTask>
               {
                   new UserTask
                   {
                       Title="Task1",
                       Description="123"
                   }
               };

                await _context.UserTasks.InsertManyAsync(tasks);
            }
        }
    }
}
