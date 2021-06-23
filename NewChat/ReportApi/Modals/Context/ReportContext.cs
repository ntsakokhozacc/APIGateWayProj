using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ReportApi.Modals.Context
{
    public class ReportContext : IdentityDbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options){}

        public DbSet<ReportModals> reports{get;set;}
    }
}