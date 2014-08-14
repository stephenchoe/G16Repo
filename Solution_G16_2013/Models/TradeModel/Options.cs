using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.Models.TradeModel
{
 
    public class Options : Derivative
    {
        public OptionsType OptionType { get; set; }
       

    }
   

    public class OptionsContract : DerivativeContract
    {
        public OptionsDirection OptionsDirection { get; set; }
    
        public double StrikePrice { get; set; }
       
     
    }


    public enum OptionsType
    {
        Europe,
        US
    }
    public enum OptionsDirection
    {
        Call,
        Put
    }



}
