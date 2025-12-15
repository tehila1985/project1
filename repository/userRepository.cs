using Microsoft.EntityFrameworkCore;
using Model;

using System.Collections.Specialized;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        myDBContext dbContext;
        public UserRepository(myDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async  Task<User?> GetUserById(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }
        public async Task<User> AddNewUser(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Login(User value)
        {
            return dbContext.Users.FirstOrDefault(user => user.Password == value.Password && user.Gmail == value.Gmail);
        }

        public async Task<User> update(int id, User value)
        {
            dbContext.Users.Update(value);
            await dbContext.SaveChangesAsync();
            return value;
        }
        public void Delete(int id)
        {
        }
    }
}
