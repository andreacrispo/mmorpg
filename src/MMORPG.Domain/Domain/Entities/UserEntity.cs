
namespace MMORPG.Domain.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public virtual ICollection<CharacterEntity> Characters { get; set; } = null;

    }
}
