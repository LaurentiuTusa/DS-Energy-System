using User_microservice.Repository;
using User_microservice.Repository.Models;

namespace DAL
{
    public class UserDAL
    {

        public List<User> GetAllUsers()
        {
            var db = new UserDbContext();
            return db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            var db = new UserDbContext();
            User u = new User();

            u = db.Users.FirstOrDefault(u => u.Id == id);

            return u;
        }

        public void AddUser(User user)
        {
            var db = new UserDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void DeleteUserById(int id)
        {
            var db = new UserDbContext();
            User u = new User();
            u = db.Users.FirstOrDefault(u => u.Id == id);

            if (u == null)
            {
                throw new Exception("User to delete Not Found");
            }

            db.Users.Remove(u);
            db.SaveChanges();
        }
    }
}