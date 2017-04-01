
using LetsConnect.Web.Framework.ViewModels.Menu;
using System;
using System.ComponentModel.DataAnnotations;

namespace LetsConnect.Data.Domains.Menu
{
    [MetadataType(typeof(MenuViewModel))]
    public partial class Menu
    {
        [Key]
        public long menuId { get; set; }

        public string menuName { get; set; }

        public string pageUrl { get; set; }

        public Nullable<long> TotalRows { get; set; }
    }

    public partial class MenuPermissions
    {
        public long administratorId { get; set; }
        public long menuId { get; set; }
        public bool isView { get; set; }
        public bool isCreate { get; set; }
        public bool isDelete { get; set; }
        public string menuName { get; set; }
        public string pageUrl { get; set; }
        public long? TotalRows { get; set; }
    }
}
