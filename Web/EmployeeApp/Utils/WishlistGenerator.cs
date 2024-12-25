using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary.Contracts;

namespace EmployeeApp.Utils
{
    public static class WishlistGenerator
    {
        // Модифицированный метод для генерации вишлиста для одного сотрудника
        public static PreferencesMessage GenerateWishlist(
            List<Employee> forEmployees, 
            List<Employee> offEmployees, 
            int employeeId,
            string role)
        {
            if (forEmployees == null || offEmployees == null)
            {
                throw new ArgumentNullException("Списки участников не должны быть null");
            }

            var employee = forEmployees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                throw new ArgumentException($"Сотрудник с ID {employeeId} не найден в списке сотрудников.");
            }

            Random random = new Random();
            var allOffIds = offEmployees.Select(t => t.Id).ToList();

            // Перемешиваем идентификаторы сотрудников в списке offEmployees
            var randomizedIds = allOffIds.OrderBy(_ => random.Next()).ToArray();

            // Создаем PreferencesMessage для конкретного сотрудника
            var preferencesMessage = new PreferencesMessage
            {
                EmployeeId = employee.Id,
                Role = role,
                Preferences = randomizedIds
            };

            return preferencesMessage;
        }
    }
}
