using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LoanRequestProvider : ILoanRequestProvider
    {
        private readonly Lms3Context _context;

        public LoanRequestProvider(Lms3Context context) 
        {
            _context = context;
        }
        public async Task<List<LoanRequest>> GetLoanRequestsAsync()
        {
            return await _context.LoanRequests.ToListAsync();
        }

        public async Task<LoanRequest> GetLoanRequestByIdAsync(string requestId)
        {
            Guid ConvRequestId;
            Guid.TryParse(requestId, out ConvRequestId);
            return await _context.LoanRequests.FirstOrDefaultAsync(r => r.RequestId == ConvRequestId);
        }

        public async Task<string> CreateLoanRequestAsync(LoanRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _context.LoanRequests.Add(request);
            await _context.SaveChangesAsync();
            return request.RequestId.ToString();
        }

        public async Task UpdateLoanRequestAsync(LoanRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _context.LoanRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanRequestAsync(string requestId)
        {
            Guid ConvRequestId;
            Guid.TryParse(requestId, out ConvRequestId);
            var requestToDelete = await _context.LoanRequests.FirstOrDefaultAsync(r => r.RequestId == ConvRequestId);
            if (requestToDelete != null)
            {
                _context.LoanRequests.Remove(requestToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
