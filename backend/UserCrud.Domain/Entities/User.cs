namespace UserCrud.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public int LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
