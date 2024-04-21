namespace ParaTakip.CodeGenerator
{
    public class EntityLayerGenerator
    {
        DataAccessLayerGenerator DataAccessGenerator;
        BusinessLayerGenerator BusinessLayerGenerator;
        ControllerLayerGenerator ControllerLayerGenerator;

        DirectoryInfo WorkingDirectory = new DirectoryInfo("C:\\Git\\ParaTakip");
        string EntityName { get; set; }
        string ClassSuffix = ".cs";
        string FolderPath;
        string FilePath;
        string FileName;

        public EntityLayerGenerator(string? entityName)
        {
            ArgumentNullException.ThrowIfNull(entityName);
            DataAccessGenerator = new DataAccessLayerGenerator(entityName);
            BusinessLayerGenerator = new BusinessLayerGenerator(entityName);
            ControllerLayerGenerator = new ControllerLayerGenerator(entityName);

            this.EntityName = entityName;
            var directory = WorkingDirectory.GetDirectories("*.Entities");
            if (directory == null || !directory.Any())
            {
                throw new Exception("Entity Folder Not Found");
            }

            FileName = EntityName + ClassSuffix;
            FolderPath = directory.First().FullName;
            FilePath = Path.Combine(FolderPath, FileName);
        }

        public void Generate()
        {
            GenerateEntityLayer();
            DataAccessGenerator.Generate();
            BusinessLayerGenerator.Generate();
            ControllerLayerGenerator.Generate();
            new ModelLayerGenerator("Add"+EntityName, "RequestModel").Generate();
            new ModelLayerGenerator("Update"+EntityName, "RequestModel").Generate();
            AddRegisterationLine();
        }

        private void GenerateEntityLayer()
        {
            if (!File.Exists(FilePath))
            {
                string template = File.ReadAllText("Templates//EntityClassTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                File.WriteAllText(FilePath, template);
            }
        }

        private void AddRegisterationLine()
        {
            string registerDataAccessLine = $"AppServiceProvider.Instance.Register(typeof(I{EntityName}DataAccess), new {EntityName}DataAccess());\n \t\t\t//@RegisterDataAccessPointer";
            string registerBusinessLine = $"AppServiceProvider.Instance.Register(typeof(I{EntityName}Service), new {EntityName}Service());\n \t\t\t//@RegisterBusinessPointer";

            string searchRegisterDataAccessLine = $"AppServiceProvider.Instance.Register(typeof(I{EntityName}DataAccess), new {EntityName}DataAccess());";
            string searchRegisterBusinessLine = $"AppServiceProvider.Instance.Register(typeof(I{EntityName}Service), new {EntityName}Service());";

            var directory = WorkingDirectory.GetDirectories("*.Configuration").FirstOrDefault();
            if (directory == null)
            {
                return;
            }

            string configurationFilePath = Path.Combine(directory.FullName , "Configurations.cs");
            var configurationFile = File.ReadAllText(configurationFilePath);

            if (!configurationFile.Contains(searchRegisterDataAccessLine))
            {
                configurationFile = configurationFile.Replace("//@RegisterDataAccessPointer" , registerDataAccessLine);
            }

            if (!configurationFile.Contains(searchRegisterBusinessLine))
            {
                configurationFile = configurationFile.Replace("//@RegisterBusinessPointer", registerBusinessLine);
            }

            File.WriteAllText (configurationFilePath, configurationFile);
        }
    }
}
