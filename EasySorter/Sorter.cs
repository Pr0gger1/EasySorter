using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace EasySorter
{
    internal class Sorter
    {
        private static readonly Dictionary<string, string[]> file_category = new Dictionary<string, string[]> {
            {
                "Фотографии", new string[]{ ".png", ".jpg"} 
            },
            {
                "Текстовые файлы", new string[]{ ".txt" }
            },
            {
                "Видео", new string[]{ ".mp4", ".avi",  ".mkv", ".mpeg", ".mpg", ".mov", ".3g2", ".3gp", ".webm"} 
            },
            {
                "Документы", new string[]{ ".pdf", ".epub", ".doc", ".docx", ".docm" } 
            },
            {
                "Архивы", new string[]{ ".zip", ".rar", ".7z", ".tar", ".arj", ".iso" } 
            },
            {
                "Аудио", new string[]{ ".mp3", ".ogg", ".wav", ".flac", ".aac" } 
            },
            {
                "Иконки", new string[]{ ".ico" } 
            },
            {
                "Шрифты", new string[]{ ".otf", ".ttf", ".fon", ".fnt" } 
            },
            {
                "Исполняемыe файлы", new string[]{ ".exe", ".msi" } 
            },
            {
                "Системные файлы", new string[]{ ".sys", ".reg" }
            },
            {
                "Презентации", new string[]{ ".pptx", ".ppt" } 
            },
            {
                "Excel-таблицы", new string[]{ ".xlsx", ".xls" } 
            },
            {
                "Торренты", new string[]{ ".torrent" }
            },
            {
                "Гипертекстовые файлы", new string[]{ ".html", ".htm" } 
            },
            {
                "JS", new string[]{ ".js" }
            },
            {
                "Python", new string[]{ ".py" }
            },
            {
                "PHP", new string[]{ ".php" }
            }
        };

        private static readonly string root_dir = Directory.GetCurrentDirectory(); // getting the current path

        public void Run()
        {
            Create_category();
            Sort_files();
            Delete_empty_dir();
            Application.Current.Shutdown();
        }

        private string[] Get_files()
        {
            return Directory.GetFiles(root_dir);
        }
        private void Create_category()
        {
            foreach (KeyValuePair<string, string[]> type_content in file_category)
            {
                Directory.CreateDirectory(type_content.Key);
            }
        }
        private void Delete_empty_dir()
        {
            foreach (KeyValuePair<string, string[]> type_content in file_category)
            {
                if (Directory.Exists($"{root_dir}\\{type_content.Key}"))
                {
                    try
                    {
                        var files = Directory.GetFiles($"{root_dir}\\{type_content.Key}");
                        if (files.Length == 0)
                        {
                            Directory.Delete($"{root_dir}\\{type_content.Key}");
                        }
                    }
                    catch{ continue; }
                    
                }
            }
        }
        private void Sort_files()
        {
            string Executable_file = Process.GetCurrentProcess().MainModule.FileName;
            string[] files = Get_files();
            foreach (string file in files)
            {
                string current_fileExtension = Path.GetExtension(file);
                foreach (KeyValuePair<string, string[]> cat_dict in file_category)
                {
                    if (cat_dict.Value.Contains(current_fileExtension) && (file != Executable_file))
                    {
                        try
                        {
                            File.Move(file, $"{root_dir}\\{cat_dict.Key}\\{Path.GetFileName(file)}");
                        }
                        catch{ continue; }
                    }
                }
            }
        }
    }
}
