using System.ComponentModel.DataAnnotations;

namespace Library_Managemnt_System.ViewModel
{
	public class LoginUser
	{
		[Required(ErrorMessage = "*")]
		public string Name { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[Display(Name = "Remember Me!!")]
		public bool RememberMe { get; set; }
	}
}
