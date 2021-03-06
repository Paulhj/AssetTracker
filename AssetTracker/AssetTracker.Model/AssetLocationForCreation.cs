﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssetTracker.Model
{
    public class AssetLocationForCreation
    {
        [Required]
        public int AssetId { get; set; }
        [Required]
        public int LocationId { get; set; }
        public string Note { get; set; }
        [Required]
        public DateTime CreateDt { get; set; }
    }
}
