//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VidPackBackEnd.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExistingSession
    {
        public ExistingSession()
        {
            this.DownloadItem = new HashSet<DownloadItem>();
        }
    
        public int Id { get; set; }
        public string SessionTitle { get; set; }
        public string SessionSubTitle { get; set; }
        public Nullable<System.DateTime> SessionDate { get; set; }
        public string Speaker { get; set; }
        public string SessionDescription { get; set; }
        public string SessionVideoUri { get; set; }
        public string SessionThumbnailUri { get; set; }
        public Nullable<int> IsActualSession { get; set; }
        public Nullable<int> IsNextSession { get; set; }
    
        public virtual ICollection<DownloadItem> DownloadItem { get; set; }
    }
}
