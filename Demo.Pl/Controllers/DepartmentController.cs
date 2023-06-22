using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System;

namespace Demo.Pl.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepo _departmentRepo;

        public IUnitOfWork _UnitOfWork;

        public DepartmentController(/*IDepartmentRepo departmentRepo*/ IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
            //_departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            //viewData => key -value
            //ViewData["Message"] = "Hello from department controller!![ViewData]";

            //viewBag => Dynamic property (object)
            //ViewBag.MessageViewBag = "Hello from department controller!![ViewBag]";
            TempData.Keep("Message");
            var drepartments = _UnitOfWork.DepartmentRepo.GetAll();

            return View(drepartments);
        }
        public IActionResult Create()
        {
            //ViewBag.MessageViewBag = "Hello from department controller!![ViewBag]";

            return View();
        
        }
        [HttpPost]
        public IActionResult Create( Department department)
        {

            TempData["Message"] = "Hello from department controller!![TempData]";
            if(ModelState.IsValid)
            {
                _UnitOfWork.DepartmentRepo.Add(department);
                return RedirectToAction(nameof( Index));
            }
            return View(department);
        }

        public IActionResult Details(int? id,string ViewName="Details")
        {
            if (id is null)
                return BadRequest();
            var department = _UnitOfWork.DepartmentRepo.Get(id.Value);
            if (department is null)
                return NotFound();
            return View(ViewName, department);
        }
        public IActionResult Update(int ? id)
        {
            //if( id is null)
            //    return BadRequest();
            //var department = _departmentRepo.Get(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
            return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int id, Department department) 
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _UnitOfWork.DepartmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
                

            }
            return View(department);
        
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id,Department department)
        {
            if (id!=department.Id)
                return BadRequest();
            try
            {
                _UnitOfWork.DepartmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(department);
            }
        }
    }
}
