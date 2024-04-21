using ParaTakip.CodeGenerator;

DirectoryInfo WorkingDirectory = new DirectoryInfo("C:\\Git\\ParaTakip");
var directory = WorkingDirectory.GetDirectories("*.Entities");
Dictionary<int, string> entities = new Dictionary<int, string>();
if (directory.Length > 0)
{
    var files = directory.First().GetFiles().OrderBy(x => x.Name).ToList();
    foreach (var file in files)
    {
        if (file.Extension == ".cs")
        {
            entities.Add(files.IndexOf(file) + 1, Path.GetFileNameWithoutExtension(file.FullName));
        }
    }
}

Console.WriteLine("Hello!");
Console.WriteLine("Select Entity :");
Console.WriteLine();
foreach (var entity in entities)
{
    Console.WriteLine(entity.Key + " - " + entity.Value);
}
Console.WriteLine();
Console.WriteLine("-----------");
Console.WriteLine();

int.TryParse(Console.ReadLine(), out int entityKey);
while (entityKey > 0)
{
    string entityName = entities[entityKey];
    new EntityLayerGenerator(entityName).Generate();
    Console.WriteLine();
    Console.WriteLine("Generate Completed ...");
    Console.WriteLine();
    Console.WriteLine("-----------");
    Console.WriteLine();
    entityName = Console.ReadLine();
    entityKey = 0;
}
