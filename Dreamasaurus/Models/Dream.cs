using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dreamasaurus.Models
{
    public class Dream
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

    }
}