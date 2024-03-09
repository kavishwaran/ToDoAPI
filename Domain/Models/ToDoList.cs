using Domain.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ToDoList : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
