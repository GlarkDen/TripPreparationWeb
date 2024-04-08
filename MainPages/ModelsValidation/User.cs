using System.ComponentModel.DataAnnotations;

namespace MainPages.Models;

public partial class User
{
    public int UserNumber { get; set; }

    [Required(ErrorMessage = "Имя является обязательным")]
	[RegularExpression(DataChecker.Name30, ErrorMessage = "Имя должно начинаться с большой буквы и содержать не более 30 символов")]
	public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Фамилия является обязательной")]
	[RegularExpression(DataChecker.Name30, ErrorMessage = "Фамилия должна начинаться с большой буквы и содержать не более 30 символов")]
	public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "Отчество является обязательным")]
	[RegularExpression(DataChecker.Name30, ErrorMessage = "Отчество должно начинаться с большой буквы и содержать не более 30 символов")]
	public string Patronymic { get; set; } = null!;

	[MaxLength(length: 250, ErrorMessage = "Максимальная длина не более 250 символов")]
	public string? Allergies { get; set; }

	[MaxLength(length: 250, ErrorMessage = "Максимальная длина не более 250 символов")]
	public string? СhronicDiseases { get; set; }

	[MaxLength(length: 250, ErrorMessage = "Максимальная длина не более 250 символов")]
	public string? MedicationsTaken { get; set; }

    [Required(ErrorMessage = "Телефон является обязательным")]
	[RegularExpression(DataChecker.PhoneRegex, ErrorMessage = DataChecker.PhoneDescription)]
	public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Почта является обязательной")]
	[RegularExpression(DataChecker.EmailRegex, ErrorMessage = DataChecker.EmailDescription)]
	public string EmailAddress { get; set; } = null!;

    [Required(ErrorMessage = "Пароль является обязательным")]
    [RegularExpression(DataChecker.PasswordRegex, ErrorMessage = DataChecker.PasswordDescription)]
    public string Password { get; set; } = null!;

    public virtual ICollection<ParticipantsList> ParticipantsLists { get; set; } = new List<ParticipantsList>();

    public virtual ICollection<DutyType> DutyTypeNumbers { get; set; } = new List<DutyType>();

    public virtual ICollection<FoodIntakeType> FoodIntakeTypeNumbers { get; set; } = new List<FoodIntakeType>();

    public virtual ICollection<Post> PostNumbers { get; set; } = new List<Post>();
}
