using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagmentSystem.Data;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveTypes;
using AutoMapper;
using LeaveManagmentSystem.Services;

namespace LeaveManagmentSystem.Controllers
{
    public class LeaveTypesController : Controller
    {
        private const string LeaveTypeNameExists = "Leave Type with the same or similar name already exists.";
        private readonly ILeaveTypesService _leaveTypesService;

        public LeaveTypesController(ILeaveTypesService leaveTypesService)
        {
            this._leaveTypesService = leaveTypesService;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {

            var viewData = await _leaveTypesService.GetAllAsync();
          
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<ActionResult<ReadVM>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var view = await _leaveTypesService.GetByIdAsync<ReadVM>(id.Value);
           
            return View(view);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            if (createVM == null) {
                return BadRequest();
            }

            bool checkNameRepeated = await _leaveTypesService.IsLeaveTypeNameRepeatedAsync(createVM.Name);

            if(checkNameRepeated) {
                ModelState.AddModelError(nameof(createVM.Name), LeaveTypeNameExists);
                return View(createVM);
            }

            if (ModelState.IsValid)
            {
                await _leaveTypesService.CreateAsync(createVM);
                return RedirectToAction(nameof(Index));
            }
            return View(createVM);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var view = await _leaveTypesService.GetByIdAsync<EditVM>(id.Value);
            if (view == null)
            {
                return NotFound();
            }
            return View(view);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  EditVM EditViewModel)
        {
            if (id != EditViewModel.Id)
            {
                return NotFound();
            }

            bool checkNameRepeated = await _leaveTypesService.IsLeaveTypeNameRepeatedForEditAsync(id, EditViewModel);

            if (checkNameRepeated)
            {
                ModelState.AddModelError(nameof(EditViewModel.Name), LeaveTypeNameExists);
                return View(EditViewModel);
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                   
                    await _leaveTypesService.EditAsync(EditViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_leaveTypesService.LeaveTypeExists(EditViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(EditViewModel);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var view = await _leaveTypesService.GetByIdAsync<ReadVM>(id.Value);
            if (view == null)
            {
                return NotFound();
            }

            return View(view);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await _leaveTypesService.GetByIdAsync<ReadVM>(id);
            if (leaveType != null)
            {
               await _leaveTypesService.Remove(id);
            }

   
            return RedirectToAction(nameof(Index));
        }

    }

}
