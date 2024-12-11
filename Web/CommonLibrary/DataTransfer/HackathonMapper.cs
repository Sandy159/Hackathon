using System.Linq;

using CommonLibrary.Dto;
using CommonLibrary.Contracts;

namespace CommonLibrary.DataTransfer
{
    public static class CompitionMapper
    {
        // Employee
        public static EmployeeDto ToEntity(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Role = employee is Junior ? "Junior" : "TeamLead"
            };
        }

        public static Employee ToDomain(EmployeeDto entity)
        {
            return entity.Role == "Junior"
                ? new Junior(entity.Id, entity.Name)
                : new TeamLead(entity.Id, entity.Name);
        }

        // Wishlist
        public static WishlistDto ToEntity(Wishlist wishlist, List<Employee> employees, CompitionDto hackathon)
        {
            var employee = employees.FirstOrDefault(e => e.Id == wishlist.EmployeeId)
                ?? throw new InvalidOperationException($"Employee with ID {wishlist.EmployeeId} not found.");
            return new WishlistDto
            {
                Employee = ToEntity(employee),
                Preferences = wishlist.Preferences,
                Hackathon = hackathon
            };
        }

        public static Wishlist ToDomain(WishlistDto entity, List<Employee> employees)
        {
            var employee = employees.FirstOrDefault(e => e.Id == entity.Employee.Id)
                ?? throw new InvalidOperationException($"Employee with ID {entity.Employee.Id} not found.");
            return new Wishlist(employee.Id, entity.Preferences);
        }

        public static PreferencesDto ToEntity(Wishlist wishlist)
        {
            return new PreferencesDto
            {
                EmployeeId = wishlist.EmployeeId.ToString(),
                Preferences = wishlist.Preferences.Select(p => p.ToString()).ToList()
            };
        }

        // Team
        public static TeamDto ToEntity(Team team, CompitionDto hackathon)
        {
            return new TeamDto
            {
                TeamLead = ToEntity(team.TeamLead),
                Junior = ToEntity(team.Junior),  
                Hackathon = hackathon
            };
        }

        public static Team ToDomain(TeamDto entity, List<Employee> employees)
        {
            var teamLead = employees.First(e => e.Id == entity.TeamLead.Id);
            var junior = employees.First(e => e.Id == entity.Junior.Id);
            return new Team(teamLead, junior);
        }

        // Compition
        /*public static CompitionDto ToEntity(Compition compition, List<Employee> employees)
        {
            var hackathonDto = new CompitionDto
            {
                Id = compition.Id,
                Score = compition.Score,
                Wishlists = new List<WishlistDto>(),
                Teams = new List<TeamDto>()
            };

            hackathonDto.Wishlists = compition.Wishlists?.Select(w => ToEntity(w, employees, hackathonDto)).ToList() ?? new List<WishlistDto>();
            hackathonDto.Teams = compition.Teams?.Select(t => ToEntity(t, hackathonDto)).ToList() ?? new List<TeamDto>();

            return hackathonDto;
        }*/

        /*public static Compition ToDomain(CompitionDto entity, List<Employee> employees)
        {
            var wishlists = entity.Wishlists.Select(w => ToDomain(w, employees)).ToList();
            var teams = entity.Teams.Select(t => ToDomain(t, employees)).ToList();

            return new Compition
            {
                Id = entity.Id,
                Score = entity.Score,
                Wishlists = wishlists,
                Teams = teams
            };
        }*/
    }
}
