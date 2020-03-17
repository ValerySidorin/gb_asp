using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using efcore_test.Models;
using efcore_test.Services;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace efcore_test.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        ILoggerFactory loggerFactory = LoggerFactory.Create(options =>
        {
            options.ClearProviders();
        });
        ILogger logger;

        public HomeController(ApplicationContext context)
        {
            db = context;
            loggerFactory.AddFile("logger.txt");
            logger = loggerFactory.CreateLogger("FileLogger");
        }

        
        public async Task<IActionResult> Index()
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            return View(await db.Employees.ToListAsync());
        }

        public async Task<IActionResult> Info(int? id)
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            if (id != null)
            {
                Employee emp = await db.Employees.FindAsync(id);
                if (emp != null)
                    return View(emp);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Add()
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            if (id != null)
            {
                Employee emp = await db.Employees.FindAsync(id);
                if (emp != null)
                    return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            db.Entry(employee).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            if (id != null)
            {
                Employee emp = await db.Employees.FindAsync(id);
                return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            logger.LogInformation($"DateTime: {DateTime.Now.ToString()} Method: {Request.Method} Protocol: {Request.Protocol} Path: {Request.Host + Request.Path}");
            if (id != null)
            {
                Employee emp = new Employee(id.Value);
                db.Entry(emp).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
