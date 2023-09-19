using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.ConsoleApp
{
    public class OutputHandler
    {
        // Region: Public Methods

        /// <summary>
        /// Prints a list of pets in a sorted order.
        /// </summary>
        /// <param name="pets">The list of pets to print.</param>
        public static void PrintPets(List<Pet> pets)
        {
            // Sort the list of pets by category name (descending) and then by pet name (descending)
            var sortedPets = pets
                .OrderByDescending(pet => pet.category?.name)
                .ThenByDescending(pet => pet.name);

            // Iterate through the sorted list and print each pet
            foreach (var pet in sortedPets)
            {
                if (pet != null && pet.id > 0 && !string.IsNullOrEmpty(pet.name) && pet.tags != null) 
                {
                    Console.WriteLine(pet.ToString());
                    Console.WriteLine();
                }

            }
        }

        // End of Region: Public Methods
    }
}

