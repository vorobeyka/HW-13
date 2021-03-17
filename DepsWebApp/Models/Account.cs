using System.ComponentModel.DataAnnotations.Schema;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Account model.
    /// </summary>
    [Table("Accounts")]
    public class Account
    {
        /// <summary>
        /// Base64 id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Account login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Account password.
        /// </summary>
        public string Password { get; set; }
    }
}
