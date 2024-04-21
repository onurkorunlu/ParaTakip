namespace ParaTakip.CodeGenerator
{
    public class BusinessLayerGenerator
    {
        DirectoryInfo WorkingDirectory = new DirectoryInfo("C:\\Git\\ParaTakip");
        string EntityName { get; set; }
        string ClassSuffix= "Service.cs";
        string FolderPath;
        string ServiceFilePath;
        string ServiceFileName;
        string InterfaceFilePath;
        string InterfaceFileName;
        public BusinessLayerGenerator(string? entityName)
        {
            ArgumentNullException.ThrowIfNull(entityName);
            this.EntityName = entityName;
            var directory = WorkingDirectory.GetDirectories("*.Business");
            if (directory == null || !directory.Any())
            {
                throw new Exception("Entity Folder Not Found");
            }

            FolderPath = directory.First().FullName;
            ServiceFileName = EntityName + ClassSuffix;
            ServiceFilePath = Path.Combine(FolderPath,"Services", ServiceFileName);

            InterfaceFileName = "I"+ EntityName + ClassSuffix;
            InterfaceFilePath = Path.Combine(FolderPath,"Interfaces", InterfaceFileName);
        }

        public void Generate()
        {
            if (!File.Exists(ServiceFilePath))
            {
                string template = File.ReadAllText("Templates//BusinessServiceClassTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                File.WriteAllText(ServiceFilePath, template);
            }

            if (!File.Exists(InterfaceFilePath))
            {
                string template = File.ReadAllText("Templates//BusinessInterfaceClassTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                File.WriteAllText(InterfaceFilePath, template);
            }
        }
    }
}
