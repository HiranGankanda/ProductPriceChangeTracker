using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataAccessLayer.DataModels.ProjectTableModels
{
    [Table("Agency")]
    public class Agency
    {
        public int AgencyID { get; set; }
        public string AgencyName { get; set; }
    }
}
