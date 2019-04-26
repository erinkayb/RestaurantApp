using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    // sqlrestaurantdata implements irestaurantdata
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDBContext db;

        public SqlRestaurantData(OdeToFoodDBContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updateRestaurant)
        {
            var entity = db.Restaurants.Attach(updateRestaurant);
        }
    }
}
