using System;
using System.IO;
using System.Text;

namespace Common
{
    /// <summary>
    /// �ļ�������
    /// </summary>
    public class FileHelper
    {
        public static Encoding DefaultEncoding = Encoding.UTF8;

        #region ���ָ��Ŀ¼�Ƿ����
        /// <summary>
        /// ���ָ��Ŀ¼�Ƿ����
        /// </summary>
        /// <param name="directoryPath">Ŀ¼�ľ���·��</param>        
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        #endregion

        #region ���ָ���ļ��Ƿ����
        /// <summary>
        /// ���ָ���ļ��Ƿ����,��������򷵻�true��
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>        
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion

        #region ���ָ��Ŀ¼�Ƿ�Ϊ��
        /// <summary>
        /// ���ָ��Ŀ¼�Ƿ�Ϊ��
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>        
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //�ж��Ƿ�����ļ�
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    return false;
                }

                //�ж��Ƿ�����ļ���
                string[] directoryNames = GetDirectories(directoryPath);
                return directoryNames.Length <= 0;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        #endregion

        #region ���ָ��Ŀ¼���Ƿ����ָ�����ļ�
        /// <summary>
        /// ���ָ��Ŀ¼���Ƿ����ָ�����ļ�,��Ҫ������Ŀ¼��ʹ�����ط���.
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>
        /// <param name="searchPattern">ģʽ�ַ�����"*"����0��N���ַ���"?"����1���ַ���
        /// ������"Log*.xml"��ʾ����������Log��ͷ��Xml�ļ���</param>        
        public static bool Contains(string directoryPath, string searchPattern)
        {
            try
            {
                //��ȡָ�����ļ��б�
                string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

                //�ж�ָ���ļ��Ƿ����
                return fileNames.Length != 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// ���ָ��Ŀ¼���Ƿ����ָ�����ļ�
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>
        /// <param name="searchPattern">ģʽ�ַ�����"*"����0��N���ַ���"?"����1���ַ���
        /// ������"Log*.xml"��ʾ����������Log��ͷ��Xml�ļ���</param> 
        /// <param name="isSearchChild">�Ƿ�������Ŀ¼</param>
        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //��ȡָ�����ļ��б�
                string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

                //�ж�ָ���ļ��Ƿ����
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region ����һ��Ŀ¼
        /// <summary>
        /// ����һ��Ŀ¼
        /// </summary>
        /// <param name="directoryPath">Ŀ¼�ľ���·��</param>
        public static void CreateDirectory(string directoryPath)
        {
            //���Ŀ¼�������򴴽���Ŀ¼
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        #endregion

        #region ����һ���ļ�
        /// <summary>
        /// ����һ���ļ���
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        public static void CreateFile(string filePath)
        {
            //����ļ��������򴴽����ļ�
            if (!IsExistFile(filePath))
            {
                //����һ��FileInfo����
                FileInfo file = new FileInfo(filePath);

                //�����ļ�
                FileStream fs = file.Create();

                //�ر��ļ���
                fs.Close();
            }
        }

        /// <summary>
        /// ����һ���ļ�,�����ֽ���д���ļ���
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        /// <param name="buffer">������������</param>
        public static void CreateFile(string filePath, byte[] buffer)
        {
            //����ļ��������򴴽����ļ�
            if (!IsExistFile(filePath))
            {
                //����һ��FileInfo����
                FileInfo file = new FileInfo(filePath);

                //�����ļ�
                FileStream fs = file.Create();

                //д���������
                fs.Write(buffer, 0, buffer.Length);

                //�ر��ļ���
                fs.Close();
            }
        }
        #endregion

        #region ��ȡ�ı��ļ�������
        /// <summary>
        /// ��ȡ�ı��ļ�������
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>        
        public static int GetLineCount(string filePath)
        {
            //���ı��ļ��ĸ��ж���һ���ַ���������
            string[] rows = File.ReadAllLines(filePath);

            //��������
            return rows.Length;
        }
        #endregion

        #region ��ȡһ���ļ��ĳ���
        /// <summary>
        /// ��ȡһ���ļ��ĳ���,��λΪByte
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>        
        public static string GetFileSize(string filePath)
        {
            //����һ���ļ�����
            FileInfo fi = new FileInfo(filePath);

            //��ȡ�ļ��Ĵ�С
            return fi.Length.ToString();
        }

        /// <summary>
        /// ��ȡһ���ļ��ĳ���,��λΪKB
        /// </summary>
        /// <param name="filePath">�ļ���·��</param>        
        public static string GetFileSizeByKb(string filePath)
        {
            //����һ���ļ�����
            FileInfo fi = new FileInfo(filePath);

            //��ȡ�ļ��Ĵ�С
            return (fi.Length / 1024.00).ToString("0.0");
        }

        /// <summary>
        /// ��ȡһ���ļ��ĳ���,��λΪMB
        /// </summary>
        /// <param name="filePath">�ļ���·��</param>        
        public static string GetFileSizeByMb(string filePath)
        {
            //����һ���ļ�����
            FileInfo fi = new FileInfo(filePath);

            //��ȡ�ļ��Ĵ�С
            return (fi.Length / 1024.00 / 1024.00).ToString("0.0");
        }
        #endregion

        #region ��ȡָ��Ŀ¼�е��ļ��б�
        /// <summary>
        /// ��ȡָ��Ŀ¼�������ļ��б�
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>        
        public static string[] GetFileNames(string directoryPath)
        {
            //���Ŀ¼�����ڣ����׳��쳣
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //��ȡ�ļ��б�
            return Directory.GetFiles(directoryPath);
        }

        /// <summary>
        /// ��ȡָ��Ŀ¼����Ŀ¼�������ļ��б�
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>
        /// <param name="searchPattern">ģʽ�ַ�����"*"����0��N���ַ���"?"����1���ַ���
        /// ������"Log*.xml"��ʾ����������Log��ͷ��Xml�ļ���</param>
        /// <param name="isSearchChild">�Ƿ�������Ŀ¼</param>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //���Ŀ¼�����ڣ����׳��쳣
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            return Directory.GetFiles(directoryPath, searchPattern, isSearchChild ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
        #endregion

        #region ��ȡָ��Ŀ¼�е���Ŀ¼�б�
        /// <summary>
        /// ��ȡָ��Ŀ¼��������Ŀ¼�б�,��Ҫ����Ƕ�׵���Ŀ¼�б�,��ʹ�����ط���.
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>        
        public static string[] GetDirectories(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }

        /// <summary>
        /// ��ȡָ��Ŀ¼����Ŀ¼��������Ŀ¼�б�
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>
        /// <param name="searchPattern">ģʽ�ַ�����"*"����0��N���ַ���"?"����1���ַ���
        /// ������"Log*.xml"��ʾ����������Log��ͷ��Xml�ļ���</param>
        /// <param name="isSearchChild">�Ƿ�������Ŀ¼</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ���ı��ļ�д������
        /// <summary>
        /// ���ı��ļ���д������
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        /// <param name="content">д�������</param>        
        public static void WriteText(string filePath, string content)
        {
            //���ļ�д������
            File.WriteAllText(filePath, content);
        }
        #endregion

        #region ���ı��ļ���β��׷������
        /// <summary>
        /// ���ı��ļ���β��׷������
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        /// <param name="content">д�������</param>
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }
        #endregion

        #region �������ļ������ݸ��Ƶ����ļ���
        /// <summary>
        /// ��Դ�ļ������ݸ��Ƶ�Ŀ���ļ���
        /// </summary>
        /// <param name="sourceFilePath">Դ�ļ��ľ���·��</param>
        /// <param name="destFilePath">Ŀ���ļ��ľ���·��</param>
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }
        #endregion

        #region ���ļ��ƶ���ָ��Ŀ¼
        /// <summary>
        /// ���ļ��ƶ���ָ��Ŀ¼
        /// </summary>
        /// <param name="sourceFilePath">��Ҫ�ƶ���Դ�ļ��ľ���·��</param>
        /// <param name="descDirectoryPath">�ƶ�����Ŀ¼�ľ���·��</param>
        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            //��ȡԴ�ļ�������
            string sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                //���Ŀ���д���ͬ���ļ�,��ɾ��
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }
                //���ļ��ƶ���ָ��Ŀ¼
                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }
        #endregion

        #region ������ȡ����������
        /// <summary>
        /// ������ȡ����������
        /// </summary>
        /// <param name="stream">ԭʼ��</param>
        public static byte[] StreamToBytes(Stream stream)
        {
            try
            {
                //����������
                byte[] buffer = new byte[stream.Length];

                //��ȡ��
                stream.Read(buffer, 0, int.Parse(stream.Length.ToString()));

                //������
                return buffer;
            }
            finally
            {
                //�ر���
                stream.Close();
            }
        }
        #endregion

        #region ���ļ���ȡ����������
        /// <summary>
        /// ���ļ���ȡ����������
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        public static byte[] FileToBytes(string filePath)
        {
            //��ȡ�ļ��Ĵ�С 
            int fileSize = int.Parse(GetFileSize(filePath));

            //����һ����ʱ������
            byte[] buffer = new byte[fileSize];

            //����һ���ļ���
            FileInfo fi = new FileInfo(filePath);
            FileStream fs = fi.Open(FileMode.Open);

            try
            {
                //���ļ������뻺����
                fs.Read(buffer, 0, fileSize);

                return buffer;
            }
            finally
            {
                //�ر��ļ���
                fs.Close();
            }
        }
        #endregion

        #region ���ļ���ȡ���ַ�����
        /// <summary>
        /// ���ļ���ȡ���ַ�����
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        public static string FileToString(string filePath)
        {
            return FileToString(filePath, DefaultEncoding);
        }
        /// <summary>
        /// ���ļ���ȡ���ַ�����
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        /// <param name="encoding">�ַ�����</param>
        public static string FileToString(string filePath, Encoding encoding)
        {
            //��������ȡ��
            StreamReader reader = new StreamReader(filePath, encoding);
            try
            {
                //��ȡ��
                return reader.ReadToEnd();
            }
            finally
            {
                //�ر�����ȡ��
                reader.Close();
            }
        }
        #endregion

        #region ���ļ��ľ���·���л�ȡ�ļ���( ������չ�� )
        /// <summary>
        /// ���ļ��ľ���·���л�ȡ�ļ���( ������չ�� )
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>        
        public static string GetFileName(string filePath)
        {
            //��ȡ�ļ�������
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }
        #endregion

        #region ���ļ��ľ���·���л�ȡ�ļ���( ��������չ�� )
        /// <summary>
        /// ���ļ��ľ���·���л�ȡ�ļ���( ��������չ�� )
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>        
        public static string GetFileNameNoExtension(string filePath)
        {
            //��ȡ�ļ�������
            FileInfo fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }
        #endregion

        #region ���ļ��ľ���·���л�ȡ��չ��
        /// <summary>
        /// ���ļ��ľ���·���л�ȡ��չ��
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>        
        public static string GetExtension(string filePath)
        {
            //��ȡ�ļ�������
            FileInfo fi = new FileInfo(filePath);
            return fi.Extension;
        }
        #endregion

        #region ���ָ��Ŀ¼
        /// <summary>
        /// ���ָ��Ŀ¼�������ļ�����Ŀ¼,����Ŀ¼��Ȼ����.
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>
        public static void ClearDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                //ɾ��Ŀ¼�����е��ļ�
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    DeleteFile(fileNames[i]);
                }

                //ɾ��Ŀ¼�����е���Ŀ¼
                string[] directoryNames = GetDirectories(directoryPath);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    DeleteDirectory(directoryNames[i]);
                }
            }
        }
        #endregion

        #region ����ļ�����
        /// <summary>
        /// ����ļ�����
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        public static void ClearFile(string filePath)
        {
            //ɾ���ļ�
            File.Delete(filePath);

            //���´������ļ�
            CreateFile(filePath);
        }
        #endregion

        #region ɾ��ָ���ļ�
        /// <summary>
        /// ɾ��ָ���ļ�
        /// </summary>
        /// <param name="filePath">�ļ��ľ���·��</param>
        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion

        #region ɾ��ָ��Ŀ¼
        /// <summary>
        /// ɾ��ָ��Ŀ¼����������Ŀ¼
        /// </summary>
        /// <param name="directoryPath">ָ��Ŀ¼�ľ���·��</param>
        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
        public static void FileSaveAs(string sourceFile, string destFile)
        {
            FileInfo info = new FileInfo(sourceFile);
            info.CopyTo(destFile, true);
        }
        #endregion

        #region �����ļ���ָ��·��
        /// <summary>
        /// �����ļ���ָ��·��
        /// </summary>
        /// <param name="fileName"></param>
        public static void SaveFile(string fileName)
        {
            if (IsExistFile(fileName))
            {
                DeleteFile(fileName);
            }
            FileInfo info = new FileInfo(fileName);
            FileStream stream = info.Create();
            stream.Close();
        }
        #endregion
    }
}
