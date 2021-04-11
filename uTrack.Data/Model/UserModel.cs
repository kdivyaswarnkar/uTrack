using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace uTrack.Data.Model
{
  public class UserModel
    {
        public int ID { get; set; }

        public string First_Name { get; set; }

        public string Middle_Name { get; set; }

        public string Last_Name { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public long Phone_Number { get; set; }

        public string Token { get; set; }


    }
}
