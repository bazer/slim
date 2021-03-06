using System;
using System.Collections.Generic;
using Slim;
using Slim.Interfaces;
using Slim.Attributes;

namespace Tests.Models
{
    [Name("dept_emp")]
    public partial class dept_emp : ITableModel
    {
        [PrimaryKey]
        [ForeignKey("departments", "dept_no", "dept_emp_ibfk_2")]
        [Type("char", 4)]
        public virtual string dept_no { get; set; }

        [Relation("departments", "dept_no")]
        public virtual departments departments { get; }

        [PrimaryKey]
        [ForeignKey("employees", "emp_no", "dept_emp_ibfk_1")]
        [Type("int")]
        public virtual int emp_no { get; set; }

        [Relation("employees", "emp_no")]
        public virtual employees employees { get; }

        [Type("date")]
        public virtual DateTime from_date { get; set; }

        [Type("date")]
        public virtual DateTime to_date { get; set; }

    }
}