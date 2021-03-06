using System;
using Slim;
using Slim.Interfaces;
using Slim.Attributes;

namespace Slim.MySql.Models
{
    [Name("COLUMN_PRIVILEGES")]
    public interface COLUMN_PRIVILEGES : IViewModel
    {
        [Type("varchar", 64)]
        string COLUMN_NAME { get; }

        [Type("varchar", 190)]
        string GRANTEE { get; }

        [Type("varchar", 3)]
        string IS_GRANTABLE { get; }

        [Type("varchar", 64)]
        string PRIVILEGE_TYPE { get; }

        [Type("varchar", 512)]
        string TABLE_CATALOG { get; }

        [Type("varchar", 64)]
        string TABLE_NAME { get; }

        [Type("varchar", 64)]
        string TABLE_SCHEMA { get; }

    }
}