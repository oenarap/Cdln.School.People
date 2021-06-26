using Apps.Communication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdln.School.People.Uwp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Handles : Attribute
    {
        ICommand[] Commands { get; }

        public Handles(params ICommand[] commands)
        {
            Commands = commands;
        }
    }
}
