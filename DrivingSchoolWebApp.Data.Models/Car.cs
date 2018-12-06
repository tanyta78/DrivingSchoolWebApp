namespace DrivingSchoolWebApp.Data.Models
{
    using Common;

    public class Car : BaseModel<int>
    {
        public string Model { get; set; }
        
        public School Owner { get; set; }

        public byte[] Image { get; set; }

    }
}