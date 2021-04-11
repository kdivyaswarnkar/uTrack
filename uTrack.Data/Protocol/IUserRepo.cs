using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using uTrack.Data.Model;

namespace uTrack.Data.Protocol
{
   public interface IUserRepo
    {
        Task<List<UserModel>> GetUser(string email, string password);
    }
}
