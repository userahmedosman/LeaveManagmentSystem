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

namespace LeaveManagmentSystem.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const string LeaveTypeNameExists = "Leave Type with the same or similar name already exists.";
        public LeaveTypesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {

            var data = await _context.LeaveTypes.ToListAsync();


            var viewData = _mapper.Map<List<IndexVM>>(data);
          
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            if (leaveType == null) {
            
                return NotFound();
            }

            var view = _mapper.Map<IndexVM>(leaveType);

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

            bool checkNameRepeated = await _context.LeaveTypes.AnyAsync(lt => createVM.Name.Contains(lt.Name));

            if(checkNameRepeated) {
                ModelState.AddModelError(nameof(createVM.Name), LeaveTypeNameExists);
                return View(createVM);
            }
            var leaveType = _mapper.Map<LeaveType>(createVM);

            if (ModelState.IsValid)
            {
                _context.Add(leaveType);
                await _context.SaveChangesAsync();
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

            var leaveType = await _context.LeaveTypes.FindAsync(id);

            var view = _mapper.Map<EditVM>(leaveType);
            if (leaveType == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LeaveDurationInDays")] EditVM EditViewModel)
        {
            if (id != EditViewModel.Id)
            {
                return NotFound();
            }

            bool checkNameRepeated = await _context.LeaveTypes.AnyAsync(lt => EditViewModel.Name.Contains(lt.Name) && lt.Id != EditViewModel.Id);

            if (checkNameRepeated)
            {
                ModelState.AddModelError(nameof(EditViewModel.Name), LeaveTypeNameExists);
                return View(EditViewModel);
            }

            if (ModelState.IsValid)
            {
                var leaveType = _mapper.Map<LeaveType>(EditViewModel);
                try
                {
                   
                    _context.Update(leaveType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveType.Id))
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

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var view = _mapper.Map<IndexVM>(leaveType);

            return View(view);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }
    }
}
