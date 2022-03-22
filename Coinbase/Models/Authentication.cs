using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coinbase.Models
{
    [Table("Authentication")]
    public class Authentication
    {
        [Key]
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        
    }
}