using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DapperNetCore8_Api.Models
{
    [Table("Ogrenciler")]
    public class Ogrenciler
    {
        [Key]
        public int id { get; set; }
        public string ad { get; set; }
        public int yas { get; set; }
    }
}
