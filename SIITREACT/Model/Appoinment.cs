using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIITREACT.Model
{
    [Table("Appointments")]
    public class Appointment
    {
        public int Id { get; set; } 
        public DateTime DateAndTime { get; set; } 
        public string Description { get; set; } 
        public string UserId { get; set; } 
       
        public Appointment(DateTime dateAndTime, string description, string userId)
        {
            DateAndTime = dateAndTime;
            Description = description;
            UserId = userId;
        }

       
        public Appointment() { }
    }
}