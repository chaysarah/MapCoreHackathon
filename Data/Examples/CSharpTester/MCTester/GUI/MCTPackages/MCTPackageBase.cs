using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security;

namespace MCTester.MCTPackages
{
    [DataContract]
    public class MCTPackageBase
    {
        public override string ToString()
        {
            // JsonReaderWriterFactory.CreateJsonWriter(new StreamWriter)
            var settings = new DataContractJsonSerializerSettings();
           
            var js = new DataContractJsonSerializer(GetType());
            var msObj = new MemoryStream();
            js.WriteObject(msObj, this);
            msObj.Position = 0;
            var sr = new StreamReader(msObj);
            var jsonString = sr.ReadToEnd().Trim() ;

            List<char> newJsonCharArray = new List<char>();
            for (int i = 0; i < jsonString.Length; )
            {
                try
                {
                    if (jsonString[i] == '"')
                    {
                        newJsonCharArray.Add(jsonString[i]);
                        i++;
                        while (jsonString[i] != '"')
                        {
                            newJsonCharArray.Add(jsonString[i]);
                            i++;
                        }
                        newJsonCharArray.Add(jsonString[i]);
                        i++;
                    }
                    else if (jsonString[i] == '{' || jsonString[i] == '}' || jsonString[i] == ',')
                    {
                        newJsonCharArray.Add(jsonString[i]);
                        newJsonCharArray.Add('\n');
                        i++;
                    }
                    else
                    {
                        newJsonCharArray.Add(jsonString[i]);
                        i++;
                    }
                }
                catch (Exception McEx)
                {
                    Console.WriteLine(McEx.Message);
                }
            }

            string newJsonString = new string(newJsonCharArray.ToArray());
            sr.Close();
            msObj.Close();
            var ident = "";
            string[] aJsonString = newJsonString.Split("\n".ToCharArray());
            newJsonString = aJsonString[0];
            for (var i = 1; i < aJsonString.Length - 1; i++)
            {
                if (aJsonString[i - 1].EndsWith("{"))
                {
                    aJsonString[i - 1] = aJsonString[i - 1].Substring(0, aJsonString[i - 1].Length - 1) + ident + "{";
                    ident += "  ";
                }
                aJsonString[i] = ident + aJsonString[i];
                if (aJsonString[i].EndsWith("}"))
                {
                    ident = ident.Substring(0, ident.Length - 2);
                    aJsonString[i] = aJsonString[i].Substring(0, aJsonString[i].Length - 1) + "\n" + ident + "}";
                }

                newJsonString += ("\n" + aJsonString[i]);
            }

            return newJsonString;
        }

        public virtual void Save(string fileName)
        {
            var strData = ToString();
            try
            {
                var sw = new StreamWriter(fileName);
                sw.Write(strData);
                sw.Close();
            }
            catch (IOException ex)
            {MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);}
            catch (UnauthorizedAccessException ex)
            { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);}
            catch (ArgumentException ex)
            { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (SecurityException ex)
            { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static bool Load<T>(string fileName, out T outPackage, Type type)
        {
            outPackage = default(T);
            try
            {
                return LoadFromStreamReader(new StreamReader(fileName), out outPackage, type);
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return false;
        }

        public static bool Load<T>(Stream stream, out T outPackage, Type type)
        {
            outPackage = default(T);
            try
            {
                return LoadFromStreamReader( new StreamReader(stream), out outPackage, type);
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public static bool LoadFromStreamReader<T>(StreamReader streamReader, out T outPackage, Type type)
        {
            outPackage = default(T);
            try
            {
                var content = streamReader.ReadToEnd();
                streamReader.Close();
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(content));
                var deserializer = new DataContractJsonSerializer(type);
                var thePackage = (T)deserializer.ReadObject(ms);
                outPackage = thePackage;
                return true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OutOfMemoryException ioe)
            {
                MessageBox.Show(ioe.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException ioe)
            {
                MessageBox.Show(ioe.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SerializationException ioe)
            {
                MessageBox.Show(ioe.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

}

