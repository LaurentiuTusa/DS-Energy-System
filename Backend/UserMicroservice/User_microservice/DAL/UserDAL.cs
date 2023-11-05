using DAL.Repository;
using DAL.Repository.Models;


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

        public User GetUserByEmail(string email)
        { 
            var db = new UserDbContext();
            User u = new User();

            u = db.Users.FirstOrDefault(u => u.Email == email);

            return u;
        }

        public void AddUser(User user)
        {
            var db = new UserDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var db = new UserDbContext();
            User u = new User();
            u = db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (u == null)
            {
                throw new Exception("User to update Not Found");
            }
            u.Name = user.Name;
            u.Email = user.Email;
            u.Password = user.Password;
            u.Role = user.Role;
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