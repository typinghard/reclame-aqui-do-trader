using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Enums;
using ReclameAquiDoTrader.Business.Interfaces.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TuesPechkin;

namespace ReclameAquiDoTrader.HtmlParaImagem
{
    /// <summary>
    /// Html Converter. Converte HTML string e URLs para bytes de imagem
    /// </summary>
    public class HtmlConverter : IHtmlParaImagem
    {
        private static string toolFilename = "wkhtmltoimage";
        private static string directory;
        private static string directoryTemp;
        private static string toolFilepath;

        private readonly INotificador _notificador;
        public HtmlConverter(INotificador notificador)
        {
            _notificador = notificador;

            directory = AppContext.BaseDirectory;
            if (IsLocalPath(directory))
                directoryTemp = Environment.GetEnvironmentVariable("TEMP");
            else
                directoryTemp = @"D:\local\Temp\";

            //ChecaPlataforma();
        }

        /// <summary>
        /// Converte HTML string para imagem.
        /// </summary>
        /// <param name="html">HTML string</param>
        /// <param name="width">Output document width</param>
        /// <param name="format">Output image format</param>
        /// <param name="quality">Output image quality 1-100</param>
        /// <returns></returns>
        public byte[] FromHtmlString(string html, int width = 1024, EImagemFormato format = EImagemFormato.Jpg, int quality = 100)
        {
            var bytes = new byte[0];
            var filename = Path.Combine(directoryTemp, $"{Guid.NewGuid()}.html");
            var reastreamento = string.Empty;
            try
            {
                reastreamento = "1";
                File.WriteAllText(filename, html);
                reastreamento = "2";
                bytes = FromUrl(filename, width, format, quality);
                reastreamento = "3";
                File.Delete(filename);
                reastreamento = "4";
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao("FromHtmlString", @$"Tivemos um problema ao converter a imagem por html.
                                                                         Erro: {ex.Message}. 
                                                                         InnerException: {ex.InnerException?.Message}
                                                                         Stack Trace: {ex.StackTrace}
                                                                         Rastreamento: {reastreamento}"));
            }

            return bytes;
        }

        private byte[] FromUrl(string url, int width = 1024, EImagemFormato format = EImagemFormato.Jpg, int quality = 100)
        {
            byte[] bytes = new byte[0];
            var rastreamento = string.Empty;
            var imageFormat = format.ToString().ToLower();
            var filename = Path.Combine(directoryTemp, $"{Guid.NewGuid().ToString()}.{imageFormat}");
            try
            {
                rastreamento = "5";
                bytes = TuesPechkinService.ConverterImagem(url, width, imageFormat, quality);
                rastreamento = "6";

                if (bytes.Length > 0)
                    return bytes;
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao("FromUrl", @$"Tivemos um problema ao converter a imagem por url.
                                                                  Erro: {ex.Message}. 
                                                                  InnerException: {ex.InnerException?.Message}
                                                                  Stack Trace: {ex.StackTrace}
                                                                  Url: {url}
                                                                  Caminho a ser Gravado: {filename}
                                                                  Rastreamento: {rastreamento}"));

            }
            finally
            {
                if (File.Exists(filename) && bytes.Length == 0)
                {
                    bytes = File.ReadAllBytes(filename);
                    File.Delete(filename);
                }

                Dispose();
            }

            return bytes;
        }

        private static bool IsLocalPath(string path)
        {
            if (path.StartsWith("http"))
                return false;

            return new Uri(path).IsFile;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                throw new Exception(e.Data);
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao("Process_ErrorDataReceived", @$"Erro: {ex.Message}. 
                                                                                    InnerException: {ex.InnerException?.Message}
                                                                                    Stack Trace: {ex.StackTrace}"));
            }
        }

        private void ChecaPlataforma()
        {
            //Check on what platform we are
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                toolFilepath = Path.Combine(directory, toolFilename + ".exe");

                if (!File.Exists(toolFilepath))
                {
                    var assembly = typeof(HtmlConverter).GetTypeInfo().Assembly;
                    var type = typeof(HtmlConverter);
                    var ns = type.Namespace;

                    using (var resourceStream = assembly.GetManifestResourceStream($"{ns}.{toolFilename}.exe"))
                    using (var fileStream = File.OpenWrite(toolFilepath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
                else
                {
                    var tempPath = Path.Combine(directoryTemp, toolFilename + ".exe");
                    if (!File.Exists(tempPath))
                    {
                        File.Copy(toolFilepath, tempPath, true);
                        toolFilepath = tempPath;
                    }
                }
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                //Check if wkhtmltoimage package is installed on this distro in using which command
                Process process = Process.Start(new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = "/bin/",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = "/bin/bash",
                    Arguments = "which wkhtmltoimage"

                });
                string answer = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(answer) && answer.Contains("wkhtmltoimage"))
                {
                    toolFilepath = "wkhtmltoimage";
                }
                else
                {
                    throw new Exception("wkhtmltoimage does not appear to be installed on this linux system according to which command; go to https://wkhtmltopdf.org/downloads.html");
                }
            }
            else
            {
                //OSX not implemented
                throw new Exception("OSX Platform not implemented yet");
            }
        }

        private void ProcessoConversaoImagem(string args)
        {
            var processStartInfo = new ProcessStartInfo(toolFilepath, args)
            {
                WorkingDirectory = directoryTemp,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.PriorityClass = ProcessPriorityClass.RealTime;
            //using (Process p = Process.GetCurrentProcess())
            //process.PriorityClass = ProcessPriorityClass.High;
            process.WaitForExit();

            //using (var streamReader = new StreamReader(process.StandardOutput.BaseStream))
            //{
            //    var teste1 = streamReader.BaseStream.ReadByte();
            //    var teste = streamReader.ReadToEnd();
            //}

            //var processStartInfo = new ProcessStartInfo
            //{
            //    FileName = toolFilepath,
            //    Arguments = args,
            //    RedirectStandardOutput = true,
            //    UseShellExecute = false
            //};
            //var process = Process.Start(processStartInfo);
            //var output = process.StandardOutput.ReadToEnd();
            //process.WaitForExit();

            //StringBuilder outputBuilder;
            //ProcessStartInfo processStartInfo;
            //Process process;

            //outputBuilder = new StringBuilder();

            //processStartInfo = new ProcessStartInfo();
            //processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //processStartInfo.CreateNoWindow = true;
            //processStartInfo.RedirectStandardOutput = true;
            //processStartInfo.RedirectStandardInput = true;
            //processStartInfo.UseShellExecute = false;
            //processStartInfo.Arguments = args;
            //processStartInfo.FileName = toolFilepath;

            //process = new Process();
            //process.StartInfo = processStartInfo;
            //// enable raising events because Process does not raise events by default
            //process.EnableRaisingEvents = true;
            //// attach the event handler for OutputDataReceived before starting the process
            //process.OutputDataReceived += new DataReceivedEventHandler
            //(
            //    delegate (object sender, DataReceivedEventArgs e)
            //    {
            //        // append the new data to the data already read-in
            //        outputBuilder.Append(e.Data);
            //    }
            //);
            //// start the process
            //// then begin asynchronously reading the output
            //// then wait for the process to exit
            //// then cancel asynchronously reading the output
            //process.Start();
            //process.BeginOutputReadLine();
            //process.WaitForExit();
            //process.CancelOutputRead();

            //// use the output
            //string output = outputBuilder.ToString();
        }

        internal void Dispose()
        {
            GC.Collect();
        }
    }

    public static class TuesPechkinService
    {
        private static ImageToolset ImageToolset = new ImageToolset(new Win64EmbeddedDeployment(new TempFolderDeployment()));

        public static byte[] ConverterImagem(string url, int width, string imageFormat, int quality)
        {
            var converter = GetConverter();
            return converter.Convert(GetDocument(url, width, imageFormat, quality));
        }

        private static IConverter GetConverter()
        {
            try
            {
                ImageToolset = new ImageToolset(new Win64EmbeddedDeployment(new TempFolderDeployment()));
                IConverter converter = new StandardConverter(ImageToolset);
                UnloadImageToolSet();
                return converter;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Dispose();
            }
        }

        private static void UnloadImageToolSet()
        {
            ImageToolset.Unload();
        }

        private static IDocument GetDocument(string url, int width, string imageFormat, int quality)
        {
            IDocument document = new HtmlToImageDocument()
            {
                Format = imageFormat,
                ScreenWidth = width,
                Quality = quality,
                In = url,
            };

            return document;
        }

        internal static void Dispose()
        {
            GC.Collect();
        }
    }
}