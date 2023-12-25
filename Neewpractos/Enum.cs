using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog10
{
    internal class Enum
    {
        internal enum button
        {
            [Description("Create a new entry")]
            Create = -8,
            [Description("Add to an existing entry")]
            Plus,
            [Description("Subtract from an existing entry")]
            Minus,
            [Description("Go back to the previous menu")]
            Back,
            [Description("Update an existing entry")]
            Update,
            [Description("Save the current changes")]
            Save,
            [Description("Search for an existing entry")]
            Search,
            [Description("Delete an existing entry")]
            F10
        }
    }
}