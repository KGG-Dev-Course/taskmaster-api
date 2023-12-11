using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ITagRepository
    {
        TagEntity GetTagById(int id);
        IEnumerable<TagEntity> GetAllTags();
        TagEntity CreateTag(TagEntity tag);
        TagEntity UpdateTag(int id, TagEntity tag);
        int DeleteTag(int id);
    }
}
