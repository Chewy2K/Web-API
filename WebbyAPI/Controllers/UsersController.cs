using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using WebbyAPI.Data;
using WebbyAPI.Model;

namespace WebbyAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly DataContext _dbContext;

        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserEntity>> GetUsers()
        {
            return _dbContext.Users.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<UserEntity> GetUser(int id)
        {
            return _dbContext.Users.Find(id);
        }
        [HttpPost("adduser")]
        public async Task<ActionResult<string>> AddUser(UserEntity registerObject)
        {
            var user = new UserEntity
            {
                Username = registerObject.Username,
                Emailaddress = registerObject.Emailaddress,
                Mobilenumber = registerObject.Mobilenumber,
                Password = registerObject.Password,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return "Data Added.";
        }

        [HttpPut("updateuserdetails")]
        public async Task<ActionResult<string>> UpdateUser(UserEntity updateObj)
        {
            var currentUser = _dbContext.Users.Where(s=> s.Id==updateObj.Id).FirstOrDefault<UserEntity>(); 
            if (currentUser != null)
            {
                currentUser.Username = updateObj.Username;
                currentUser.Emailaddress = updateObj.Emailaddress;
                currentUser.Mobilenumber = updateObj.Mobilenumber;
                currentUser.Password = updateObj.Password;

                _dbContext.SaveChanges();
            }
            else
            {
                return "Insert Details";
            }
            return "Update sucessfully";
        }

        [HttpDelete("deleteuser")]
        public async Task<ActionResult<string>> DeleteUser(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Invalid Input");
            }
            var currentUser = _dbContext.Users.Where(s => s.Id == Id).FirstOrDefault<UserEntity>();
            if (currentUser != null)
            {
                _dbContext.Entry(currentUser).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            else
            {
                return "User not exist.";
            }
            return "Deleted sucessfully";
        }
    }
}
