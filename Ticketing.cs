using System;
namespace Travel_Agency
{
    class Customer//First of two classes used.
    {
        int CustomerAge;
        string CustomerFName;
        string CustomerLName;
        Int32 CustomerPassport;
        double CustomerPrice;
        string CustomerTitle;
        double KidDiscount;
        double SeniorDiscount;

        public Customer(string passengerFName, Int32 passportNumber, int passengerAge, double standardFare)//While technically an identifier, it was more useful to have it used as an int.
        {
            Customer_Title = passengerFName;//Checks passenger name
            Customer_Passport = passportNumber;//Checks Passport Number
            Customer_Age = passengerAge;//Uses passengerAge
            KidDiscount = 100.0 / 100.0; //"Remove all value if the passenger's a kid."
            SeniorDiscount = 20.0 / 100.0;//"20% discount if senior citizen"
            //Since lots of names are too short for 5 characters, I assume you mean First AND Last? (example: "Joel" Is an "invalid input".)
            string[] splittedTitle = CustomerTitle.Split(' ');//I googled how to do this, and it recommended "splitting" the input into two.
            if (splittedTitle.Length == 1)//With a tutor's help and the class's textbook on page 483, I managed to do the following:
                Customer_FName = Customer_Title;
            else if (splittedTitle.Length == 2)
            {
                Customer_FName = splittedTitle[0];//First name array Uses FName
                Customer_LName = splittedTitle[1];//Last name array. Uses LName
            }
            else
            {
                for (int i = 1; i < splittedTitle.Length; i++)
                    Customer_LName = Customer_LName + splittedTitle[i];//Split ends.
            }

            CalculatePrice(standardFare);//CalculatePrice method.
        }

        public void CalculatePrice(double standardFare)//This isn't a bank with rapid transactions.
        {
            if (Customer_Age < 3)//If under 3, they ride for free
                Customer_Price = standardFare - (standardFare * Kid_Discount);
            else if (Customer_Age > 65)//if over 65, 20% discount
                Customer_Price = standardFare - (standardFare * Senior_Discount);
            else if (Customer_Age >= 3 && Customer_Age <= 18) //Using the double ampersands for "and" since a single is for booleans.
                Customer_Price = standardFare - (standardFare * (10.0 / 100.0));//Kid discount 10%.
            else
                Customer_Price = standardFare;//Otherwise, standard fare.
        }

        public void Display()//Display method is used
        {
            Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", Customer_Title, Customer_Passport, Customer_Price));
        }

        public int Customer_Age
        {
            get
            {
                return CustomerAge;
            }

            set
            {
                CustomerAge = value;
            }
        }

        public string Customer_FName
        {
            get
            {
                return CustomerFName;
            }

            set
            {
                CustomerFName = value;
            }
        }

        public string Customer_LName
        {
            get
            {
                return CustomerLName;
            }

            set
            {
                CustomerLName = value;
            }
        }

        public Int32 Customer_Passport //taking the passport, accepting it as an int
        {
            get
            {
                return CustomerPassport;
            }

            set
            {
                CustomerPassport = value;
            }
        }

        public double Customer_Price
        {
            get
            {
                return CustomerPrice;
            }

            set
            {
                CustomerPrice = value;
            }
        }

        public string Customer_Title
        {
            get
            {
                return CustomerTitle;
            }

            set
            {
                CustomerTitle = value;
            }
        }

        public double Kid_Discount //discounting the 100%
        {
            get
            {
                return KidDiscount;
            }

            set
            {
                KidDiscount = value;
            }
        }

        public double Senior_Discount
        {
            get
            {
                return SeniorDiscount;
            }

            set
            {
                SeniorDiscount = value; //Basically these are all the usual constructor types, but Mac doesn't do tabs so it's in the same window.
            }
        }

    }
    class Application//As specified, only two classes are used.
    {
        int GetAge()//declares that GetAge is an integer. Not a double, because no one writes "I'm 29.9 years old!" 
        {
            Console.Write("Enter Age:");
            string ageStr = Console.ReadLine();//needs to be read as a string before being parsed.
            int age;//Searches for int
            while (!Int32.TryParse(ageStr, out age) || age < 0)//now it's an int!
            {
                Console.Write("Invalid Input. Please Enter Age (use a whole number) :");//error message given, but per specs, must be ">0" which is "greater than or equal to 0"
                ageStr = Console.ReadLine();//age needed to be differentiated, thus ageStr.
            }
            return age;
        }
        double GetAirFare()
        {
            return 1000;//At no point was the standard fare ever given for a ticket, so I just went and wrote "1,000" since it was a nice, easy value to check my math by. "#Overly honest research methods."
        }

        Customer GetInformationFromCust()
        {
            string passengerFName = GetTitle();
            Int32 passportNumber = GetPassportNo();
            int passengerAge = GetAge();
            double standardFare = GetAirFare();
            Customer customer = new Customer(passengerFName, passportNumber, passengerAge, standardFare);

            return customer;
        }
        Int32 GetPassportNo()//INT32 since this exceeds the max value for int16. Bear with me, read my justification below for keeping it an int.
        {
            Console.Write("Enter your, 8-digit Passport Number:");
            string passportNumberStr = Console.ReadLine();//I wrote 'str' at the end to differentiate it from passportNumber, and to remind myself that it's a string not an int
            Int32 passportNumber;//Best used as an int, really, because while it's nice as a string, with the time I had there wasn't anything in my head for how to both check that it was all numbers and reject if it wasn't. No harm to keeping it as an int for this use.
            while (!Int32.TryParse(passportNumberStr, out passportNumber) || passportNumberStr.Length != 8)
            {
                Console.Write("Invalid input. Please type in a Passport Number with 8 numeric digits :");//error message
                passportNumberStr = Console.ReadLine();
            }
            return passportNumber;//returns value
        }

        string GetTitle()
        {
            Console.Write("Enter Customer's First and Last Name:");
            string customerName = Console.ReadLine();
            while (customerName.Length < 5 || customerName.Length > 15)//accepts values only greater than  or equal to 5 or less or equal to 15, meeting requiremnt "between 5 and 15")
            {
                Console.Write("Incorrect Entry, please make this entry between 5 and 15 characters:");
                customerName = Console.ReadLine();//reads the input
            }
            return customerName;
        }
        static void Main(string[] args)
        {
            Application travelAgency = new Application();
            int numberOfCustomer = 0; //default val. for array
            Console.Write("Enter the number of Customers: ");//User instruction
            string numberOfCustomerStr = Console.ReadLine();
            while (!Int32.TryParse(numberOfCustomerStr, out numberOfCustomer) || numberOfCustomer < 0)
            {
                Console.Write("Invalid input, Please Enter Number of Customers (Must be greater than 0) :");
                numberOfCustomerStr = Console.ReadLine();
            }
            Customer[] customers = new Customer[numberOfCustomer]; //User input the number of them, setting the length of the array.
            for (int i = 1; i <= numberOfCustomer; i++)
            {
                Console.WriteLine("Customer " + i);
                Customer customer = travelAgency.GetInformationFromCust();
                customers[i - 1] = customer;
            }
            Console.WriteLine();
            Console.WriteLine(String.Format("|{0,30}|{1,30}|{2,30}|", "Customer Name", "Passport No", "Fare"));//formatting from 509 and last semester for the column titles. 
            double totalFare = 0;//displays total fare.
            for (int i = 0; i < numberOfCustomer; i++)//Who else likes loops?
            {
                customers[i].Display();
                totalFare += customers[i].Customer_Price;
            }
            Console.WriteLine();
            Console.Write("Total Fare for a Family is :" + totalFare);//Was I supposed to check for same last-name's and then add the values? This was unclear.
            //If I had more time I'd have done it that way with an 'if', and then prompt input for 'if the same, then, 'are these part of the same family?' and then added values.
            //Instead I just have the program always assume that it's a family buying their tickets all at once and trying to figure out the total value.
            //I also never store the passport value and then add it to the blacklist using arrrays. Again. Time constraints.
        }
    }
}


