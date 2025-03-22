using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClassName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
