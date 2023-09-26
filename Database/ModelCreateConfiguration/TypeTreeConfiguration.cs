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
            builder.HasData(
                new TypeTreeEntity(1, "Xoài"),
                new TypeTreeEntity(2, "Thanh long"),
                new TypeTreeEntity(3, "Chuối"),
                new TypeTreeEntity(4, "Đu đủ"),
                new TypeTreeEntity(5, "Ổi"),
                new TypeTreeEntity(6, "Táo"),
                new TypeTreeEntity(7, "Nhãn"),
                new TypeTreeEntity(8, "Chôm chôm"),
                new TypeTreeEntity(9, "Vải"),
                new TypeTreeEntity(10, "Sầu riêng"),
                new TypeTreeEntity(11, "Mít"),
                new TypeTreeEntity(12, "Khế"),
                new TypeTreeEntity(13, "Dứa"),
                new TypeTreeEntity(14, "Na"),
                new TypeTreeEntity(15, "Sapoche"),
                new TypeTreeEntity(16, "Me"),
                new TypeTreeEntity(17, "Cam"),
                new TypeTreeEntity(18, "Bưởi"),
                new TypeTreeEntity(19, "Chanh"),
                new TypeTreeEntity(20, "Chanh dây"),
                new TypeTreeEntity(21, "Nho")
                );
        }
    }
}
