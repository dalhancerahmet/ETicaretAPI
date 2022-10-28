using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Dtos.Configuration;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Action = ETicaretAPI.Application.Dtos.Configuration.Action;

namespace ETicaretAPI.Infrastructure.Services.Configuration
{
    public class AplicationService : IAplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);// controllerdan gelen type vererek Assemblye nesnesi oluşturduk.
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase))); // assembly nesnesinden ControllerBase tipli controller'lara eriştik.

            List<Menu> menus = new();//Menü sınıfından nesne türettik.
            if (controllers != null)// eğer controllers boş değilse
                foreach (var controller in controllers) // controllersları dön
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));// controller içerisindeki AuthorizeDefinitionAttribute ile işaretlenmiş metotlara eriştik.
                    if (actions != null) // actionlar boş değilse
                        foreach (var action in actions) // actions'ları tek tek gez
                        {
                            var attributes = action.GetCustomAttributes(true);// metodun attributes'lerini çektik.
                            if (attributes != null) // attribute boş değilse
                            {
                                Menu menu = null;

                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute; // attributes'ler içerisindeki authorizeDefinitionAttribute ile işaretlenmiş attribute'ü çektik
                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu)) //menu nesnesinin propersinden Name'e çektiğimiz attribute'ün Menüsünü veridik.
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else //değilse menus içerisindeki name i authorizeDefinitionAttribute. Menu ye  eşit olanı verdik.
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                                Action _action = new() // Action nesnesi oluşturduk
                                {
                                    ActionType = Enum.GetName(typeof(ActionType),authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition
                                };

                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;

                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                                menu.Actions.Add(_action);
                            }
                        }
                }


            return menus;
        }
    }
}
