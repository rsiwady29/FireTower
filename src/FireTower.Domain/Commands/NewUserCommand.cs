using FireTower.Domain.Entities;

namespace FireTower.Domain.Commands
{
    public class NewUserCommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public long FacebookId { get; set; }

        public string Locale { get; set; }

        public string Username { get; set; }

        public bool Verified { get; set; }

        public Location Location { get; set; }
    }
}