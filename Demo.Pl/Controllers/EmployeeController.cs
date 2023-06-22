using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.Pl.Helper;
using Demo.Pl.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Demo.Pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepo  _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeController(/*IEmployeeRepo employeeRepo,*/ IUnitOfWork unitOfWork,IMapper mapper)
        {
            //_employeeRepo = employeeRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  IActionResult Index(string SearchValue="")
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                 employees = _unitOfWork.EmployeeRepo.GetAll();
            }
            else
            {
                employees = (IEnumerable<Employee>)_unitOfWork.EmployeeRepo.Search(SearchValue);
            }
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployees);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeviewmodel)
        {
            if (ModelState.IsValid)
            {
                employeeviewmodel.ImageUrl = DocumentSettings.UploadFile(employeeviewmodel.Image, "Images");
                var mappedEmployees = _mapper.Map<Employee>(employeeviewmodel);
                _unitOfWork.EmployeeRepo.Add(mappedEmployees);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeviewmodel);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = _unitOfWork.EmployeeRepo.Get(id.Value);
            if (employee is null)
                return NotFound();
            var mappedEmployees = _mapper.Map<EmployeeViewModel>(employee);

            return View(ViewName, mappedEmployees);
        }
        public IActionResult Update(int? id)
        {
            if (id is null)
                return NotFound();
            var employee = _unitOfWork.EmployeeRepo.Get(id.Value);

            var mappedEmployees = _mapper.Map<EmployeeViewModel>(employee);
            if (employee is null)
                return NotFound();
            return View(mappedEmployees);

            //return Details(id, "Update");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee=_mapper.Map<Employee>(employeeViewModel);
                    _unitOfWork.EmployeeRepo.Update(mappedEmployee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(employeeViewModel);

        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                DocumentSettings.DeletFile("Images", employee.ImageUrl);

                _unitOfWork.EmployeeRepo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }
        }
    }
}
