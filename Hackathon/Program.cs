using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Hackathon
{
    class Program
    {
        static void Main(string[] args)
        {
            string juniorsFile = "Data/Juniors20.csv"; 
            string teamLeadsFile = "Data/Teamleads20.csv"; 

            HRManager hrManager = new HRManager(juniorsFile, teamLeadsFile);

            if (!hrManager.Juniors.Any())
            {
                Console.WriteLine("Нет доступных джунов для хакатона.");
                return; 
            }
            if (!hrManager.TeamLeads.Any())
            {
                Console.WriteLine("Нет доступных тимлидов для хакатона.");
                return; 
            }

            double averageHarmonicity = hrManager.ConductMultipleHackathons(1000);

            Console.WriteLine($"Средняя гармоничность по 1000 хакатонам: {averageHarmonicity:F2}");
        }
    }
}