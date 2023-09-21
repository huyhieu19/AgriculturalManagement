using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelCreateConfiguration
{
    public class TypeTreeConfiguration : IEntityTypeConfiguration<TypeTreeEntity>
    {
        public void Configure(EntityTypeBuilder<TypeTreeEntity> builder)
        {
            builder.ToTable("TypeTree");
            builder.HasKey(p => p.Id);
        }
    }
}
