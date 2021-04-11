using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace uTrack.Data.Context
{
  public class IdmDBContext : DbContext
    {
        public IdmDBContext(DbContextOptions<IdmDBContext> options) : base(options)
        {

        }
    }
}
