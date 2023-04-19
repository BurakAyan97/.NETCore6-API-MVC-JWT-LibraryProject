using CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst_WebApi_LibraryDb.EntityTypeConfigurations
{
    public class BookTypeConfig :BaseConfig<BookType>
    {
        public override void Configure(EntityTypeBuilder<BookType> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Book).WithMany(x => x.BookTypes).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Type).WithMany(x=>x.BookTypes).HasForeignKey(x=>x.TypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
