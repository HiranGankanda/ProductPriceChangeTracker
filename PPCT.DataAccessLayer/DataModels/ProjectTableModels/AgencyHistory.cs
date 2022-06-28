using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport.DataModels.ProjectTableModels
{
    [Table("AgencyHistory")]
    public class AgencyHistory
    {
        public int HistoryID { get; set; }
        public int? AgencyID { get; set; }
        public string AgencyName { get; set; }
        public string ChangeNote { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedOn { get; set; }
    }
}