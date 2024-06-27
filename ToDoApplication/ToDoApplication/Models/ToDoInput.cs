using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public class ToDoInput
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter the task")]
        public string Task {  get; set; }

        public string Status { get; set; } = "Pending";
    }
}
