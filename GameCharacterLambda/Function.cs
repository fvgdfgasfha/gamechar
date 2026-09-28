using Amazon.Lambda.Core;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GameCharacterLambda;

public class Function
{
    private static readonly Random Random = new();

    public object FunctionHandler(JsonElement input, ILambdaContext context)
    {
        string name = "Unknown";
        string className = "Warrior";

        if (input.TryGetProperty("Name", out JsonElement nameElement))
        {
            name = nameElement.GetString() ?? "Unknown";
        }

        if (input.TryGetProperty("ClassName", out JsonElement classElement))
        {
            className = classElement.GetString() ?? "Warrior";
        }

        if (input.TryGetProperty("queryStringParameters", out JsonElement query))
        {
            if (query.TryGetProperty("name", out JsonElement queryName))
            {
                name = queryName.GetString() ?? name;
            }

            if (query.TryGetProperty("className", out JsonElement queryClass))
            {
                className = queryClass.GetString() ?? className;
            }
        }

        string[] weapons =
        {
            "Iron Sword",
            "Magic Staff",
            "Golden Bow",
            "Battle Axe",
            "Dark Dagger"
        };

        int level = Random.Next(1, 11);
        int strength = Random.Next(50, 101);
        int luck = Random.Next(1, 101);

        string weapon = weapons[Random.Next(weapons.Length)];

        return new
        {
            name = name,
            className = className,
            level = level,
            weapon = weapon,
            strength = strength,
            luck = luck,
            message = $"{name} готов к приключению!"
        };
    }
}