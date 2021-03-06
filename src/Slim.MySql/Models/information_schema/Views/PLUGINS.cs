using System;
using Slim;
using Slim.Interfaces;
using Slim.Attributes;

namespace Slim.MySql.Models
{
    [Name("PLUGINS")]
    public interface PLUGINS : IViewModel
    {
        [Type("varchar", 64)]
        string LOAD_OPTION { get; }

        [Nullable]
        [Type("varchar", 80)]
        string PLUGIN_AUTH_VERSION { get; }

        [Nullable]
        [Type("varchar", 64)]
        string PLUGIN_AUTHOR { get; }

        [Nullable]
        [Type("longtext", 4294967295)]
        string PLUGIN_DESCRIPTION { get; }

        [Nullable]
        [Type("varchar", 64)]
        string PLUGIN_LIBRARY { get; }

        [Nullable]
        [Type("varchar", 20)]
        string PLUGIN_LIBRARY_VERSION { get; }

        [Type("varchar", 80)]
        string PLUGIN_LICENSE { get; }

        [Type("varchar", 12)]
        string PLUGIN_MATURITY { get; }

        [Type("varchar", 64)]
        string PLUGIN_NAME { get; }

        [Type("varchar", 16)]
        string PLUGIN_STATUS { get; }

        [Type("varchar", 80)]
        string PLUGIN_TYPE { get; }

        [Type("varchar", 20)]
        string PLUGIN_TYPE_VERSION { get; }

        [Type("varchar", 20)]
        string PLUGIN_VERSION { get; }

    }
}