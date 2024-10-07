using Library_Managemnt_System.Models;

namespace Library_Managemnt_System.Repositories
{
    public class BookRepository : IBookRepository
    {
        LibraryContext context;
        public BookRepository(LibraryContext libraryContext) { context = libraryContext; }
        public void Add(Book book)
        {
            context.Add(book);
        }

        public void Delete(Book book)
        {
            context.Remove(book);
        }

        public List<Book> GetAll()
        {
            return context.Book.ToList();
        }

        public Book GetById(int id)
        {
            return context.Book.FirstOrDefault(d => d.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

		public async void SaveAsync()
		{
			await context.SaveChangesAsync();
		}

		public void Update(Book book)
        {
            context.Update(book);
        }
    }
}
