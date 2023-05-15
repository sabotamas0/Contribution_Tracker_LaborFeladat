using ContributionTracker.Interfaces;
using ContributionTracker.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ContributionTracker.InterfaceImplementations
{
    public class TransactionService : ITransactionService 
    {
        private ITransactionRepository _transactionRepository;
        public TransactionService(IEnumerable<ITransactionRepository> repositories) 
        {
            foreach (var repository in repositories) 
            {
                if(repository is JsonRepository) // here we can change, which data access we want to use
                {
                    _transactionRepository = repository;
                    break;
                }
            }
        }
        public void AddTransaction(TransactionDto transaction)
        {
            _transactionRepository.Write(transaction);
        }

        public List<int> ContributionsForYear(DateTime dateTime)
        {
            var chartData = new List<int>();
            var transactions = GetAllTransactions();

            var sumContributions = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionDate.Year == dateTime.Year)
                {
                    ++sumContributions;
                }
            }
            chartData.Add(sumContributions);
            return chartData;
        }

        /* TODO: implement last endpoint in dashboard 
          
        public List<Tuple<string, List<double>>> AverageContributionsByMonthAndYear()
        {

        }   
        */
        public List<Tuple<string, List<int>>> ContributionsIncreaseDecreaseByYear()
        {
            var contributionsIncreaseDecreaseByYear = new List<Tuple<string, List<int>>>();

            var yearsAndContributions = new List<Tuple<int, List<int>>>();
            var transactions = GetAllTransactions();

            foreach (var transaction in transactions)
            {
                if(!yearsAndContributions.Any(m => m.Item1 == transaction.TransactionDate.Year))
                {
                    yearsAndContributions.Add(new Tuple<int, List<int>>(transaction.TransactionDate.Year,ContributionsForYear(transaction.TransactionDate)));
                }
            }
            var orderedyearsAndContributions = yearsAndContributions.OrderBy(m => m.Item1).ToList(); // ordering by year

            // label ->  <-decrease% year increase%-> this  was the concept for displaying data according to the user story
            //TODO: create local methods(at least) to clean up this mess... or create a helper class beacuse this is too convoluted

            for (int i = 0; i < orderedyearsAndContributions.Count;++i)
            {
                double previousValue = 0;
                double currentValue = 0;
                double nextValue = 0;
                double nextPercentage = 0;
                double previousPercentage = 0;

                if(i == orderedyearsAndContributions.Count - 1) // if the index is at the last item there's no next, and we need to break out from the loop
                {
                    currentValue = orderedyearsAndContributions[i].Item2.Sum();
                    previousValue = orderedyearsAndContributions[i - 1].Item2.Sum();

                    if(previousValue > currentValue) //decrease
                    {
                        previousPercentage = (previousValue - currentValue) / previousValue * 100; // percentage increase/decrease formulas didn't work...
                    }
                    else
                    {
                        previousPercentage = Math.Abs((previousValue - currentValue) / previousValue * 100);
                    }

                    contributionsIncreaseDecreaseByYear.Add(new Tuple<string, List<int>>($"{Math.Round(previousPercentage,2)}%<-{orderedyearsAndContributions[i].Item1}", orderedyearsAndContributions[i].Item2));
                    break;
                }
                if (i == 0) // if index is 0 there's no previous year
                {
                    currentValue = orderedyearsAndContributions[i].Item2.Sum();
                    nextValue = orderedyearsAndContributions[i + 1].Item2.Sum();
                    if (nextValue > currentValue) //decrease
                    {
                        nextPercentage = (currentValue - nextValue) / currentValue * 100;
                    }
                    else
                    {
                        nextPercentage = Math.Abs((currentValue - nextValue) / currentValue * 100);
                    }
                    contributionsIncreaseDecreaseByYear.Add(new Tuple<string, List<int>>($"{orderedyearsAndContributions[i].Item1}->{Math.Round(nextPercentage,2)}%", orderedyearsAndContributions[i].Item2));
                }
                else
                {
                    currentValue = orderedyearsAndContributions[i].Item2.Sum();
                    nextValue = orderedyearsAndContributions[i + 1].Item2.Sum();
                    previousValue = orderedyearsAndContributions[i - 1].Item2.Sum();

                    if (nextValue > currentValue) //decrease
                    {
                        nextPercentage = (currentValue - nextValue) / currentValue * 100;
                    }
                    else
                    {
                        nextPercentage = Math.Abs((currentValue - nextValue) / currentValue * 100);
                    }
                    if (previousValue > currentValue) //decrease
                    {
                        previousPercentage = (previousValue - currentValue) / previousValue * 100;
                    }
                    else
                    {
                        previousPercentage = Math.Abs((previousValue - currentValue) / previousValue * 100);
                    }
                    contributionsIncreaseDecreaseByYear.Add(new Tuple<string, List<int>>($"{Math.Round(previousPercentage,2)}%<-{orderedyearsAndContributions[i].Item1}->{Math.Round(nextPercentage,2)}%", orderedyearsAndContributions[i].Item2));
                }
            }
            return contributionsIncreaseDecreaseByYear;
        }
        public List<Tuple<string, List<int>>> ContributionsByMonthForCurrentYear() // a not so elegant way to get all contributions by months for a year...
        {
            var chartData = new List<Tuple<string, List<int>>>();
            var currYear = DateTime.Now.Year;

            void IncrementIfExists(string month) // local method for avoiding DRY
            {
                var selectedMonth = chartData.Where(m => m.Item1 == month).Single();
                var currItem2Count = selectedMonth.Item2.Count;
                
                selectedMonth.Item2.Clear();
                selectedMonth.Item2.Add(++currItem2Count);
            }

            var transactions = GetAllTransactions();

            foreach(var transaction in transactions)
            {
                if(transaction.TransactionDate.Year == currYear)
                {
                    switch(transaction.TransactionDate.Month)
                    {
                        case 1:
                            if(!chartData.Any(m =>m.Item1=="January"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("January", new List<int>()));
                                IncrementIfExists("January");
                            }
                            else
                            {
                                IncrementIfExists("January");
                            }
                            break;
                        case 2:
                            if (!chartData.Any(m => m.Item1 == "February"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("February", new List<int>()));
                                IncrementIfExists("February");
                            }
                            else
                            {
                                IncrementIfExists("February");
                            }
                            break;
                        case 3:
                            if (!chartData.Any(m => m.Item1 == "March"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("March", new List<int>()));
                                IncrementIfExists("March");
                            }
                            else
                            {
                                IncrementIfExists("March");
                            }
                            break;
                        case 4:
                            if (!chartData.Any(m => m.Item1 == "April"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("April",new List<int>()));
                                IncrementIfExists("April");
                            }
                            else
                            {
                                IncrementIfExists("April");
                            }
                            break;
                        case 5:
                            if (!chartData.Any(m => m.Item1 == "May"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("May", new List<int>()));
                                IncrementIfExists("May");
                            }
                            else
                            {
                                IncrementIfExists("May");
                            }
                            break;
                        case 6:
                            if (!chartData.Any(m => m.Item1 == "June"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("June", new List<int>()));
                                IncrementIfExists("June");
                            }
                            else
                            {
                                IncrementIfExists("June");
                            }
                            break;
                        case 7:
                            if (!chartData.Any(m => m.Item1 == "July"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("July", new List<int>()));
                                IncrementIfExists("July");
                            }
                            else
                            {
                                IncrementIfExists("July");
                            }
                            break;
                        case 8:
                            if (!chartData.Any(m => m.Item1 == "August"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("August", new List<int>()));
                                IncrementIfExists("August");
                            }
                            else
                            {
                                IncrementIfExists("August");
                            }
                            break;
                        case 9:
                            if (!chartData.Any(m => m.Item1 == "September"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("September", new List<int>()));
                                IncrementIfExists("September");
                            }
                            else
                            {
                                IncrementIfExists("September");
                            }
                            break;
                        case 10:
                            if (!chartData.Any(m => m.Item1 == "October"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("October", new List<int>()));
                                IncrementIfExists("October");
                            }
                            else
                            {
                                IncrementIfExists("October");
                            }
                            break;
                        case 11:
                            if (!chartData.Any(m => m.Item1 == "November"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("November", new List<int>()));
                                IncrementIfExists("November");
                            }
                            else
                            {
                                IncrementIfExists("November");
                            }
                            break;
                        case 12:
                            if (!chartData.Any(m => m.Item1 == "December"))
                            {
                                chartData.Add(new Tuple<string, List<int>>("December", new List<int>()));
                                IncrementIfExists("December");
                            }
                            else
                            {
                                IncrementIfExists("December");
                            }
                            break;
                    }
                }
            }
            var orderedChartData = chartData.OrderBy(m => DateTime.ParseExact(m.Item1,"MMMM", CultureInfo.InvariantCulture)).ToList();
            return orderedChartData;
        }

        public void DeleteTransaction(Guid transactionId)
        {
            _transactionRepository.Delete(transactionId);
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactionRepository.Read();
        }

        public void UpdateTransaction(TransactionDto transaction)
        {
            _transactionRepository.Update(transaction);
        }
    }
}
