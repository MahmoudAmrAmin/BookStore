using Library_Managemnt_System.Models;

namespace Library_Managemnt_System.Repositories
{
    public interface IBookRepository
    {
        public void Add(Book book);
        public void Update(Book book);
        public void Delete(Book book);
        public List<Book> GetAll();

        public Book GetById(int id);

        public void Save();
        public void SaveAsync();

    }
}
