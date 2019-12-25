using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileComparator
{
    public struct FileAtributes 
    {
     public string fileName; 
     public int    fileSize;  
    }


    public partial class Form1 : Form
    {

        List<FileAtributes> FilesA;
        List<FileAtributes> FilesB;
        string filesPathA;
        string filesPathB; 
     // Создаем список файлов. 
     // Нужно создать тип файла
     // определяе различия 

      public Form1()
        {
          InitializeComponent();

            dGvFileList.AllowUserToAddRows = false;
            dGvFileList.AllowUserToDeleteRows = false;
            dGvFileList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dGvFileList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dGvFileList.Columns.Add("Index", "Index");
            dGvFileList.Columns.Add("FileName", "FileName");
            dGvFileList.Columns.Add("Size", "Size");

            dGvFileList.Columns[0].Width = 90;
            dGvFileList.Columns[1].Width = 90;
            dGvFileList.Columns[2].Width = dGvFileList.Width - 180 - dGvFileList.RowHeadersWidth;
            FilesA = new List<FileAtributes>();
            FilesB = new List<FileAtributes>();

        }

        private void button2_Click(object sender, EventArgs e)
        {
          FolderBrowserDialog fbDialogFileA = new FolderBrowserDialog();
            if (fbDialogFileA.ShowDialog() == DialogResult.OK)
            {
                filesPathA = fbDialogFileA.SelectedPath;
                textBox1.Text = filesPathA; 
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
     //       string nameFileA;
     //       string sizeFileA;
            FileAtributes fA = new FileAtributes();
            // запускаем поиск файлов расположенных в текущем каталоге. с учетом подкаталогов. 
            label1.Text = "Индексирование элементов массива А";
            string[] allfilesA = Directory.GetFiles(filesPathA, "*.*", SearchOption.AllDirectories);

            //возникает ошибка при индексировании. 
            // Нужно проводить индексирование файлов в каждома каталоге
            // Добавлять в каталог файл сразу при получении его имени.
            // При ошибке доступа к каталогу выводить соответствующее исключение. 
            // выводить разрешение файла

            for (int indexA = 0; indexA < allfilesA.Length; indexA++)
            {
                fA.fileName = Path.GetFileName(allfilesA[indexA]);
                // добавляем файлы в массив.
                string strEx = Path.GetExtension(allfilesA[indexA]);
               if (strEx != ".cs")
                FilesA.Add(fA);
                progressBar1.Value = (int)((indexA / allfilesA.Length) * 100); 
            }
            label1.Text = "Индексирование элементов массива B";
            string[] allfilesB = Directory.GetFiles(filesPathB, "*.*", SearchOption.AllDirectories);

            for (int indexB = 0; indexB < allfilesB.Length; indexB++)// foreach (string filename in allfilesB)
            {
                fA.fileName = Path.GetFileName(allfilesB[indexB]);
                // добавляем файлы в массив.
                if (Path.GetExtension(allfilesB[indexB]) != ".cs")
                    FilesB.Add(fA);
                progressBar1.Value = (int)((indexB / allfilesB.Length) * 100);
            }

            // Файлы проиндексированы
            // Проводим сравнение файлов. 

            // DirectoryInfo dir = new DirectoryInfo(@"D:\Temp");
            // foreach (var item in dir.GetDirectories())
            // {
            //   Console.WriteLine(item.Name);
            // }
            //Console.ReadLine();

            label1.Text = "Сравнение элементов массива"; 
            int index = 0;
            int matches;// Можно будет попробовать реализовать перебор как в Java. 
            for (int indexB = 0; indexB < FilesB.Count;indexB++)
            {
                matches = 0;
                //при проходе файл накапливает массу. 
                for (int indexA =0; indexA< FilesA.Count;indexA++)
                {
                    if (FilesA[indexA].fileName == FilesB[indexB].fileName)
                    {
                      matches++;
                    }
          
                } 

                if (matches == 0)
                    {
                      index++;
                      dGvFileList.Rows.Add(1);
                      dGvFileList[0, dGvFileList.Rows.Count - 1].Value = index.ToString();
                      dGvFileList[1, dGvFileList.Rows.Count - 1].Value = FilesB[indexB].fileName;
                    }
                progressBar1.Value = (int)((indexB / FilesB.Count) * 100);
            }
            label1.Text = "Вывод результата";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialogFileB = new FolderBrowserDialog();
            if (fbDialogFileB.ShowDialog() == DialogResult.OK)
            {
                filesPathB = fbDialogFileB.SelectedPath;
                textBox2.Text = filesPathB;
            }

        }
    }
}
