﻿using MayNghien.Infrastructures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Commons.Enums;

namespace Todo.Models.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Tier Priority { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}
