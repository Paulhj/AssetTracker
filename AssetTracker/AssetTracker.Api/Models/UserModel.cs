using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string NmFirst { get; set; }
        public string NmLast { get; set; }
        public string Email { get; set; }
    }
}
