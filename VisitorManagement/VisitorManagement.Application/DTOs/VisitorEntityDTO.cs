namespace VisitorManagement.Application.DTOs
{
    public class VisitorEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string PersonalIdType { get; set; }
        public string PersonalIdNumber { get; set; }
        public IFormFile PersonalIdCardImage { get; set; }
        public IFormFile PersonalImage { get; set; }
    }
}
