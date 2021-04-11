using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using uTrack.Business.Interface;
using uTrack.Data.Model;
using uTrack.Data.Protocol;

namespace uTrack.Business
{
    public class UserService : IUserService
    {
        private IUserRepo _dataProvider;
        public UserService(IUserRepo dataProvider)
        {
            this._dataProvider = dataProvider;
        }

        public async Task<UserModel> GetUser(string email, string password)
        {
            List<UserModel> userList = await this._dataProvider.GetUser(email, password);
            return userList[0];
        }
    }
}
