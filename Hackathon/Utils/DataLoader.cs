using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hackathon
{
    public static class DataLoader
    {
        public static List<T> LoadEmployees<T>(string filePath, Func<int, string, T> createEntity) where T : Participant
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл не найден: {filePath}");
                return new List<T>();
            }

            return File.ReadAllLines(filePath)
                       .Skip(1) // Пропускаем заголовок
                       .Select(line => line.Split(';'))
                       .Where(parts => parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[1]))
                       .Select(parts =>
                       {
                           int id = int.Parse(parts[0]);
                           string name = parts[1];
                           return createEntity(id, name);
                       })
                       .ToList();
        }
    }
}
