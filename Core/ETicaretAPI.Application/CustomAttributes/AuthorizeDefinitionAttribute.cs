using ETicaretAPI.Application.Dtos.Configuration;
using ETicaretAPI.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute : Attribute
    {
        public string Menu { get; set; }
        public string Definition { get; set; } 
        public ActionType ActionType { get; set; }

        public AuthorizeDefinitionAttribute(string menu, string definition, ActionType actionType)
        {
            Menu = menu;
            Definition = definition;
            ActionType = actionType;
        }
    }
    
}
