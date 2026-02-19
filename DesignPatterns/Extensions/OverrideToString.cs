using System;
using System.Reflection;
using System.Text;

public static class OverrideToStringExtensionMethods
{

    public static string OverrideToString(this object obj) {
        //get fields
        var fields = obj.GetType().GetFields(System.Reflection.BindingFlags.Instance |
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Public)
                    .Where(f => !f.Name.Contains("k__BackingField"));
        var parts = fields.Select(f => { //$"({f.FieldType.Name}) {f.Name} = {f.GetValue(obj)}"
            //var type = f.FieldType;
            //var typeName = GetTypeName(type);
            //var value = f.GetValue(obj); 
            //return $"({typeName}) {f.Name} = {value}";
            return GetTypeName(obj, f);

        });

        //get properties
        var props = obj.GetType().GetProperties();
        parts = parts.Concat(props.Select(p => { //$"({p.PropertyType.Name}) {p.Name} = {p.GetValue(obj)}")
            //var type = p.PropertyType;
            //var typeName = GetTypeName(type);
            //var value = p.GetValue(obj);
            //return $"({typeName}) {p.Name} = {value}";
            return GetTypeName(obj, p);
        }));

        return string.Join(", ", parts);
    }

    private static string GetTypeName(object obj, MemberInfo member) {
        var type = member switch {
            FieldInfo f => f.FieldType,
            PropertyInfo p => p.PropertyType,
            _ => throw new NotSupportedException()
        };

        var typeName = TypeKeywords.TryGetValue(type, out var keyword)
            ? keyword
              : type.Name;

        if (type.IsClass && type != typeof(string))
            typeName = $"class:{type.Name}";
        //if (type.IsValueType)
        //    typeName = $"struct:{type.Name}";
        if (type.IsArray)
            typeName = $"array:{type.GetElementType()!.Name}";


        var value = member switch {
            FieldInfo f => f.GetValue(obj),
            PropertyInfo p => p.GetValue(obj),
            _ => throw new NotSupportedException()
        }; 

        return $"({typeName}) {member.Name} = {value}"; ;
    }

    //private static string GetTypeName(Type? type) {
    //    var typeName = TypeKeywords.TryGetValue(type, out var keyword)
    //            ? keyword
    //            : type.Name;

    //    if (type.IsClass && type != typeof(string))
    //        typeName = $"class:{type.Name}";
    //    return typeName;
    //}


    private static readonly Dictionary<Type, string> TypeKeywords = new() {
        { typeof(int), "int" },
        { typeof(string), "string" },
        { typeof(bool), "bool" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(decimal), "decimal" },
        { typeof(char), "char" },
        { typeof(byte), "byte" },
        { typeof(long), "long" },
        { typeof(short), "short" },
        { typeof(object), "object" }
    };

}
