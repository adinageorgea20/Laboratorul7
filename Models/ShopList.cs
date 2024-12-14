using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
namespace GeorgeaAdinaLab7.Models
{
    public class ShopList
    {
         
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        [MaxLength(250), Unique]
        public string Description { get; set; } = "Default description";
        public DateTime Date { get; set; }

        [ForeignKey("Shop")] 
        public int ShopID { get; set; }
    }
}
