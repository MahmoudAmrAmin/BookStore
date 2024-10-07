using Library_Managemnt_System.Models;

namespace Library_Managemnt_System.Repositories
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(Category category);
        public List<Category> GetAll();

        public Category GetById(int id);

        public void Save();
    }
}
