using CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst_WebApi_LibraryDb.EntityTypeConfigurations
{
    public class AuthorBookConfig : BaseConfig<AuthorBook>
    {
        public override void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Author).WithMany(x => x.AuthorBooks).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Book).WithMany(x => x.AuthorBooks).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
