namespace ParaTakip.CodeGenerator
{
    public class ModelLayerGenerator
    {
        DirectoryInfo WorkingDirectory = new DirectoryInfo("C:\\Git\\ParaTakip");
        string EntityName { get; set; }
        string ClassSuffix = ".cs";
        string FolderPath;
        string FilePath;
        string FileName;
        string folder;

        public ModelLayerGenerator(string? entityName, string folder)
        {
            ArgumentNullException.ThrowIfNull(entityName);

            this.EntityName = entityName + folder;
            this.folder = folder;
            var directory = WorkingDirectory.GetDirectories("*.Model");
            if (directory == null || !directory.Any())
            {
                throw new Exception("Entity Folder Not Found");
            }

            FileName = EntityName + ClassSuffix;
            FolderPath = directory.First().FullName;
            FilePath = Path.Combine(FolderPath, folder, FileName);
        }

        public void Generate()
        {
            if (!File.Exists(FilePath))
            {
                string template = File.ReadAllText("Templates//ModelClassTemplate.txt");
                template = template.Replace("@EntityName", EntityName);
                template = template.Replace("@Folder", folder);
                File.WriteAllText(FilePath, template);
            }
        }
    }
}
