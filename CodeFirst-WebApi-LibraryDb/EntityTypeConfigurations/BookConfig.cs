using CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst_WebApi_LibraryDb.EntityTypeConfigurations
{
    public class BookConfig : BaseConfig<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(50);

        }
    }
}
