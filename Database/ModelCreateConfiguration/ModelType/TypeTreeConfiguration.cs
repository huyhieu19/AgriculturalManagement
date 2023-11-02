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
                new TypeTreeEntity { Id = 1, NameType = "Xoài", Description = "Loại cây ăn quả có thịnh hành tại Việt Nam." },
                new TypeTreeEntity { Id = 2, NameType = "Chuối", Description = "Loại cây ăn quả thường thấy trong vườn nhà dân." },
                new TypeTreeEntity { Id = 3, NameType = "Sầu riêng", Description = "Cây sầu riêng thường được trồng ở miền Nam Việt Nam." },
                new TypeTreeEntity { Id = 4, NameType = "Mít", Description = "Loại cây ăn quả có hạt lớn và ngon." },
                new TypeTreeEntity { Id = 5, NameType = "Vải", Description = "Vải là loại cây ăn quả có quả nhỏ màu đỏ tươi." },
                new TypeTreeEntity { Id = 6, NameType = "Nhãn", Description = "Nhãn là cây ăn quả thường thấy tại Việt Nam." },
                new TypeTreeEntity { Id = 7, NameType = "Chôm chôm", Description = "Loại cây ăn quả có vỏ màu đỏ và hạt trắng." },
                new TypeTreeEntity { Id = 8, NameType = "Bưởi", Description = "Bưởi là loại cây ăn quả có vị ngọt và hấp dẫn." },
                new TypeTreeEntity { Id = 9, NameType = "Cam", Description = "Cam là loại cây ăn quả chứa nhiều vitamin C." },
                new TypeTreeEntity { Id = 10, NameType = "Nho", Description = "Nho là loại cây ăn quả có nhiều loại khác nhau." },
                new TypeTreeEntity { Id = 11, NameType = "Đu đủ", Description = "Đu đủ là cây ăn quả phổ biến ở Việt Nam." },
                new TypeTreeEntity { Id = 12, NameType = "Khế", Description = "Khế là cây ăn quả có hương vị chua ngọt đặc trưng." },
                new TypeTreeEntity { Id = 13, NameType = "Thanh long", Description = "Loại cây ăn quả có hình dáng độc đáo." },
                new TypeTreeEntity { Id = 14, NameType = "Dứa", Description = "Dứa là cây ăn quả thường thấy trong vườn nhà dân." },
                new TypeTreeEntity { Id = 15, NameType = "Cây cau", Description = "Cây cau thường trồng ở vùng nhiệt đới Việt Nam." },
                new TypeTreeEntity { Id = 16, NameType = "Chanh", Description = "Chanh là cây ăn quả có vị chua và mùi thơm." },
                new TypeTreeEntity { Id = 17, NameType = "Me", Description = "Me là cây ăn quả có vị chua ngọt đặc trưng." },
                new TypeTreeEntity { Id = 18, NameType = "Sapoche", Description = "Sapoche là loại cây ăn quả có vị ngọt và mùi thơm đặc trưng." },
                new TypeTreeEntity { Id = 19, NameType = "Cherry", Description = "Cherry là loại cây ăn quả có hạt nhỏ màu đỏ." },
                new TypeTreeEntity { Id = 20, NameType = "Lựu", Description = "Lựu là cây ăn quả có hình dáng đặc trưng." }
            );
        }
    }
}