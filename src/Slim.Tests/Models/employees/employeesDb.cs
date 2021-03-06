using System;
using System.Collections.Generic;
using Slim;
using Slim.Interfaces;
using Slim.Attributes;

namespace Tests.Models
{
    [Name("employees")]
    public interface employeesDb : IDatabaseModel
    {
        DbRead<current_dept_emp> current_dept_emp { get; }
        DbRead<departments> departments { get; }
        DbRead<dept_emp> dept_emp { get; }
        DbRead<dept_emp_latest_date> dept_emp_latest_date { get; }
        DbRead<dept_manager> dept_manager { get; }
        DbRead<employees> employees { get; }
        DbRead<salaries> salaries { get; }
        DbRead<titles> titles { get; }
    }
}