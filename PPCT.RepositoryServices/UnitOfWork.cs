using PPCT.DataAccessLayer;
using PPCT.RepositoryServices.VATRepoServices;

namespace PPCT.RepositoryServices
{
    public class UnitOfWork
    {
        private readonly DatabaseContext _context;
        private VATRepo _vatRepo;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public VATRepo VATRepoUOW
        {
            get { 
                if(_vatRepo == null)
                {
                    _vatRepo = new VATRepo(_context);
                }
                return _vatRepo;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
