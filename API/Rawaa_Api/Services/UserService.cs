
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Services
{
    public class UserService//: IUserService
    {
    //    private ApplicationDbContext _context;

    //    public UserService(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public void Authenticate(User model)
    //    {
    //        var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

    //        // validate
    //        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
    //            throw new AppException("Username or password is incorrect");


    //        return response;
    //    }

    //    public IEnumerable<User> GetAll()
    //    {
    //        return _context.Users;
    //    }

    //    public User GetById(int id)
    //    {
    //        return getUser(id);
    //    }

    //    public void Register(User model)
    //    {
    //        // validate
    //        if (_context.Users.Any(x => x.Email == model.Email))
    //        {

    //        }
    //        // return in valid

    //        var user = new User();
    //        // hash password
    //        user.PasswordHash = BCrypt.HashPassword(model.Password);

    //        // save user
    //        _context.Users.Add(user);
    //        _context.SaveChanges();
    //    }

    //    public void Update(int id, UpdateRequest model)
    //    {
    //        var user = getUser(id);

    //        // validate
    //        if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
    //            throw new AppException("Username '" + model.Username + "' is already taken");

    //        // hash password if it was entered
    //        if (!string.IsNullOrEmpty(model.Password))
    //            user.PasswordHash = BCrypt.HashPassword(model.Password);

    //        // copy model to user and save
    //        _mapper.Map(model, user);
    //        _context.Users.Update(user);
    //        _context.SaveChanges();
    //    }

    //    public void Delete(int id)
    //    {
    //        var user = getUser(id);
    //        _context.Users.Remove(user);
    //        _context.SaveChanges();
    //    }

    //    // helper methods

    //    private User getUser(int id)
    //    {
    //        var user = _context.Users.Find(id);
    //        if (user == null) throw new KeyNotFoundException("User not found");
    //        return user;
    //    }

    }
}
