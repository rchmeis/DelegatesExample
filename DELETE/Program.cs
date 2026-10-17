using System.Security.Cryptography.X509Certificates;

namespace DelegatesExamples
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //BUSINESS RULES:
            //zone1 25%, zone2 12% high risk, zone3 8%, zone4 4% high risk
            //high risk= USD25

            //SHIPPING LOGIC SEPARATED FROM MAIN PROGRAM. SEE Shipping CLASS.

            Console.WriteLine("CALCULATE FEE");

            string zone="";
            while (zone != "0")
            {
                Console.Write("Indtast zone - skriv 1, 2, 3 eller 4. Tast 0 for at lukke programmet");
                zone = Console.ReadLine().Trim();

                if (zone == "0")
                {
                    break;
                }

                //Retrieving the zone object
                Shipping theDestination = Shipping.GetDestination(zone);

                Console.Write("Indtast pris: ");
                decimal price = Convert.ToDecimal(Console.ReadLine().Trim());

                //Adding the specific fee calculation method to the delegate
                ShippingFeesDelegate s = theDestination.CalcShippingFee;

                //Setting the fee to 0 but adding 25 if the zone is high risk using 
                //composable delegates and a ref parameter
                //Ref parameters can be manipulated inside the one delegate and be passed on to the next.

                //Note that I used an inline anonymous delegate for this. It simply takes the fee which was
                //calculated in the previous delegate and adds 25 to that number.
                decimal fee = 0.0m;
                if (theDestination.isHighRisk)
                {
                    s += delegate (decimal price, ref decimal fee)
                    {
                        fee += 25;
                    };
                }
                s(price, ref fee);

                Console.WriteLine("The fee is: " + fee);

            }
                     
        }
  
    }
}
