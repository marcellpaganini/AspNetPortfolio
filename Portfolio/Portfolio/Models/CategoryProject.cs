using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class CategoryProject
    {
        public virtual int CategoryId { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Project Project { get; set; }
    }
}