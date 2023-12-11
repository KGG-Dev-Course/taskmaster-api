using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TagEntity GetTagById(int id)
        {
            return _context.Tags.Find(id);
        }

        public IEnumerable<TagEntity> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public TagEntity CreateTag(TagEntity tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag;
        }

        public TagEntity UpdateTag(int id, TagEntity tag)
        {
            if (_context.Tags.Find(id) is TagEntity oldTag)
            {
                tag.Id = id;
                _context.Tags.Entry(oldTag).State = EntityState.Detached;
                _context.Tags.Entry(tag).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return tag;
        }

        public int DeleteTag(int id)
        {
            var tagToDelete = _context.Tags.Find(id);
            if (tagToDelete != null)
            {
                _context.Tags.Remove(tagToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
