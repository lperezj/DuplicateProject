using System;
using System.IO;
using DuplicateProject.Helpers;

namespace DuplicateProject
{
    class Program
    {
        public const string SEPATATOR = "/";
        public const string BaseProject = "Cognizant";

        public const string _pathBaseProject = "/users/758792/Projects/Personal";

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
                    FileHelper.ReplaceContent(file, Program.BaseProject, nameNewProject);
                    FileHelper.RenameFile(file, Program.BaseProject, nameNewProject);
                }

                //Directories
                foreach (var d in directories)
                {
                    RenameFilesAndDirectories(d, nameNewProject);
                    DirectoryHelper.RenameDirectory(d, Program.BaseProject, nameNewProject);
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
