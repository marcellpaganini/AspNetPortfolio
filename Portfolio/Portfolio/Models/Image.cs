﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Image Name")]
        [StringLength(255, MinimumLength = 2)]
        public string ImageName { get; set; }
    }
}