using Library_Managemnt_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Managemnt_System.ViewModel
{
    public class BookViewModel
    {
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Quantity { get; set; }

		//public string ImagePath { get; set; }
		
		public IFormFile Image { get; set; }
	}
}
