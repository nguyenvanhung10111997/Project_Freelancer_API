using Freelancer_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freelancer_Data
{
    public class Freelancer_DBContext: DbContext
    {
        public Freelancer_DBContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Account> Accounts { get; set; }
    }
}
