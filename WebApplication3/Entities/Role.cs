using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
