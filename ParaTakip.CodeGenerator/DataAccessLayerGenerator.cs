namespace ParaTakip.CodeGenerator
{
    public class DataAccessLayerGenerator
    {
        DirectoryInfo WorkingDirectory = new DirectoryInfo("C:\\Git\\ParaTakip");
        string EntityName { get; set; }
        string ClassSuffix="DataAccess.cs";
        string FolderPath;
        string ServiceFilePath;
        string ServiceFileName;
        string InterfaceFilePath;
        string InterfaceFileName;

        public DataAccessLayerGenerator(string? entityName)
        {
            ArgumentNullException.ThrowIfNull(entityName);
            this.EntityName = entityName;
            var directory = WorkingDirectory.GetDirectories("*.DataAccess");
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
                string template = File.ReadAllText("Templates//DataAccessServiceClassTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                File.WriteAllText(ServiceFilePath, template);
            }

            if (!File.Exists(InterfaceFilePath))
            {
                string template = File.ReadAllText("Templates//DataAccessInterfaceClassTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                File.WriteAllText(InterfaceFilePath, template);
            }
        }
    }
}
