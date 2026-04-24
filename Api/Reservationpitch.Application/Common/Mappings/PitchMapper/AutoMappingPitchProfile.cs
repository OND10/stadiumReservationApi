using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Common.Mappings.PitchMapper
{
    public class AutoMappingPitchProfile : Profile
    {
        public AutoMappingPitchProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMap<>);
            var mappingMethodName = nameof(IMap<object>.Mapping);
            bool hasInterface(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == mapFromType;
            var types = assembly.GetExportedTypes().Where(t=>t.GetInterfaces().Any(hasInterface)).ToList();
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(mappingMethodName);
                if(methodInfo != null )
                {
                    methodInfo.Invoke(instance, new object[] {this});
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(hasInterface).ToList();
                    if( interfaces.Count > 0) { 
                        foreach(var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);
                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    
                    }
                }
            }
        }
    }
}
