using Microsoft.Maui.Graphics;

namespace LifeManagementApp.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public Color PriorityColor
        {
            get
            {
                return Priority switch
                {
                    1 => Colors.Red,
                    2 => Colors.Orange,
                    3 => Colors.Green,
                    _ => Colors.Gray
                };
            }
        }

        public Color DueDateColor
        {
            get
            {
                if (DueDate.Date < DateTime.Today)
                    return Colors.Red;

                if (DueDate.Date == DateTime.Today)
                    return Colors.Orange;

                return Colors.Gray;
            }
        }
    }
}
