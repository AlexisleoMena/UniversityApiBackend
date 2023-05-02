using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        public enum Level
        {
            Basic,
            Medium,
            Advanced,
            Expert
        }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string shortDescription { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public Level level { get; set; } = Level.Basic;


        [Required]
        public ICollection<Category> categories { get; set; } = new List<Category>();

        [Required]
        public Chapter chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> students { get; set; } = new List<Student>();


    }
}
