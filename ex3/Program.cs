using System;
using System.IO;

class Program3
{
	static void Main(string[] args)
	{
		string inputFile = "input.txt";
		string outputFile = "result.txt";
		int replacements = 0;

		try
		{
			// Чтение входного файла
			string inputText = File.ReadAllText(inputFile);

			// Замена цифр на символ "*"
			string outputText = "";
			foreach (char c in inputText)
			{
				if (char.IsDigit(c))
				{
					outputText += "*";
					replacements++;
				}
				else
				{
					outputText += c;
				}
			}

			// Запись результата в выходной файл
			File.WriteAllText(outputFile, outputText);

			// Вывод количества замен на консоль
			Console.WriteLine("Количество замен: " + replacements);
		}
		catch (IOException e)
		{
			Console.WriteLine("Ошибка ввода-вывода: " + e.Message);
		}
	}
}