using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecomm.Domain.Entities;

namespace ecomm.Application.Interfaces
{
    public interface IUserServices
    {
        void AddUser(User_Reg user);
        List<Login> Login(Login login);
        List<User_Reg> GetUser();
        List<User_Reg> GetUserById(int? uid);
        void DeleteUser(int? uid);
        void UpdateUser(UpdateUser uumod);
    }

}
