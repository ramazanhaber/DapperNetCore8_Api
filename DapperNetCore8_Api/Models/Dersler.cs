using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DapperNetCore8_Api.Models
{
    [Table("Dersler")]
    public class Dersler
    {
        [Key]
        public int id { get; set; }
        public string ad { get; set; }
    }
}
