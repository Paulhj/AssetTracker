using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssetTracker.Model
{
    public class AssetForUpdate : AssetDropDownLists
    {
        //Asset Properties
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Tag { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
}
