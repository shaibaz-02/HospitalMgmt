using HospitalMgmt.Core.Entities;
using HospitalMgmt.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalMgmt.Web.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _unitOfWork.Doctors.GetAllWithDepartmentAsync();
            return View(doctors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdWithDepartmentAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDepartmentsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Doctors.AddAsync(doctor);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDepartmentsAsync(doctor.DepartmentId);
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            await LoadDepartmentsAsync(doctor.DepartmentId);
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {
            if (id != doctor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.Doctors.Update(doctor);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDepartmentsAsync(doctor.DepartmentId);
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdWithDepartmentAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            _unitOfWork.Doctors.Delete(doctor);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // One place to build the dropdown, so no action can forget it
        private async Task LoadDepartmentsAsync(object? selectedId = null)
        {
            var departments = await _unitOfWork.Departments.GetActiveDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", selectedId);
        }
    }
}