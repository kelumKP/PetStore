using System;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.Models
{
    // Region: Pet Class

    /// <summary>
    /// Represents a pet in the Pet Store.
    /// </summary>
    public class Pet : Base
    {
        // Region: Properties

        /// <summary>
        /// Gets or sets the category to which the pet belongs.
        /// </summary>
        public Category category { get; set; }

        /// <summary>
        /// Gets or sets the list of photo URLs for the pet.
        /// </summary>
        public List<string> photoUrls { get; set; }

        /// <summary>
        /// Gets or sets the list of tags associated with the pet.
        /// </summary>
        public List<Tag> tags { get; set; }

        /// <summary>
        /// Gets or sets the status of the pet.
        /// </summary>
        public string status { get; set; }

        // End of Region: Properties

        // Region: Public Methods

        /// <summary>
        /// Generates a string representation of the pet.
        /// </summary>
        /// <returns>A formatted string representing the pet.</returns>
        public override string ToString()
        {
            // Extract tag names and join them with a comma
            string tagNames = string.Join(", ", tags?.Select(tag => tag.name));

            return $"Pet ID: \u001b[33m{id}\u001b[0m, " +
                $"Name: \u001b[34m{name}\u001b[0m, " +
                $"Status: \u001b[35m{status}\u001b[0m, " +
                $"Tags: {FormatTagsWithColor(tagNames)}";
        }

        // End of Region: Public Methods

        // Region: Private Methods

        /// <summary>
        /// Formats tag names with color.
        /// </summary>
        /// <param name="tagNames">The tag names to format.</param>
        /// <returns>A formatted string with color.</returns>
        private string FormatTagsWithColor(string tagNames)
        {
            if (string.IsNullOrEmpty(tagNames))
            {
                return "\u001b[36mNot Available\u001b[0m";
            }
            else
            {
                return "\u001b[36m" + tagNames + "\u001b[0m";
            }
        }

        // End of Region: Private Methods
    }

    // End of Region: Pet Class
}
