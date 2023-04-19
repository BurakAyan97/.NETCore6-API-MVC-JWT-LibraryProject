using CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst_WebApi_LibraryDb.EntityTypeConfigurations
{
    public class BaseConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).IsRequired().UseIdentityColumn();
        }
    }
}
