using System.Collections.Generic;

namespace Dreamasaurus.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Dream> Dreams { get; set; }
    }
}