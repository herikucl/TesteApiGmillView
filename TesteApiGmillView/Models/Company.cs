﻿namespace TesteApiGmillView.Models
{
    public class Company
    {
        public Company(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public Company(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public Company(int id, int companyId, string name, string description, DateTime date)
        {
            Id = id;
            CompanyId = companyId;
            Name = name;
            Description = description;
            Date = date;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Project> Projects { get; set; }
        public int CompanyId { get; }
        public string Description { get; }
        public DateTime Date { get; }
    }
}
