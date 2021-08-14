using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Title")]
        [StringLength(255, MinimumLength = 2)]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        [StringLength(2000, MinimumLength = 100)]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = "Project date is required.")]
        [Display(Name = "Project Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectDate { get; set; }

        [Required]
        [Display(Name = "Last Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Updated { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}