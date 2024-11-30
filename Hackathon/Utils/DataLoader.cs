using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public static class DataLoader
    {
        /// <summary>
        /// Загружает сотрудников из файла и преобразует их в список объектов Employee.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Список сотрудников.</returns>
        public static List<Employee> LoadEmployees(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Employee>();
            }

            return File.ReadAllLines(filePath)
                       .Skip(1) // Пропускаем заголовок
                       .Select(line => line.Split(';'))
                       .Where(parts => parts.Length == 2 && 
                                        int.TryParse(parts[0], out _) &&
                                        !string.IsNullOrWhiteSpace(parts[1]))
                       .Select(parts =>
                       {
                           int id = int.Parse(parts[0]);
                           string name = parts[1];
                           return new Employee(id, name);
                       })
                       .ToList();
        }

        /// <summary>
        /// Преобразует сотрудников в специфический тип (Junior или TeamLead).
        /// </summary>
        /// <typeparam name="T">Целевой тип (Junior или TeamLead).</typeparam>
        /// <param name="employees">Список сотрудников.</param>
        /// <param name="createEntity">Функция для создания целевого типа.</param>
        /// <returns>Список преобразованных объектов.</returns>
        public static List<T> ConvertEmployees<T>(IEnumerable<Employee> employees, Func<Employee, T> createEntity) where T : Employee
        {
            return employees.Select(createEntity).ToList();
        }
    }
}
