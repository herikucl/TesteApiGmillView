using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Loader;
using System.Text;
using TesteApiGmillView.Domain.Requests.CompanyR;
using TesteApiGmillView.Domain.Requests.EmployeeR;
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

        public IActionResult SaveCreateEmployeer(string name, string document, string phone, int companyId)
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

                var responseTask = client.GetAsync("Employee/all");
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
                client.BaseAddress = new Uri("https://localhost:7160/api/Employee?id=");


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
            var employee = new EmployeeView { Id = id };
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Employee?Id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeView>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new EmployeeView();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(employee);
            }
        }

        public IActionResult EmployeeProjects(int id)
        {
            IEnumerable<ProjectView> projects;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Employee/all/Projects?Id={id}");
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


        public IActionResult Companies()
        {
            IEnumerable<CompanyView> companies;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7160/api/");

                var responseTask = client.GetAsync("Company/all");
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
                client.BaseAddress = new Uri("https://localhost:7160/api/Company?id=");


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

        public IActionResult CreateCompany()
        {
            return View();
        }

        public IActionResult DetailCompany(int id)
        {
            var company = new CompanyView { Id = id };
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Company?Id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CompanyView>();
                    readTask.Wait();
                    company = readTask.Result;
                }
                else
                {
                    company = new CompanyView();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(company);
            }
        }

        public IActionResult CompanyEmployees(string id, string name, string address)
        {
            IEnumerable<EmployeeView> employees;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync($"https://localhost:7160/api/Company/all/Employees?companyId={id}");
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