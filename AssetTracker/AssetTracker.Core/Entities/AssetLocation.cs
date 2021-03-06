﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssetTracker.Core.Entities
{
    public class AssetLocation : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset Asset { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTime CreateDt { get; set; }

        public DateTime TransferDt { get; set; }
    }
}
