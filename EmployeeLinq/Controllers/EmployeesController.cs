using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeLinq.Models;
namespace EmployeeLinq.Controllers
{
    public class EmployeesController : ApiController
    {
        LinqEntities db = new LinqEntities();
        // GET api/Employees  
        public IEnumerable<Employee> Get()
        {
            return db.Employees.ToList<Employee>();
        }
        // GET api/Employees/name  
        public IEnumerable<Employee> Get(string id)
        {
            var list = from g in db.Employees where g.FirstName == id select g;
            return list;
        }
        // POST api/Employees  
        public HttpResponseMessage Post(Employee value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(value);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        // PUT api/Employees/5  
        public Employee Put(int id, string FirstName, string LastName, string Gender, int Salary)
        {
            var obj = db.Employees.Where(n => n.id == id).SingleOrDefault();
            if (obj != null)
            {
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.Gender = Gender;
                obj.Salary = Salary;
          
                db.SaveChanges();
            }
            return obj;
        }
        // DELETE api/Employees/5  
        public void Delete(int id)
        {
            var obj = db.Employees.Find(id);
            db.Employees.Remove(obj);
            db.SaveChanges();
        }
    }
}