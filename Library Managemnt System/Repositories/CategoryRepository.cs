using Library_Managemnt_System.Models;

namespace Library_Managemnt_System.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        LibraryContext context;
        public CategoryRepository(LibraryContext libraryContext) { context = libraryContext; }
        public void Add(Category category)
        {
            context.Add(category);
        }

        public void Delete(Category category)
        {
            context.Remove(category);
        }

        public List<Category> GetAll()
        {
            return context.Category.ToList();
        }

        public Category GetById(int id)
        {
            return context.Category.FirstOrDefault(d => d.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            context.Update(category);
        }
    }
}
