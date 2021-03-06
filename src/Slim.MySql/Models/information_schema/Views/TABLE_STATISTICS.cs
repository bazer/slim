using System;
using Slim;
using Slim.Interfaces;
using Slim.Attributes;

namespace Slim.MySql.Models
{
    [Name("TABLE_STATISTICS")]
    public interface TABLE_STATISTICS : IViewModel
    {
        [Type("bigint")]
        long ROWS_CHANGED { get; }

        [Type("bigint")]
        long ROWS_CHANGED_X_INDEXES { get; }

        [Type("bigint")]
        long ROWS_READ { get; }

        [Type("varchar", 192)]
        string TABLE_NAME { get; }

        [Type("varchar", 192)]
        string TABLE_SCHEMA { get; }

    }
}