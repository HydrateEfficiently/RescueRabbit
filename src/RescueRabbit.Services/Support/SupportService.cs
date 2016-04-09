using Microsoft.Data.Entity;
using RescueRabbit.Framework.Models.Support;
using RescueRabbit.Framework.Services;
using RescueRabbit.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Support
{
    public interface ISupportService
    {
        Task<SupportOutlet> CreateAsync(CreateSupportOutletRequest request);

        Task<SupportOutlet> GetAsync(Guid id);

        Task<IEnumerable<SupportOutlet>> ListAsync();

        Task<SupportOutlet> UpdateAsync(UpdateSupportOutletRequest updateReq);
    }

    public class SupportService : ISupportService
    {
        private readonly IIdentityResolver _identityResolver;
        private readonly ApplicationDbContext _dbContext;

        public SupportService(
            IIdentityResolver identityResolver,
            ApplicationDbContext dbContext)
        {
            _identityResolver = identityResolver;
            _dbContext = dbContext;
        }

        // TODO: Authorize creation, Authorize can manually set ID
        public async Task<SupportOutlet> CreateAsync(CreateSupportOutletRequest request) 
        {
            var supportOutlet = request.ToSupportOutlet();
            supportOutlet.CreatedById = _identityResolver.Resolve().GetId();
            supportOutlet.Created = DateTime.UtcNow;

            _dbContext.SupportOutlets.Add(supportOutlet);
            await _dbContext.SaveChangesAsync();

            return supportOutlet;
        }

        public async Task<SupportOutlet> GetAsync(Guid id)
        {
            return await _dbContext.SupportOutlets.FirstOrDefaultAsync(so => so.Id == id);
        }

        public async Task<IEnumerable<SupportOutlet>> ListAsync()
        {
            return await _dbContext.SupportOutlets.ToListAsync();
        }

        // TODO: Authorize
        public Task<SupportOutlet> UpdateAsync(UpdateSupportOutletRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
