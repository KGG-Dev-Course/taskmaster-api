using Microsoft.EntityFrameworkCore;

namespace taskmaster_api.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
