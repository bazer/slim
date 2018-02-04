using System;
using Slim.Interfaces;
using Slim.Attributes;

namespace Slim.Models
{
    public interface INNODB_CMP : IViewModel
    {
        [Type("int")]
        int compress_ops { get; }

        [Type("int")]
        int compress_ops_ok { get; }

        [Type("int")]
        int compress_time { get; }

        [Type("int")]
        int page_size { get; }

        [Type("int")]
        int uncompress_ops { get; }

        [Type("int")]
        int uncompress_time { get; }

    }
}