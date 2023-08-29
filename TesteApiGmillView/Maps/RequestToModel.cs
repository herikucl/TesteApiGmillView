using AutoMapper;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Requests.ProjectR;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Maps
{
    public class RequestToModel : Profile
    {
        public RequestToModel()
        {
            CreateMap<CreateCompanyRequest, Company>();
            CreateMap<EditCompanyRequest, Company>();

            CreateMap<EditEmployeeRequest, Employee>();
            CreateMap<CreateEmployeeRequest, Employee>();

            CreateMap<EditProjectRequest, Project>();
            CreateMap<CreateProjectRequest, Project>();

            CreateMap<AddEmployeeToProjectRequest, EmployeeProject>();
        }
    }
}
