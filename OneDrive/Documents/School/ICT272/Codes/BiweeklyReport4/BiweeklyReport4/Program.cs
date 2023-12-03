using System;
using System.Linq;
using System.Collections.Generic;

namespace SydneyHotel
{
    class Program
    {
        class ReservationDetail
        {
            public string customerName { get; set; }
            public int nights { get; set; }
            public string roomService { get; set; }
            public double totalPrice { get; set; }

        }
        // calculation of room service prices
        static double calculatedPrice(int nights, string roomService)
        {
            //declaring constant values for room rates
            const double baseRate1 = 100.0;
            const double baseRate2 = 80.5;
            const double baseRate3 = 75.3;
            const double roomServiceRate = 0.1;

            double price = 0;

            if (nights > 0 && nights <= 3)
                price = baseRate1 * nights;
            else if (nights > 3 && nights <= 10)
                price = baseRate2 * nights;
            else if (nights > 10 && nights <= 20)
                price = baseRate3 * nights;

            //room service should be checked to lower yes
            if (roomService.ToLower() == "yes")
                return price + price * roomServiceRate;
            else
                return price;

        }
        static void Main(string[] args)
        {
            //declaring a string for the dots and dashes for code efficiency
            string dots = new string('.', 25);
            string dashes = new string('-', 73);

            Console.Write(dots);
            Console.Write("Welcome to Sydney Hotel");
            Console.Write(dots);

            //constant values for minimum and maximum number of customers
            int n;
            const int minCustomers = 1;
            const int maxCustomers = 50;

            //do-while statement with more error handling to check if user inputs the valid number of customer limitation, if not valid it will ask user to enter again
            do
            {
                Console.Write($"\nEnter no. of Customers (between {minCustomers} and {maxCustomers}): ");

                while (!int.TryParse(Console.ReadLine(), out n))
                {
                    Console.Write("Please enter a valid number: ");
                }

                if (n < minCustomers || n > maxCustomers)
                {
                    Console.WriteLine($"Please enter a number between {minCustomers} and {maxCustomers}.");
                }

            } while (n < minCustomers || n > maxCustomers);

            Console.WriteLine(dashes);

            //declaring a list to store each customer
            List<ReservationDetail> rdList = new List<ReservationDetail>();

            for (int i = 0; i < n; i++)
            {
                ReservationDetail detail = new ReservationDetail();

                //constant values for minimum and maximum number of nights
                const int minNights = 1;
                const int maxNights = 20;
                int nights;

                Console.WriteLine($"Customer #{i + 1} details");

                Console.Write("Enter customer name: ");
                detail.customerName = Console.ReadLine();

                Console.Write("Enter the number of night/s: ");
                //while loop statement to check if user enters valid number of nights, and asks again if users enter invalid value
                while (!int.TryParse(Console.ReadLine(), out nights) ||
                       !(nights >= minNights && nights <= maxNights))
                {
                    Console.Write("Please enter a valid number of nights between 1 and 20: ");
                }
                detail.nights = nights;

                Console.Write("Enter yes/no to indicate whether you want a room service: ");
                // While loop to check if the user enters a valid value for room service
                while (true)
                {
                    string userInput = Console.ReadLine().ToLower();

                    if (userInput == "yes" || userInput == "no")
                    {
                        detail.roomService = userInput;
                        break; // Exit the loop if a valid value is entered
                    }
                    else
                    {
                        Console.Write("Please enter 'yes' or 'no': ");
                    }
                }

                detail.totalPrice = calculatedPrice(detail.nights, detail.roomService);

                Console.WriteLine($"The total price for {detail.customerName} is ${detail.totalPrice}");
                Console.WriteLine(dashes);

                rdList.Add(detail);
            }

            Console.WriteLine("\nPrinting reservation details...\n");

            Console.WriteLine(dashes + new string('-', 10));
            Console.WriteLine("\n\t\t\t\tSummary of reservation\n");
            Console.WriteLine(dashes + new string('-', 10));
            Console.WriteLine("Name\t\tNumber of nights\t\tRoom service\t\tCharge");

            //go through the each customer in the list
            foreach (var details in rdList)
            {
                Console.WriteLine($"{details.customerName}\t\t\t{details.nights}\t\t\t{details.roomService}\t\t\t{details.totalPrice}");
            }

            Console.WriteLine(dashes + new string('-', 10) + "\n");

            // Find and display the customer spending the most and least
            var (minPrice, minIndex) = rdList.Select((x, i) => (x.totalPrice, i)).Min();
            var (maxPrice, maxIndex) = rdList.Select((x, i) => (x.totalPrice, i)).Max();

            ReservationDetail minrd = rdList[minIndex];
            ReservationDetail maxrd = rdList[maxIndex];

            Console.WriteLine($"The customer spending the most is {maxrd.customerName}, for the price of ${maxrd.totalPrice}");
            Console.WriteLine($"The customer spending the least is {minrd.customerName}, for the price of ${minrd.totalPrice}\n");

            Console.WriteLine($"Press any key to continue....");

        }
    }
}
