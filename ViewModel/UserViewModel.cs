using MongoDB.Bson.Serialization.Attributes;

namespace Challenge_Locaweb.ViewModel
{
    public class UserViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsEmailVerified { get; set; } = false;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
