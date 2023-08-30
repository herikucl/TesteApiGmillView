using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.Loader;
using System.Text;
using System.Xml.Linq;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
using TesteApiGmillView.Domain.Requests.ProjectR;
using TesteApiGmillView.Domain.Response;
using TesteApiGmillView.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TesteApiGmillView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SaveCreateEmployee(string name, string document, string phone, int companyId)
        {
            using (var client = new HttpClient())
            {
                var employee = new CreateEmployeeRequest { Name = name, Document = document, Phone = phone, CompanyId = companyId };
                var jsonString = JsonConvert.SerializeObject(employee);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync($"https://localhost:7160/api/Employee", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return RedirectToAction("Employees");
            }
        }


        public IActionResult Employees()
        {
            IEnumerable<EmployeeView> employees;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7160/api/");

                var responseTask = client.GetAsync("Employee/All");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeView>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
                else
                {
                    employees = Enumerable.Empty<EmployeeView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(employees);
            }
        }

        public IActionResult DeleteEmployee(string id)
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.DeleteAsync($"https://localhost:7160/api/Employee?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }
            return RedirectToAction("Employees");
        }

        public IActionResult SaveEditEmployee(int id, string name, string document, string phone, int companyId)
        {
            using (var client = new HttpClient())
            {
                var employee = new Employee(id, name, document, phone, companyId);
                var jsonString = JsonConvert.SerializeObject(employee);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var responseTask = client.PutAsync($"https://localhost:7160/api/Employee?id={id}", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }
            return RedirectToAction("Employees");
        }

        public IActionResult EditEmployee(int id, string name, string document, string phone, int companyId)
        {
            return View();
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }

        public IActionResult DetailEmployee(int id)
        {
			IEnumerable<ProjectView> projects;

			using (var client = new HttpClient())
			{
				var responseTask = client.GetAsync($"https://localhost:7160/api/Employee/Project?id={id}");
				responseTask.Wait();
				var result = responseTask.Result;

				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<ProjectView>>();
					readTask.Wait();
					projects = readTask.Result;
				}
				else
				{
					projects = Enumerable.Empty<ProjectView>();
					ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
				}
				return View(projects);
			}
		}

        public IActionResult EmployeeProjects(int id)
        {
            IEnumerable<ProjectView> projects;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Employee/Project?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProjectView>>();
                    readTask.Wait();
                    projects = readTask.Result;
                }
                else
                {
                    projects = Enumerable.Empty<ProjectView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(projects);
            }
        }

        
        

        public IActionResult Companies()
        {
            IEnumerable<CompanyView> companies;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:7160/api/Company/All");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CompanyView>>();
                    readTask.Wait();
                    companies = readTask.Result;
                }
                else
                {
                    companies = Enumerable.Empty<CompanyView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(companies);
            }
        }


        public IActionResult CreateCompany()
        {
            return View();
        }

        public IActionResult SaveCreateCompany(string name, string address)
        {
            using (var client = new HttpClient())
            {
                var company = new CreateCompanyRequest { Name = name, Address = address };
                var jsonString = JsonConvert.SerializeObject(company);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync($"https://localhost:7160/api/Company", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return RedirectToAction("Companies");
            }
        }

        public IActionResult DeleteCompany(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7160/api/Company?id=");

                var responseTask = client.DeleteAsync($"https://localhost:7160/api/Company?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }
            return RedirectToAction("Companies");
        }

        public IActionResult SaveEditCompany(int id, string name, string address)
        {
            using (var client = new HttpClient())
            {
                var company = new Company(id, name, address);
                var jsonString = JsonConvert.SerializeObject(company);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var responseTask = client.PutAsync($"https://localhost:7160/api/Company?id={id}", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }
            return RedirectToAction("Companies");
        }

        public IActionResult EditCompany(int id, string name, string address)
        {
            return View();
        }


        public IActionResult DetailCompany(int id, string name, string address)
        {
            var company = new CompanyView { Id = id,Name = name,Address= address };
            return View(company);
        }

        public IActionResult CompanyEmployees(string id, string name, string address)
        {
            IEnumerable<EmployeeView> employees;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Company/All/Employees?companyId={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeView>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
                else
                {
                    employees = Enumerable.Empty<EmployeeView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(employees);
            }
        }

        public IActionResult CompanyProjects(string id, string name, string address)
        {
            IEnumerable<ProjectView> projects;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Company/All/Projects?companyId={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProjectView>>();
                    readTask.Wait();
                    projects = readTask.Result;
                }
                else
                {
                    projects = Enumerable.Empty<ProjectView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(projects);
            }
        }

        public IActionResult Projects()
        {
            IEnumerable<ProjectView> projects;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:7160/api/Project/All");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProjectView>>();
                    readTask.Wait();
                    projects = readTask.Result;
                }
                else
                {
                    projects = Enumerable.Empty<ProjectView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(projects);
            }
        }


        public IActionResult CreateProject()
        {
            return View();
        }

        
        public IActionResult SaveCreateProject(int companyId, string name, string description,string status)
        {
            using (var client = new HttpClient())
            {
                var project = new CreateProjectRequest { CompanyId = companyId, Name = name, Description = description, Status = status};
                var jsonString = JsonConvert.SerializeObject(project);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync($"https://localhost:7160/api/Project", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return RedirectToAction("Projects");
            }
        }

        public IActionResult DeleteProject(string id)
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.DeleteAsync($"https://localhost:7160/api/Project?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }
            return RedirectToAction("Projects");
        }

        public IActionResult EditProject(int id, int companyId, string name, string description)
        {
            return View();
        }

        public IActionResult SaveEditProject(int id, int companyId, string name, string description)
        {
            using (var client = new HttpClient())
            {
                var project = new Project(id, companyId, name,description);
                var jsonString = JsonConvert.SerializeObject(project);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var responseTask = client.PutAsync($"https://localhost:7160/api/Project?id={id}", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
            }
            return RedirectToAction("Projects");
        }

		public IActionResult DetailProject(int id)
		{
			IEnumerable<EmployeeView> employees;

			using (var client = new HttpClient())
			{
				var responseTask = client.GetAsync($"https://localhost:7160/api/Project/Employee?id={id}");
				responseTask.Wait();
				var result = responseTask.Result;

				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<EmployeeView>>();
					readTask.Wait();
					employees = readTask.Result;
				}
				else
				{
					employees = Enumerable.Empty<EmployeeView>();
					ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
				}
				return View(employees);
			}
		}

		public IActionResult ProjectEmployees(int id)
        {
            IEnumerable<EmployeeView> employees;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Project/Employee?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeView>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
                else
                {
                    employees = Enumerable.Empty<EmployeeView>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(employees);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}