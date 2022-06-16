using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PPCT.DataAccessLayer;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCT.RepositoryServices.AgencyRepoServices
{
    public class AgencyRepo : IAgencyRepo
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;
        public AgencyRepo(DatabaseContext context, ILogger<AgencyRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateNewAgency(Agency AgencyDetails)
        {
            _logger.LogInformation($"[AgencyRepo]CreateNewAgency(AgencyDetails) hit at {DateTime.UtcNow.ToLongTimeString()}");

            try
            {
                bool available = _context.Agencies.Any(a => a.AgencyName == AgencyDetails.AgencyName);
                if (available == true)
                {
                    _logger.LogInformation($"[AgencyRepo](AgencyDetails) [AGENCY ALREADY CREATED] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.Agencies.Add(AgencyDetails);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[AgencyRepo]CreateNewAgency(AgencyDetails) [SUCCESS] hit at {DateTime.UtcNow.ToLongTimeString()}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while CreateNewAgency(AgencyDetails), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> UpdateAgency(Agency AgencyDetails)
        {
            _logger.LogInformation($"[AgencyRepo]UpdateAgency() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                _context.Entry(AgencyDetails).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while UpdateAgency(), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteAgency(int AgencyId)
        {
            _logger.LogInformation($"[AgencyRepo]DeleteAgency(int AgencyId) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                var agency = await _context.Agencies.FindAsync(AgencyId);
                if (agency == null)
                {
                    _logger.LogInformation($"[AgencyRepo]DeleteAgency(AgencyId) [AGENCY NOT FOUND] hit at {DateTime.UtcNow.ToLongTimeString()}");
                    return false;
                }
                else
                {
                    _context.Agencies.Remove(agency);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"[AgencyRepo]DeleteAgency(AgencyId) [AGENCY DELETED SUCCESSFULLY] hit at {DateTime.UtcNow.ToLongTimeString()}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while DeleteAgency(int AgencyId), Exception : {1}", ex.ToString());
                return false;
            }
        }
        public async Task<List<Agency>> GetAgenciesListAsync()
        {
            _logger.LogInformation($"[AgencyRepo]GetAgenciesListAsync() hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.Agencies.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while GetAgenciesListAsync(), Exception : {1}", ex.ToString());
                return null;
            }
        }
        public async Task<Agency> FindAgencyDetails(int Id)
        {
            _logger.LogInformation($"[AgencyRepo]FindAgencyDetails(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            try
            {
                return await _context.Agencies.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error occurred while FindAgencyDetails(int Id), Exception : {1}", ex.ToString());
                return null;
            }
        }
    }
}
