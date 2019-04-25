using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
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

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}
