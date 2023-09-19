using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public class Pet : Base
    {
        public Category category { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            // Extract tag names and join them with a comma
            string tagNames = string.Join(", ", tags?.Select(tag => tag.name));

            return $"Pet ID: \u001b[33m{id}\u001b[0m, " +
                $"Name: \u001b[34m{name}\u001b[0m, " +
                $"Status: \u001b[35m{status}\u001b[0m, " +
                $"Tags: {FormatTagsWithColor(tagNames)}";
        }

        private string FormatTagsWithColor(string tagNames)
        {
            if (string.IsNullOrEmpty(tagNames) || tagNames == "string")
            {
                return "\u001b[36mNot Available\u001b[0m";
            }
            else
            {
                return "\u001b[36m" + tagNames + "\u001b[0m";
            }
        }
    }
}
