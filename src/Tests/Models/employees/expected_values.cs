using System;
using Slim.Interfaces;
using Slim.Attributes;

namespace Tests.Models
{
    public interface expected_values : ITableModel
    {
        [PrimaryKey]
        [Type("varchar", 30)]
        string table_name { get; }

        [Type("varchar", 100)]
        string crc_md5 { get; }

        [Type("varchar", 100)]
        string crc_sha { get; }

        [Type("int")]
        int recs { get; }

    }
}