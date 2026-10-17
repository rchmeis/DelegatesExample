using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DelegatesExamples
{
    public delegate void ShippingFeesDelegate(decimal price, ref decimal fee);
    public abstract class Shipping
    {
        public bool isHighRisk { get; set; }
        public virtual void CalcShippingFee(decimal price, ref decimal fee) { }
        public static Shipping GetDestination(string zone)
        {
            return zone switch
            {
                "1" => new Zone1(),
                "2" => new Zone2(),
                "3" => new Zone3(),
                "4" => new Zone4(),
                _ => throw new ArgumentException("Insert valid zone")
            };

        }
    }

    public class Zone1:Shipping{
        
        public Zone1(){
            this.isHighRisk = false;
        }
        public override void CalcShippingFee(decimal price, ref decimal fee)
        {
            fee = price * 0.25m;
        }
    }

    public class Zone2 : Shipping
    {

        public Zone2()
        {
            this.isHighRisk = true;
        }
        public override void CalcShippingFee(decimal price, ref decimal fee)
        {
            fee = price * 0.12m;
        }
    }

    public class Zone3 : Shipping
    {

        public Zone3()
        {
            this.isHighRisk = false;
        }
        public override void CalcShippingFee(decimal price, ref decimal fee)
        {
            fee = price * 0.08m;
        }
    }

    public class Zone4 : Shipping
    {

        public Zone4()
        {
            this.isHighRisk = true;
        }
        public override void CalcShippingFee(decimal price, ref decimal fee)
        {
            fee = price * 0.04m;
        }
    }
}
