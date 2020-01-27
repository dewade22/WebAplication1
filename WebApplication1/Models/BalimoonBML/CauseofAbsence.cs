﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class CauseofAbsence
    {
        public int CauseofAbsenceId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UnitofMeasureCode { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}