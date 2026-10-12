using HospitalMgmt.Core.Entities;
using HospitalMgmt.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalMgmt.Web.Controllers
{
    public class DepartmentsController: Controller
    {   
        private readonly IDepartmentRepository departmentRepository;
        // private readonly HospitalDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            this.departmentRepository=departmentRepository;
            _unitOfWork=unitOfWork;
        }

        [HttpGet]
            public async Task<IActionResult> Index()
            {
                var departments = await  _unitOfWork.Departments.GetAllAsync();
                return View(departments);
            }

            [HttpGet]
            public async Task<IActionResult> Details(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department==null)
            {
                return NotFound();
            }
            return View(department);
        }

       [HttpGet]
public async Task<IActionResult> Create()
{
    ViewBag.Departments = new SelectList(
        await _unitOfWork.Departments.GetAllAsync(),
        "Id",
        "Name");
    return View();
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Departments.AddAsync(department);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(
        await _unitOfWork.Departments.GetAllAsync(),
        "Id",
        "Name");
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department= await _unitOfWork.Departments.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            ViewBag.Departments = new SelectList(
        await _unitOfWork.Departments.GetAllAsync(),
        "Id",
        "Name");
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (department.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Departments.Update(department);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(
        await _unitOfWork.Departments.GetAllAsync(),
        "Id",
        "Name");
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department=await  _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.Departments.Delete(department);
                await _unitOfWork.SaveChangesAsync();
                TempData["SuccessMessage"] =
            "Department deleted successfully.";
            }catch(DbUpdateException)
            {
                    TempData["ErrorMessage"] =
            "Cannot delete this department because doctors are assigned to it.";
            }
            return RedirectToAction(nameof(Index));
            
        }


    }
}