namespace MainPages
{
	/// <summary>
	/// Класс обработки и проверки корректности данных
	/// </summary>
	public static class DataChecker
	{
		public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,40}$";
		public const string PasswordDescription = "Пароль должен содержать заглавные буквы, цифры, и быть длинной от 8 до 40 символов.";

		public const string EmailRegex = @"^\S+\@\S+\.\S+$";
		public const string EmailDescription = "Почта должна соответствовать шаблону: name@example.ru";

		public const string PhoneRegex = @"^\d{11}$";
		public const string PhoneDescription = "Телефон должен содержать 11 цифр";

		public const string Name40 = @"^[А-ЯA-Z].{1,39}$";
		public const string Name30 = @"^[А-ЯA-Z].{1,29}$";
		public const string Name150 = @"^[А-ЯA-Z].{1,149}$";
		public const string Name100 = @"^[А-ЯA-Z].{1,99}$";
		public const string Name50 = @"^[А-ЯA-Z].{1,49}$";
		public const string Name19 = @"^[А-ЯA-Z].{1,18}$";
		public const string Name200 = @"^[А-ЯA-Z].{1,199}$";
		public const string Name300 = @"^[А-ЯA-Z].{1,299}$";

		/// <summary>
		/// Сравнивает указанную дату с текущей
		/// </summary>
		/// <param name="date">Дата</param>
		/// <returns>0 - равны; -1 - date < now; 1 - date > now</now></returns>
		public static int CheckDateNow(DateOnly date)
		{
			DateTime now = DateTime.Now;

			if (date.Year == now.Year && date.Month == now.Month && date.Day == now.Day)
				return 0;

			if (date.Year < now.Year)
				return -1;
			else if (date.Year == now.Year && date.Month < now.Month)
				return -1;
			else if (date.Month == now.Month && date.Day < now.Day)
				return -1;
			else
				return 1;
		}

		/// <summary>
		/// Создание из цифр телефона его стандартного представления
		/// </summary>
		/// <param name="text">Текст</param>
		/// <returns></returns>
		public static string BeautyPhone(string text)
		{
			return text.Substring(0, 1) + " (" + text.Substring(1, 3) + ") " + text.Substring(4, 3) + "-" + text.Substring(7, 2) + "-" + text.Substring(9, 2);
		}

		/// <summary>
		/// Перевод даты из DbDate в WebDate
		/// </summary>
		/// <param name="date">Дата</param>
		public static string DbDateToWebDate(string date)
		{
			string[] parts = date.Split('/');
			return parts[2] + "-" + parts[1] + "-" + parts[0];
        }

		/// <summary>
		/// Преобразование списка строк
		/// </summary>
		/// <param name="strings">Список</param>
		/// <returns>Строка формата "Str1, str2, str..."</returns>
		public static string ListToString(List<string> strings)
		{
			string result = "";
			result += strings[0];
			for (int j = 1; j < strings.Count; j++)
			{
				result += ", " + char.ToLower((strings[j].ToString())[0]) + strings[j].ToString().Substring(1);
			}
			return result;
		}

		/// <summary>
		/// Преобразование списка строк
		/// </summary>
		/// <param name="strings">Список</param>
		/// <returns>Строка формата "str1 \n str2 \n str..."</returns>
        public static string ObjectListToStringLikeList(List<string> strings)
        {
            string result = "";
            result += strings[0];
            for (int j = 1; j < strings.Count; j++)
            {
                result += "\n" + strings[j];
            }
            return result;
        }
    }
}
