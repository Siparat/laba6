using System;
using System.IO;

namespace BMPHeaders
{
	class Program5
	{
		static void Main(string[] args)
		{
			Console.Write("Введите имя файла: ");
			string filePath = Console.ReadLine() + ".bmp";

			try
			{
				using (FileStream fs = new FileStream(filePath, FileMode.Open))
				{
					byte[] bfType = new byte[2];
					fs.Read(bfType, 0, 2);
					uint bfSize = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					ushort bfReserved1 = BitConverter.ToUInt16(ReadLittleEndian(fs, 2), 0);
					ushort bfReserved2 = BitConverter.ToUInt16(ReadLittleEndian(fs, 2), 0);
					uint bfOffBits = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);

					uint biSize = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					uint biWidth = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					uint biHeight = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					ushort biPlanes = BitConverter.ToUInt16(ReadLittleEndian(fs, 2), 0);
					ushort biBitCount = BitConverter.ToUInt16(ReadLittleEndian(fs, 2), 0);
					uint biCompression = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					uint biSizeImage = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					int biXPelsPerMeter = BitConverter.ToInt32(ReadLittleEndian(fs, 4), 0);
					int biYPelsPerMeter = BitConverter.ToInt32(ReadLittleEndian(fs, 4), 0);
					uint biClrUsed = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);
					uint biClrImportant = BitConverter.ToUInt32(ReadLittleEndian(fs, 4), 0);


					// Вывод информации на консоль
					Console.WriteLine($"Размер файла: {bfSize} байт");
					Console.WriteLine($"Ширина: {biWidth} пикселей");
					Console.WriteLine($"Высота: {biHeight} пикселей");
					Console.WriteLine($"Количество бит на пиксель: {biBitCount}");
					Console.WriteLine($"Разрешение горизонтальное: {biXPelsPerMeter} пикселей на метр");
					Console.WriteLine($"Разрешение вертикальное: {biYPelsPerMeter} пикселей на метр");
					Console.WriteLine($"Тип сжатия: {(biCompression == 0 ? "без сжатия" : (biCompression == 1 ? "4бит RLE" : "8бит RLE"))}");
				}
			} catch (Exception ex) {
				Console.WriteLine($"Ошибка: {ex.Message}");
			}
		}

		// Функция для чтения little-endian данных из потока
		static byte[] ReadLittleEndian(FileStream fs, int length)
		{
			byte[] buffer = new byte[length];
			fs.Read(buffer, 0, length);
			Array.Reverse(buffer);
			for (int i = 0; i < length; i++)
			{
				if (buffer[i] < 0)
				{
					buffer[i] = (byte)(buffer[i] | 0xFF);
				}
			}
			return buffer;
		}
	}
}
