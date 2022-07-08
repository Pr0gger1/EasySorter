using System;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;

namespace EasySorter
{
    internal class RegistryWorker
    {
        static RegistryKey RegistryPath = Registry.ClassesRoot;

        private const string Registry_RootPath = "Directory\\Background\\shell\\EasySorter";
        private const string Registry_SubMenuPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell";

        //получение имени исполняемого файла
        private static readonly string ModulePath = GetExecuteFileName() + " -a";

        private static string GetExecuteFileName()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        public static void Activate()
        {
            RegistryKey MenuCatalog = RegistryPath.CreateSubKey(Registry_RootPath); // creating folder for context menu

            MenuCatalog.SetValue("MUIVerb", "Сортировать файлы");
            MenuCatalog.SetValue("Icon", GetExecuteFileName());

            MenuCatalog.CreateSubKey("command", true).SetValue(string.Empty, ModulePath); // writing link to execution program

            RegistryPath.Close();
            MenuCatalog.Close();

        }
        public static void Deactivate()
        {
            try
            {
                RegistryPath.DeleteSubKeyTree(Registry_RootPath);
                if (RegistryPath.OpenSubKey("Directory\\Background\\shell\\SortMenu") != null)
                {
                    RegistryPath.DeleteSubKeyTree("Directory\\Background\\shell\\SortMenu");
                }

                RegistryPath.Close();
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Пути до раздела кнопки в реестре не существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static void CreateSubMenu(string titleItem, string menuName)
        {
            try
            {
                RegistryPath = Registry.LocalMachine;

                RegistryKey RootCatalog = RegistryPath.CreateSubKey(Registry_SubMenuPath);
                RegistryKey MenuCatalog = RootCatalog.CreateSubKey(menuName, true);

                MenuCatalog.SetValue(string.Empty, menuName);
                MenuCatalog.SetValue("Icon", "explorer.exe");
                MenuCatalog.CreateSubKey("command", true).SetValue(string.Empty, "explorer.exe");

                RootCatalog.Close();

                RegistryPath = Registry.ClassesRoot;
                MenuCatalog = RegistryPath.OpenSubKey("Directory\\Background\\shell", true);
                RegistryKey SubMenuCatalog = MenuCatalog.CreateSubKey("SortMenu");

                SubMenuCatalog.SetValue("MUIVerb", "Меню UniversalTools");
                SubMenuCatalog.SetValue("SubCommands", $"{menuName};");

                RegistryPath.Close();
                MenuCatalog.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        public static bool IsRegExist()
        {
            // Checking, if folder exist
            RegistryPath = Registry.ClassesRoot;
            return true && RegistryPath.OpenSubKey(Registry_RootPath) != null;

        }
    }
}
