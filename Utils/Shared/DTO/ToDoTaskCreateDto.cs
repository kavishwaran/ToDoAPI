﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Shared.Enums;

namespace Utils.Shared.DTO
{
    public class ToDoTaskCreateDto
    {
        //public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusEnum StatusEn { get; set; }
        public int ToDoListId { get; set; }
    }
}
