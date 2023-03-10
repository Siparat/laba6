using System;
using System.IO;

class Program4
{
	static void Main(string[] args)
	{
		string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		string tempDir = Path.Combine(homeDir, "Lab6_Temp");

		// Создаем директорию Lab6_Temp, если ее еще нет
		Directory.CreateDirectory(tempDir);

		string labFileSrc = Path.Combine("..", "ex1", "lab.dat");
		string labFileDst = Path.Combine(tempDir, "lab.dat");
		string labBackupFileDst = Path.Combine(homeDir, "lab_backup.dat");

		// Копируем файл lab.dat в директорию Lab6_Temp
		try
		{
			File.Copy(labFileSrc, labFileDst, true);
		}
		catch (IOException)
		{
			Console.WriteLine("Ошибка при копировании файла. Возможно файл отсутствует по пути ../ex1/lab.dat");
			return;
		}

		// Создаем копию файла lab.dat в домашней директории
		byte[] labFileData = File.ReadAllBytes(labFileSrc);
		File.WriteAllBytes(labBackupFileDst, labFileData);

		// Выводим информацию о файле lab.dat
		FileInfo labFileInfo = new FileInfo(labFileSrc);
		Console.WriteLine($"Размер файла lab.dat: {labFileInfo.Length} байт");
		Console.WriteLine($"Время последнего изменения файла lab.dat: {labFileInfo.LastWriteTime}");
		Console.WriteLine($"Время последнего доступа к файлу lab.dat: {labFileInfo.LastAccessTime}");
	}
}