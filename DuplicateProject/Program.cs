using System;
using System.IO;
using DuplicateProject.Helpers;

namespace DuplicateProject
{
    class Program
    {
        public const string SEPATATOR = "/";
        public const string BaseProject = "Xamarin";
        public const string NameSpaceByDefault = "Cognizant";

        public const string _pathBaseProject = "/users/758792/Projects";

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the new Project name:");
            string newProjectName = Console.ReadLine();
            while (string.IsNullOrEmpty(newProjectName))
            {
                newProjectName = Console.ReadLine();
            }

            newProjectName = newProjectName.FirstCharToUpper();

            Console.WriteLine($"The new project is:{newProjectName}");

            CleanHelper.CleanProject($"{_pathBaseProject}/{BaseProject}");

            Console.WriteLine($"Project clear");

            DirectoryHelper.DirectoryCopy($"{_pathBaseProject}/{BaseProject}", $"{_pathBaseProject}/{newProjectName}", true);
            Console.WriteLine($"New project created");

            RenameFilesAndDirectories($"{_pathBaseProject}/{newProjectName}", newProjectName);
            Console.WriteLine($"Files and directories renamed");
        }

        static void RenameFilesAndDirectories(string directory, string nameNewProject)
        {
            try
            {
                var files = Directory.GetFiles(directory);
                var directories = Directory.GetDirectories(directory);

                //Files
                foreach (var file in files)
                {
                    FileHelper.ReplaceContent(file, NameSpaceByDefault, nameNewProject);
                    FileHelper.RenameFile(file, NameSpaceByDefault, nameNewProject);
                }

                //Directories
                foreach (var d in directories)
                {
                    RenameFilesAndDirectories(d, nameNewProject);
                    DirectoryHelper.RenameDirectory(d, NameSpaceByDefault, nameNewProject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(DuplicateProject)} error:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
