using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIITREACT.Model
{
    [Table("Appointments")]
    public class Appointment
    {
        public int Id { get; set; } // Unique identifier for the appointment
        public DateTime DateAndTime { get; set; } // Date and time of the appointment
        public string Description { get; set; } // Description of the appointment
        public string UserId { get; set; } // Id of the user making the appointment
        // Constructor
        public Appointment(DateTime dateAndTime, string description, string userId)
        {
            DateAndTime = dateAndTime;
            Description = description;
            UserId = userId;
        }

        // Default constructor required for Entity Framework
        public Appointment() { }
    }
}