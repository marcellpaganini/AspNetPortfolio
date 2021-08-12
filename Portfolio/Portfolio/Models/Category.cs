using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Category")]
        [StringLength(255, MinimumLength = 2)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [Display(Name = "Type")]
        [StringLength(255, MinimumLength = 2)]
        public string Type { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}