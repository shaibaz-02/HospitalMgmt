using AutoMapper;
using HospitalMgmt.Application.DTOs;
using HospitalMgmt.Application.Interfaces;
using HospitalMgmt.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalMgmt.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _departmentService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<DepartmentViewModel>>(dtos));
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _departmentService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(_mapper.Map<DepartmentViewModel>(dto));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _departmentService.CreateAsync(_mapper.Map<CreateDepartmentDto>(model));
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Department created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(nameof(model.Name), result.ErrorMessage!);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _departmentService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(_mapper.Map<DepartmentFormViewModel>(dto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentFormViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var result = await _departmentService.UpdateAsync(_mapper.Map<UpdateDepartmentDto>(model));
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Department updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(nameof(model.Name), result.ErrorMessage!);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _departmentService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(_mapper.Map<DepartmentViewModel>(dto));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _departmentService.DeleteAsync(id);

            if (result.Success)
                TempData["SuccessMessage"] = "Department deleted successfully.";
            else
                TempData["ErrorMessage"] = result.ErrorMessage;

            return RedirectToAction(nameof(Index));
        }
    }
}