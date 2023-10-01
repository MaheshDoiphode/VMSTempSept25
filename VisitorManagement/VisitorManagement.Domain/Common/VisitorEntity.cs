using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Common
{
    public class VisitorEntity
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitorEntityId { get; set; }

        [Key, Column(Order = 2)]
        public string VisitorName { get; set; }

        [Key, Column(Order = 3)]
        public string VisitorContactNumber { get; set; }

        public string VisitorPersonalIdType { get; set; }
        public string VisitorPersonalIdNumber { get; set; }

        [NotMapped]
        public IFormFile VisitorPersonalIdCardImage { get; set; }

        [NotMapped]
        public IFormFile VisitorPersonalImage { get; set; }
    }
}
