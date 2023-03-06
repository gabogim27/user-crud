namespace UserCrud.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
