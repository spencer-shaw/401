using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mtg.Card.Tracker.Models;


namespace Mtg.Card.Tracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MagicCard> Cards { get; set; }

        public DbSet<MtgUser> MtgUsers { get; set; }
    }
}
