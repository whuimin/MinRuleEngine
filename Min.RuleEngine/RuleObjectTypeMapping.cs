using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Min.RuleEngine
{
    internal class RuleObjectTypeMapping
    {
        private readonly Dictionary<string, Type> defaultTypes = new Dictionary<string, Type>() {
            { "bool", typeof(bool) },
            { "Boolean", typeof(bool) },
            { "short", typeof(short) },
            { "Int16", typeof(short) },
            { "int", typeof(int) },
            { "Int32", typeof(int) },
            { "long", typeof(long) },
            { "Int64", typeof(long) },
            { "float", typeof(float) },
            { "Single", typeof(float) },
            { "double", typeof(double) },
            { "Double", typeof(double) },
            { "decimal", typeof(decimal) },
            { "Decimal", typeof(decimal) },
            { "string", typeof(string) },
            { "String", typeof(string) },
            { "HashSet<string>", typeof(HashSet<string>) },
            { "HashSet<String>", typeof(HashSet<string>) }
        };

        //变量/参数名。
        private readonly Stack<string> names = new Stack<string>();

        //参数表达式。
        private readonly ParameterExpression[] parameterExpressions;

        //参数名索引。
        private readonly Dictionary<string, int> parameterNameIndexes = new Dictionary<string, int>();

        //类型名<->类型关系。
        private readonly Dictionary<string, Type> parameterTypes = new Dictionary<string, Type>();

        //局部变量名<->类型关系。
        private readonly Dictionary<string, ParameterExpression> localParameterExpressions = new Dictionary<string, ParameterExpression>();

        public RuleObjectTypeMapping(IList<Type> parameterTypes)
        {
            parameterExpressions = new ParameterExpression[parameterTypes.Count - 1];

            var index = 0;
            foreach (var type in parameterTypes)
            {
                if (index >= parameterTypes.Count - 1)
                {
                    break;
                }
                this.parameterTypes.TryAdd(type.Name, type);

                var name = ParseDefaultName(type, index);

                parameterNameIndexes.Add(name, index);
                var expression = Expression.Parameter(type, name);
                parameterExpressions[index] = expression;

                index++;
            }

            ReturnType = parameterTypes.Last();
        }

        private string ParseDefaultName(Type type, int index)
        {
            var name = type.Name;
            if (defaultTypes.ContainsKey(name))
            {
                switch (name)
                {
                    case "Boolean":
                        return "bool" + index;
                    case "Int16":
                        return "short" + index;
                    case "Int32":
                        return "int" + index;
                    case "Int64":
                        return "long" + index;
                    case "String":
                        return "string" + index;
                    case "Single":
                        return "float" + index;
                    case "Double":
                        return "double" + index;
                    case "Decimal":
                        return "decimal" + index;
                    case "HashSet<string>":
                    case "HashSet<String>":
                        return "stringHashSet" + index;
                    default:
                        return name.Substring(0, 1).ToLower() + name.Substring(1) + index;
                }
            }

            return name.Substring(0, 1).ToLower() + name.Substring(1) + index;
        }

        public void PushName(string name)
        {
            names.Push(name);
        }

        public string PopName()
        {
            return names.Pop();
        }

        public Type GetType(string typeName)
        {
            return parameterTypes.GetValueOrDefault(typeName) ?? defaultTypes.GetValueOrDefault(typeName);
        }

        public Type ReturnType { get; }

        public ParameterExpression GetParameterExpression(string variableName)
        {
            if (parameterNameIndexes.TryGetValue(variableName, out int index))
            {
                return parameterExpressions[index];
            }

            return localParameterExpressions.GetValueOrDefault(variableName);
        }

        public IEnumerable<ParameterExpression> ParameterExpressions
        {
            get
            {
                return parameterExpressions.Select(pe => pe);
            }
        }

        public void AddParameterExpression(string variableName, ParameterExpression parameterExpression)
        {
            if (parameterNameIndexes.TryGetValue(variableName, out int index))
            {
                parameterExpressions[index] = parameterExpression;
            }
            else
            {
                var index1 = 0;
                foreach (var parameterExpression1 in parameterExpressions)
                {
                    if (parameterExpression1.Type == parameterExpression.Type)
                    {
                        break;
                    }
                    index1++;
                }

                var parameterExpression2 = parameterExpressions[index];
                parameterNameIndexes.Remove(parameterExpression2.Name);
                parameterNameIndexes.Add(parameterExpression.Name, index1);

                parameterExpressions[index1] = parameterExpression;
            }
        }

        public IEnumerable<ParameterExpression> LocalParameterExpressions
        {
            get
            {
                return localParameterExpressions.Select(lpe => lpe.Value);
            }
        }

        public void AddLocalParameterExpression(string variableName, ParameterExpression parameterExpression)
        {
            localParameterExpressions.TryAdd(variableName, parameterExpression);
        }
    }
}
