using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        // methods
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updateRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Neighborhood Pizza", Location = "Amsterdam", Cuisine= CuisineType.Italian},
                new Restaurant { Id = 2, Name = "Saphran", Location = "Haarlem", Cuisine= CuisineType.Indian},
                new Restaurant { Id = 3, Name = "Taco Bowl", Location = "Amsterdam", Cuisine= CuisineType.Mexican},
                new Restaurant { Id = 4, Name = "Sofia's Burgers", Location = "Amsterdam", Cuisine= CuisineType.None},
                new Restaurant { Id = 5, Name = "Padro's Table", Location = "Haarlem", Cuisine= CuisineType.Mexican},
                new Restaurant { Id = 6, Name = "Salsa Spot", Location = "Amsterdam", Cuisine= CuisineType.Mexican},
                new Restaurant { Id = 7, Name = "Spicy Pepper", Location = "Amsterdam", Cuisine= CuisineType.Indian},
                new Restaurant { Id = 8, Name = "Piazza Pizza", Location = "Amsterdam", Cuisine= CuisineType.Italian},
                new Restaurant { Id = 9, Name = "American House", Location = "Almere", Cuisine= CuisineType.None},
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            //only for testing and learning. Do not use in production.
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            // look inside the list of restaurants and pull one out
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }
}
