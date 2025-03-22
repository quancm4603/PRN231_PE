using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

        [Range(0, 150)]
        public int Age { get; set; }

        [Range(0, 100)]
        public int Mark { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
