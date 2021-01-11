using DS3Wiki.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DbTest.Contexts
{
    public class WikiContext : DbContext
    {
        public WikiContext() : base("DefaultConnection")
        {

        }

        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponArt> WeaponArts { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}