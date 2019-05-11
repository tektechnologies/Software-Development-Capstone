﻿/// <summary>
/// Wes Richardson
/// Created: 2019/01/28
/// 
/// An Enmumeration for the Mode of the Room Details UI
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public enum EditMode
    {
        View,
        Edit,
        Add
    }
    public enum CrudFunction
    {
        Create,
        Retrieve,
        Update,
        Deactivate,
        Reactivate,
        Delete,
        Checkout
    }
}
