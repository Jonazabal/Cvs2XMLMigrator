using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using JonasSoftHouseConverter.Models;

namespace JonasSoftHouseConverter
{
    /// <summary>
    /// Migrating a cvs to xml accourding to rules specified
    /// </summary>
    internal class Migrator
    {
        /// <summary>
        /// Full path to input file
        /// </summary>
        private string _fileName;
        /// <summary>
        /// The delimiter used in csv
        /// </summary>
        private char _delimiter;
        /// <summary>
        /// The root object in XML
        /// </summary>
        public People People { get; set; }
        /// <summary>
        /// The result or response of the migrations
        /// </summary>
        public MigrationResult Result { get; set; }

        /// <summary>
        /// Construct and load input data
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="delimiter"></param>
        public Migrator(string fileName, char delimiter)
        {
            _fileName = fileName;
            _delimiter = delimiter;

            Result = ReadData();

            if (Result.Success)
            {
                Result = WriteXml();
            }
        }

        /// <summary>
        /// Write to XML and validate against schema
        /// </summary>
        /// <returns></returns>
        public MigrationResult WriteXml()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(People));
                var filePath = _fileName.Substring(0, _fileName.LastIndexOf('\\'));

                using (FileStream fileOut = new FileStream(filePath + Path.DirectorySeparatorChar + Constants.OUTPUT_FILE_NAME, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fileOut, People);
                }

                ValidateSchema(filePath + Path.DirectorySeparatorChar + Constants.OUTPUT_FILE_NAME, "." + Path.DirectorySeparatorChar + Constants.SCHEMA_FILE_NAME);
            }
            catch (Exception ex)
            {
                return new MigrationResult { Message = ex.Message, Success = false };
            }

            return new MigrationResult {  Message = "Success", Success = true };
        }

        /// <summary>
        /// Read data from input csv
        /// </summary>
        /// <returns></returns>
        public MigrationResult ReadData()
        {
            try
            {
                var input = ReadCsv(_fileName, _delimiter);
                var currentObject = string.Empty;
                People = new People();
                Person currentPerson = null;
                Family currentFamily = new Family();

                foreach (var item in input)
                {
                    switch (item.First())
                    {
                        case "P":
                            currentPerson = new Person(item);
                            People.Person.Add(currentPerson);
                            currentObject = item.First();
                            break;
                        case "T":
                            if (currentObject == "P")
                            {
                                currentPerson.Phone = new Phone(item);
                            }
                            else if (currentObject == "F")
                            {
                                currentFamily.Phone = new Phone(item);
                            }
                            break;
                        case "F":
                            currentFamily = new Family(item);
                            currentPerson.Family.Add(currentFamily);
                            currentObject = item.First();
                            break;
                        case "A":
                            if (currentObject == "P")
                            {
                                currentPerson.Address = new Address(item);
                            }
                            else if (currentObject == "F")
                            {
                                currentFamily.Address = new Address(item);
                            }
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                return new MigrationResult {  Success = false, Message  = e.Message };
            }
            return new MigrationResult { Success = true, Message = string.Empty };
        }

        /// <summary>
        /// Read the specified csv file using the specified limiter
        /// </summary>
        /// <param name="fileName">Full path and file name</param>
        /// <param name="delimiter">The limiter used in csv</param>
        /// <returns></returns>
        public IList<string[]> ReadCsv(string fileName, char delimiter)
        {
            return File
              .ReadLines(fileName)
              // Ignore empty lines
              .Where(line => !string.IsNullOrEmpty(line))
              // Ignore comments
              .Where(line => !line.StartsWith('#'))
              .Select(line => line.Split(delimiter))
              .ToList();
        }

        /// <summary>
        /// Validates xml against a schema. Throws XmlSchemaValidationException if not valid
        /// </summary>
        /// <param name="xmlPath">Source path to xml to validate</param>
        /// <param name="xsdPath">Path to the xml schema</param>
        public void ValidateSchema(string xmlPath, string xsdPath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);
            xml.Schemas.Add(null, xsdPath);
            xml.Validate(null);
        }
    }
}
