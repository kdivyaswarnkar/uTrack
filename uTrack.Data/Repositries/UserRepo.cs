using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using uTrack.Data.Context;
using uTrack.Data.Helper;
using uTrack.Data.Model;
using uTrack.Data.Protocol;

namespace uTrack.Data.Repositries
{
   public class UserRepo : IUserRepo
    {
        private IdmDBContext _dbContext;

        public UserRepo(IdmDBContext dBContext)
        {
            this._dbContext = dBContext;
        }

        public async Task<List<UserModel>> GetUser(string email, string password)
        {
            List<UserModel> user = await this._dbContext.ExcecuteStoredProcedureAsync<UserModel>(
                "SP_GetUser",
            ("Email", email),
            ("Password", password));

            return user;
        }
    }
}
