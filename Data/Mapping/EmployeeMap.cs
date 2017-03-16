using System.Data.Entity.ModelConfiguration;
using HRM.Domain;

namespace Data.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            this.ToTable("Employee");
            this.HasKey(e => e.Id);

            this.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            this.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        }
    }
}
