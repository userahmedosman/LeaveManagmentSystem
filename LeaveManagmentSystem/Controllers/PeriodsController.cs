using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagmentSystem.Data;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.Services.PeriodService;
using LeaveManagmentSystem.ViewModels.Periods;

namespace LeaveManagmentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PeriodsController : Controller
    {
        private readonly IPeriodService _periodSerivce;

        public PeriodsController(IPeriodService periodService)
        {
            this._periodSerivce = periodService;
        }

        // GET: Periods
        public async Task<IActionResult> Index()
        {
            return View(await _periodSerivce.GetAllPeriodsAsync());
        }

        // GET: Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodSerivce.GetPeriodByIdAysnc<PeriodReadVM>(id.Value);

            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PeriodCreateVM period)
        {
            bool checkValidPeriodExist = await _periodSerivce.CheckPeriodIsInitiatedAsync();

            if (checkValidPeriodExist)
            {
                ModelState.AddModelError(nameof(period.Name), "There is a valid period");
                return View(period);
            }
            bool checkCreatingPeriodIsValid = _periodSerivce.CheckPeriodToCreateIsValid(period);

            if (!checkCreatingPeriodIsValid) {
                ModelState.AddModelError(nameof(period.Name), $"This period is not a valid realtime period \nStart Date should be 01\\01\\{DateTime.Now.Year}\nEnd Date should be 12\\31\\{DateTime.Now.Year}");
                return View(period);
            }
            if (ModelState.IsValid)
            {
                await _periodSerivce.CreateAsync(period);
                return RedirectToAction(nameof(Index));
            }
            return View(period);
        }

        // GET: Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodSerivce.GetPeriodByIdAysnc<PeriodEditVM>(id.Value);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeriodEditVM period)
        {
            if (id != period.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _periodSerivce.EditPeriodNameAsync(period);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _periodSerivce.PeriodExists(period.Id))
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
            return View(period);
        }

        // GET: Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodSerivce.GetPeriodByIdAysnc<PeriodReadVM>(id.Value);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var period = await _periodSerivce.GetPeriodByIdAysnc<PeriodReadVM>(id);


            if (period.EndDate.Year == DateTime.Now.Year)
            {
              
                ModelState.AddModelError(nameof(period.Name), "This is a realtime period you can only delete old periods");
           
                return View(period);
            }
            if (period != null)
            {
                await _periodSerivce.RemoveOldPeriodAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        
    }
}
