using Entities;

namespace Repository.Contracts
{
    public interface ITypeTreeRepository
    {
        void CreateTypeTrees(TypeTreeEntity entity);
        void UpdateTypeTree(TypeTreeEntity entity);
        void DeleteTypeTrees(TypeTreeEntity entity);
        Task<List<TypeTreeEntity>> GetTypeTree();
    }
}