using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst_WebApi_LibraryDb.EntityTypeConfigurations
{
    public class TypeConfig : BaseConfig<Entities.Type>
    {
        public override void Configure(EntityTypeBuilder<Entities.Type> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
