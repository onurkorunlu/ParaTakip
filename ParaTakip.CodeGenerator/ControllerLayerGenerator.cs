namespace ParaTakip.CodeGenerator
{
    public class ControllerLayerGenerator
    {
        DirectoryInfo WorkingDirectory = new DirectoryInfo("C:\\Git\\ParaTakip\\ParaTakip");
        string EntityName { get; set; }
        string ClassSuffix= "Controller.cs";
        string FolderPath;
        string ControllerFilePath;
        string ControllerFileName;
        public ControllerLayerGenerator(string? entityName)
        {
            ArgumentNullException.ThrowIfNull(entityName);
            this.EntityName = entityName;
            var directory = WorkingDirectory.GetDirectories("*.Server");
            if (directory == null || !directory.Any())
            {
                throw new Exception("Entity Folder Not Found");
            }

            FolderPath = directory.First().FullName;
            ControllerFileName = EntityName + ClassSuffix;
            ControllerFilePath = Path.Combine(FolderPath,"Controllers", ControllerFileName);
        }

        public void Generate()
        {
            if (!File.Exists(ControllerFilePath))
            {
                string template = File.ReadAllText("Templates//EntityControllerTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                File.WriteAllText(ControllerFilePath, template);
            }
        }
    }
}
