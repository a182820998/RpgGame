using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RpgGame.Model.GameCharacter;

namespace RpgGame.Model.GameJob
{
    // 一個Job可以對應到多個角色
    public class Job
    {
        // Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ExtraStr { get; set; } = 0;

        [Required]
        public int ExtraDex { get; set; } = 0;

        [Required]
        public int ExtraCon { get; set; } = 0;

        [Required]
        public string Attribute { get; set; }

        // Navigation Properties
        //public virtual ICollection<UserCharacter> Characters { get; set; }

        // Navigation Properties
        //public virtual ICollection<GameMonster> Monsters { get; set; }

        [NotMapped]
        public virtual IDamageBy DamageBy { get; set; }

        [NotMapped]
        public virtual IAntagonisticBy AntagonisticBy { get; set; }
    }
}
