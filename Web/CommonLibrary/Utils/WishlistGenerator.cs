using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary.Contracts;

namespace CommonLibrary.Utils
{
    public static class WishlistGenerator
    {
        public static List<Wishlist> GenerateWishlist(
            List<Employee> forEmployees,
            List<Employee> offEmployees)
        {
            if (forEmployees == null || offEmployees == null)
            {
                throw new ArgumentNullException("Списки участников не должны быть null");
            }

            Random random = new Random();
            var wishlists = new List<Wishlist>();

            var allOffIds = offEmployees.Select(t => t.Id).ToList();

            foreach (var employee in forEmployees)
            {
                var randomizedIds = allOffIds.OrderBy(_ => random.Next()).ToArray();
                wishlists.Add(new Wishlist(employee.Id, randomizedIds));
            }

            return wishlists;
        }
    }
}
