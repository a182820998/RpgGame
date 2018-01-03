using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RpgGame.Model.GameJob;

namespace RpgGame.Model.GameCharacter
{
    // 一個Character(Monster)只會對應一個Job
    public abstract class BaseCharacter
    {
        // Primary Key
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }

        // Foreign Key
        public int JobId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; } = 1;

        [Required]
        public int Str { get; set; } = 0;

        [Required]
        public int Dex { get; set; } = 0;

        [Required]
        public int Con { get; set; } = 0;

        [Required]
        public int Exp { get; set; } = 0;

        [Required]
        public int TotalFightTimes { get; set; } = 0;

        [Required]
        public int FailFightTimes { get; set; } = 0;

        // Navigation Property
        //public virtual Job Job { get; set; }

        [NotMapped]
        public Job Job { get; set; }
    }
}
