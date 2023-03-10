class Program2
{
	static void Main(string[] args)
	{
		string mainFilename = "progression.bin";
		string resultFilename = "result.bin";
		// Записываем числа прогрессии в файл
		WriteProgression(mainFilename);

		// Читаем 3-й и 7-й члены прогрессии из файла и записываем их во второй файл
		ReadWriteProgression(mainFilename, resultFilename);
	}

	static void ReadWriteProgression(string sourceFile, string destinationFile)
	{
		// Создаем файловый поток для чтения из бинарного файла
		using (BinaryReader reader = new BinaryReader(File.Open(sourceFile, FileMode.Open)))
		{
			// Читаем 3-й и 7-й члены прогрессии
			int third = reader.ReadInt32();
			reader.ReadBytes(sizeof(int) * 3); // Пропускаем 3 числа
			int seventh = reader.ReadInt32();

			Console.WriteLine($"Третий член прогрессии {third}");
			Console.WriteLine($"Седьмой член прогрессии {seventh}");

			// Создаем файловый поток для записи во второй файл
			using (BinaryWriter writer = new BinaryWriter(File.Open(destinationFile, FileMode.Create)))
			{
				// Записываем 3-й и 7-й члены прогрессии во второй файл
				writer.Write(third);
				writer.Write(seventh);
			}
		}
	}

	static void WriteProgression(string filename)
	{
		using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
		{
			int first = 3; // Первый член прогрессии
			int step = 4; // Шаг прогрессии
			int count = 10; // Количество чисел прогрессии для записи

			for (int i = 0; i < count; i++)
			{
				// Вычисляем очередное число прогрессии и записываем его в файл
				int current = first * (int)Math.Pow(step, i);
				writer.Write(current);
			}
		}
	}
}