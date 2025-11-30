using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Text.Json;

namespace ScmDesignli.Api.Filters
{
    /// <summary>
    /// Schema filter to display enum values with their descriptions in Swagger
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                // Clear the default enum values
                schema.Enum = null;
                
                // Build a list of objects with value and description
                var enumValues = new List<object>();

                foreach (var value in Enum.GetValues(context.Type))
                {
                    var enumMember = context.Type.GetMember(value.ToString()!).FirstOrDefault();
                    var descriptionAttribute = enumMember?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .OfType<DescriptionAttribute>()
                        .FirstOrDefault();

                    var enumValue = (int)value;
                    var description = descriptionAttribute?.Description ?? value.ToString();
                    
                    enumValues.Add(new { value = enumValue, description });
                }

                // Convert to formatted JSON string
                var jsonOptions = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                var jsonString = JsonSerializer.Serialize(enumValues, jsonOptions);
                schema.Description = $"Available values:\n```json\n{jsonString}\n```";
            }
        }
    }
}
