using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;

enum LibraryItemType
{
	[Description("Х")]
	Fiction,
	[Description("У")]
	Textbook,
	[Description("С")]
	Reference
}

struct TableRow
{
	public string Column1;
	public string Column2;
	public int Column3;
	public LibraryItemType Column4;
}

class Program1
{
	static List<TableRow> table = new List<TableRow>();
	static List<string> log = new List<string>();
	static DateTime startTime = DateTime.Now;
	static string fileName = "lab.dat";

	static void Main()
	{
		ReadFromFile();

		while (true)
		{
			Console.WriteLine("Выберите действие:");
			Console.WriteLine("1 – Просмотр таблицы");
			Console.WriteLine("2 – Добавить запись");
			Console.WriteLine("3 – Удалить запись");
			Console.WriteLine("4 – Обновить запись");
			Console.WriteLine("5 – Поиск записей");
			Console.WriteLine("6 – Просмотреть лог");
			Console.WriteLine("7 - Выход");

			int choice = ReadInt("Введите номер действия: ");

			switch (choice)
			{
				case 1:
					ViewTable();
					startTime = DateTime.Now;
					break;

				case 2:
					AddRow();
					startTime = DateTime.Now;
					break;

				case 3:
					RemoveRow();
					startTime = DateTime.Now;
					break;

				case 4:
					UpdateRow();
					startTime = DateTime.Now;
					break;

				case 5:
					SearchRows();
					startTime = DateTime.Now;
					break;

				case 6:
					ViewLog();
					break;

				case 7:
					SaveTableToFile(); // Сохранение данных в файл
					Console.WriteLine("Данные сохранены в файл");
					return;

				default:
					Console.WriteLine("Некорректный номер действия");
					break;
			}
		}
	}

	static void ReadFromFile()
	{
		if (!File.Exists(fileName))
		{
			return;
		}

		using (StreamReader reader = new StreamReader(fileName))
		{
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				string[] columns = line.Split(';');

				if (columns.Length == 4)
				{
					TableRow row = new TableRow();
					row.Column1 = columns[0];
					row.Column2 = columns[1];
					row.Column3 = int.Parse(columns[2]);
					Enum.TryParse(columns[3], out LibraryItemType itemType);
					row.Column4 = itemType;
					table.Add(row);
				}
			}
		}
	}

	static void SaveTableToFile()
	{
		try
		{
			using (FileStream fileStream = new FileStream("lab.dat", FileMode.Create))
			using (StreamWriter writer = new StreamWriter(fileStream))
			{
				foreach (TableRow row in table)
				{
					writer.WriteLine($"{row.Column1};{row.Column2};{row.Column3};{row.Column4}");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
		}
	}
	static void AddRow()
	{
		Console.WriteLine("Введите данные новой записи:");
		string column1 = ReadString("Автор: ");
		string column2 = ReadString("Название: ");
		int column3 = ReadInt("Год: ");
		LibraryItemType column4 = ReadLibraryItemType("Группа (Х/У/С): ");

		TableRow row = new TableRow();
		row.Column1 = column1;
		row.Column2 = column2;
		row.Column3 = column3;
		row.Column4 = column4;
		table.Add(row);

		log.Add($"[{DateTime.Now}] Добавлена запись: {row.Column1};{row.Column2};{row.Column3};{row.Column4}");
	}
	static void RemoveRow()
	{
		int index = ReadInt("Введите индекс записи для удаления: ");
		if (index < 0 || index >= table.Count)
		{
			Console.WriteLine("Некорректный индекс записи");
			return;
		}

		TableRow row = table[index];
		table.RemoveAt(index);

		log.Add($"[{DateTime.Now}] Удалена запись: {row.Column1};{row.Column2};{row.Column3};{row.Column4}");
	}
	static void UpdateRow()
	{
		int index = ReadInt("Введите индекс записи для обновления: ");
		if (index < 0 || index >= table.Count)
		{
			Console.WriteLine("Некорректный индекс записи");
			return;
		}

		Console.WriteLine($"Введите новые данные для записи {index}:");

		string column1 = ReadString("Автор: ");
		string column2 = ReadString("Название: ");
		int column3 = ReadInt("Год: ");
		LibraryItemType column4 = ReadLibraryItemType("Группа (Х/У/С): ");

		TableRow row = table[index];
		row.Column1 = column1;
		row.Column2 = column2;
		row.Column3 = column3;
		row.Column4 = column4;

		log.Add($"[{DateTime.Now}] Обновлена запись: {row.Column1};{row.Column2};{row.Column3};{row.Column4}");
	}
	static void ViewTable(List<TableRow> rows = null)
	{
		if (rows == null)
		{
			rows = table;
		}
		Console.WriteLine(new string('-', 80));
		Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|", "Автор", "Название", "Год", "Группа");
		Console.WriteLine(new string('-', 80));

		foreach (TableRow row in rows)
		{
			Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|", row.Column1, row.Column2, row.Column3, row.Column4);
		}

		Console.WriteLine(new string('-', 80));
		Console.WriteLine($"Всего записей: {table.Count}");
	}

	static void SearchRows()
	{
		Console.WriteLine("Введите критерии поиска:");
		string column1 = ReadString("Автор: ");
		string column2 = ReadString("Название: ");
		int column3 = ReadInt("Год: ");
		LibraryItemType column4 = ReadLibraryItemType("Группа (Х/У/С): ");

		bool found = false;
		foreach (TableRow row in table)
		{
			if ((column1 == "" || row.Column1.Contains(column1)) &&
				(column2 == "" || row.Column2.Contains(column2)) &&
				(column3 == -1 || row.Column3 == column3) &&
				(column4 == LibraryItemType.Fiction || row.Column4 == column4))
			{
				Console.WriteLine($"{row.Column1};{row.Column2};{row.Column3};{row.Column4}");
				found = true;
			}
		}

		if (!found)
		{
			Console.WriteLine("Записи, удовлетворяющие критериям поиска, не найдены");
		}
	}

	static void ViewLog()
	{
		Console.WriteLine("Лог изменений:");
		foreach (string entry in log)
		{
			Console.WriteLine(entry);
		}
	}

	static string ReadString(string prompt)
	{
		Console.Write(prompt);
		return Console.ReadLine();
	}

	static int ReadInt(string prompt)
	{
		while (true)
		{
			Console.Write(prompt);
			string input = Console.ReadLine();

			if (int.TryParse(input, out int result))
			{
				return result;
			}
			else
			{
				Console.WriteLine("Некорректный формат числа");
			}
		}
	}
	static LibraryItemType ReadLibraryItemType(string message)
	{
		// LibraryItemType itemType = LibraryItemType.Fiction;
		// string itemCode = ((DescriptionAttribute)typeof(LibraryItemType)
		// 	.GetField(itemType.ToString())
		// 	.GetCustomAttributes(false)
		// 	.First()).Description;
		while (true)
		{
			Console.Write(message);
			string input = Console.ReadLine().Trim().ToUpper();

			if (input == "Х")
			{
				return LibraryItemType.Fiction;
			}
			else if (input == "У")
			{
				return LibraryItemType.Textbook;
			}
			else if (input == "С")
			{
				return LibraryItemType.Reference;
			}
			else
			{
				Console.WriteLine("Некорректное значение. Пожалуйста, введите Х, У или С");
			}
		}
	}
}