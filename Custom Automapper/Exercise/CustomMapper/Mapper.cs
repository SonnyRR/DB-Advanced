namespace CustomMapper
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Reflection;

    public class Mapper
    {
        public T Map<T>(object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source can not be null");
            }

            T dest = (T)Activator.CreateInstance(typeof(T));

            return DoMapping<T>(source, dest);
        }

        private T DoMapping<T>(object source, T dest)
        {
            var properties = dest
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToArray();

            var srcProperties = source
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToArray();


            foreach (var property in properties)
            {
                var srcProperty = srcProperties.
                    Where(p => p.Name == property.Name)
                    .FirstOrDefault();

                if (srcProperty == null)
                {
                    continue;
                }

                var sourceValue = srcProperty
                    .GetMethod
                    .Invoke(source, new object[0]);

                if (sourceValue is null)
                    continue;

                if (ReflectionUtils.IsPrimitive(sourceValue.GetType()))
                {
                    property.SetValue(dest, srcProperty.GetValue(source));

                    continue;
                }

                if (ReflectionUtils.IsGenericCollection(sourceValue.GetType()))
                {
                    if (ReflectionUtils.IsPrimitive(sourceValue.GetType().GetGenericArguments()[0]))
                    {
                        var destinationCollection = sourceValue;
                        property.SetMethod.Invoke(dest, new[] { destinationCollection });
                    }

                    else
                    {
                        var destCollection = property.GetMethod.Invoke(dest, new object[0]);
                        var destType = destCollection.GetType().GetGenericArguments()[0];

                        foreach (var destP in (IEnumerable)sourceValue)
                        {
                            var destInstance = Activator.CreateInstance(destType);
                            ((IList)destCollection).Add(this.DoMapping(destP, destInstance));
                        }
                    }
                }

                else if (ReflectionUtils.IsNonGenericCollection(sourceValue.GetType()))
                {
                    var destCollection = (IList)Activator.CreateInstance(property.PropertyType,
                        new object[] { ((object[])sourceValue).Length });

                    for (int i = 0; i < ((object[])sourceValue).Length; i++)
                    {
                        destCollection[i] = this.DoMapping(((object[])sourceValue)[i],
                            property.PropertyType.GetElementType());
                    }

                    property.SetValue(dest, destCollection);
                }

                else
                {
                    // FIXME
                    //var propertyInstance = Activator.CreateInstance(srcProperty.GetValue(source).GetType());

                    var propertyType = property.DeclaringType;
                    var name = propertyType.Name;
                                      
                    var propertyInstance = Activator.CreateInstance(propertyType);

                    property.SetValue(dest,
                        this.DoMapping(srcProperty.GetValue(source), propertyInstance));
                }
            }

            return dest;
        }
    }

}
