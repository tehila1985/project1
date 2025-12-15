using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class PassWord
    {
        public string? Password { get; set; }
        public int? Strength { get; set; }
    }
}
