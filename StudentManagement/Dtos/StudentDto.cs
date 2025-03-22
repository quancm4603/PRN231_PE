namespace StudentManagement.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int Age { get; set; }
        public int Mark { get; set; }
        public bool IsActive { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
    }

    public class CreateStudentDto
    {
        public string Fullname { get; set; }
        public int Age { get; set; }
        public int Mark { get; set; }
        public bool IsActive { get; set; }
        public int ClassId { get; set; }
    }

    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int Age { get; set; }
        public int Mark { get; set; }
        public bool IsActive { get; set; }
        public int ClassId { get; set; }
    }
}
