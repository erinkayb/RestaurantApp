using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    // inherited from entity framework core's dbcontext class
    public class OdeToFoodDBContext : DbContext
    {
        // add properties with the data that needs to be stored in the database

        // DbSet used to tell entity framework to not only query for Restaurant information but also need to insert, update and delete restaurant information.
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
