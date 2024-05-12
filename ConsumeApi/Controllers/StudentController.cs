using ConsumeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeApi.Controllers
{
    public class StudentController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7241/api");
        private readonly HttpClient _httpClient;

        public StudentController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<StudentViewModel> studentList = new List<StudentViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/student").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                studentList = JsonConvert.DeserializeObject<List<StudentViewModel>>(data);
            }

            return View(studentList);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            StudentViewModel student = new StudentViewModel();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/student/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel>(data);
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<CourseViewModel> courseList = new List<CourseViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/course").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                courseList = JsonConvert.DeserializeObject<List<CourseViewModel>>(data);
            }
            return View(courseList);
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel student)
        {
            try
            {
                string data = JsonConvert.SerializeObject(student);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/student", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student created.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {

            StudentViewModel student = new StudentViewModel();
            List<CourseViewModel> courseList = new List<CourseViewModel>();

            HttpResponseMessage studentTask = _httpClient.GetAsync(_httpClient.BaseAddress + "/student/" + id).Result;
            HttpResponseMessage courseTask = _httpClient.GetAsync(_httpClient.BaseAddress + "/course").Result;

            if (studentTask.IsSuccessStatusCode)
            {
                string studentData = studentTask.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel>(studentData);
            }

            if (courseTask.IsSuccessStatusCode)
            {
                string courseData = courseTask.Content.ReadAsStringAsync().Result;
                courseList = JsonConvert.DeserializeObject<List<CourseViewModel>>(courseData);
            }


            var studentCourseViewModel = new StudentCourseViewModel
            {
                Student = student,
                Course = courseList
            };

            return View(studentCourseViewModel);
        }

        [HttpPost]
        public IActionResult Update(StudentViewModel student) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(student);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/student/" + student.StudentId, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student updated.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/student/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student deleted.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
    
}
