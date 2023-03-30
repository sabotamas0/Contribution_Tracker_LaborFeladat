using ContributionTracker.Interfaces;
using ContributionTracker.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StructureMap;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ContributionTracker.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService) 
        {
            _transactionService = transactionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNewTransactionForm(TransactionDto? model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveNewTransaction(TransactionDto model)
        {
            var validator = new TransactionDtoValidator();

            ValidationResult result = validator.Validate(model);

            if (result.IsValid)
            {
                _transactionService.AddTransaction(model);
                return View("Index");
            }
            else
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);

                }
                return View("AddNewTransactionForm",model);
            }
        }

    }
}
