using System;

namespace BH.Framework.Swagger
{
    public class SwaggerDescAttribute : Attribute
    {

        public string Name { get; set; }
        public string Value { get; set; }

        public SwaggerDescAttribute(string value)
        {
            this.Value = value;
        }

        public SwaggerDescAttribute(string name, string value) : this(value)
       {
            this.Name = name;
        }
    }
}