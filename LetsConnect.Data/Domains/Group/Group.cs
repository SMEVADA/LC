using LetsConnect.Data.Domains.common;
using LetsConnect.Web.Framework.ViewModels.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Group
{
    [MetadataType(typeof(GroupViewModel))]
    public partial class Group : BaseEntity
    {
        [Key]
        public long groupId { get; set; }
        public string groupName { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }
}
