using ContributionTracker.Interfaces;
using ContributionTracker.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StructureMap;
using StructureMap.Query;
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

        [HttpGet]
        public IActionResult TransactionPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var model = _transactionService.GetAllTransactions();
            return PartialView(model);
        }

		[HttpGet]
		public IActionResult ModifyTransactionForm(TransactionDto model)
		{
			return PartialView(model);
		}

		[HttpPost]
        public IActionResult ModifyTransaction(TransactionDto transaction)
        {
			var validator = new TransactionDtoValidator();

			ValidationResult result = validator.Validate(transaction);

			if (result.IsValid)
            {
				_transactionService.UpdateTransaction(transaction);
				return View("TransactionPage");
			}
			else
			{
				foreach (ValidationFailure failer in result.Errors)
				{
					ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage); // passing the errors to ViewDictionary
                }
                ViewBag.ActionName = "Modify"; 
                return View("TransactionPage"); // Displaying the errors in the full view through ViewDictionary
            }
        }

        [HttpPost]
        public IActionResult DeleteTransaction(Guid transactionId)
        {
            _transactionService.DeleteTransaction(transactionId);
            return View("TransactionPage");
        }

        [HttpGet]
        public IActionResult AddNewTransactionForm(TransactionDto? model)
        {
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult SaveNewTransaction(TransactionDto model)
        {
            var validator = new TransactionDtoValidator();

            ValidationResult result = validator.Validate(model);

            if (result.IsValid)
            {
                _transactionService.AddTransaction(model);
                return View("TransactionPage");
            }
            else
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage); // passing the errors to ViewDictionary
                }
                ViewBag.ActionName = "Save";
                return View("TransactionPage");
            }
        }

    }
}
