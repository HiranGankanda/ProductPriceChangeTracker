using Microsoft.Extensions.Logging;
using PPCT.DataSupport.DataModels.ProjectTableModels;
using System;

namespace PPCT.RepositoryServices.Helpers.HistoryRecordCreators
{
    public class RetailStoreVATPercentageHistoryRecorder
    {
        private readonly ILogger _logger;
        public RetailStoreVATPercentageHistoryRecorder(ILogger<RetailStoreVATPercentageHistoryRecorder> logger)
        {
            _logger = logger;
        }

        public RetailStoreVATPercentage_History VATPercentageHistoryCreator(RetailStoreVATPercentage RecordData, string ChangeNote, string CreatedBy)
        {
            try
            {
                return new RetailStoreVATPercentage_History() { 
                    ChangeNote = ChangeNote,
                    CreatedBy = CreatedBy,
                    CreatedOn = DateTime.Now,
                    IsActive = RecordData.IsActive,
                    RetailStoreID = RecordData.RetailStoreID,
                    VATPercentageId = RecordData.VATPercentageId,
                    VATPercentageValue = RecordData.VATPercentageValue
                };
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Error occurred while CreateHistoryRecord(), Exception : {1}", ex.ToString());
                return null;
            }
        }
    }
}
