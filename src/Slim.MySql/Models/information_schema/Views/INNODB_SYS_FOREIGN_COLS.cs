using System;
using Slim;
using Slim.Interfaces;
using Slim.Attributes;

namespace Slim.MySql.Models
{
    [Name("INNODB_SYS_FOREIGN_COLS")]
    public interface INNODB_SYS_FOREIGN_COLS : IViewModel
    {
        [Type("varchar", 193)]
        string FOR_COL_NAME { get; }

        [Type("varchar", 193)]
        string ID { get; }

        [Type("int")]
        int POS { get; }

        [Type("varchar", 193)]
        string REF_COL_NAME { get; }

    }
}