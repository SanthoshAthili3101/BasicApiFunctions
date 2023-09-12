using System.ComponentModel.DataAnnotations;

namespace SampleApiCheckingAS.Models
{
    public class Food
    {
        [Required]
        public string Name { get; set; }
        public string Explanation { get; set; }
    }
}
