using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp
{
    public class OutputHandler
    {
        public static void PrintPets(List<Pet> pets)
        {
            var sortedPets = pets
                .OrderByDescending(pet => pet.category?.name)
                .ThenByDescending(pet => pet.name);

            foreach (var pet in sortedPets)
            {
                Console.WriteLine(pet.ToString());
                Console.WriteLine();
            }
        }
    }
}
