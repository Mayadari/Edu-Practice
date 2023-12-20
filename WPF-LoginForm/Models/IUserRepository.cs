using System.Collections.Generic;
using System.Net;

namespace WPF_LoginForm.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);

        void RegistrationUser(UserModel userModel);

        void Add(UserModel userModel);

        void Edit(UserModel userModel);

        void Remove(string username, string name, string lastname);

        UserModel GetByUsername(string username);

        List<UserModel> GetByAll();
    }
}
