namespace StudentManagement.Dtos
{
    public class ClassDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<StudentDto> Students { get; set; }
    }

    public class CreateClassDto
    {
        public string ClassName { get; set; }
    }

    public class UpdateClassDto
    {
        public string ClassName { get; set; }
    }
}
