using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using uTrack.Data.Model;

namespace uTrack.Business.Interface
{
  public interface IUserService
    {
        Task<UserModel> GetUser(string email, string password);
    }
}
