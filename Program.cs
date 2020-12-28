using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Artco
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UpdateForm updateForm = new UpdateForm();
            Application.Run(updateForm);

            LoginForm serverSelectForm = new LoginForm();
            Application.Run(serverSelectForm);

            SplashForm splashForm = new SplashForm();
            Application.Run(splashForm);

            MainForm mainForm = new MainForm();
            Application.Run(mainForm);
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            var name = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";

            var resources = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(name));
            if (resources.Count() > 0) {
                string resourceName = resources.First();
                using Stream stream = thisAssembly.GetManifestResourceStream(resourceName);
                if (stream != null) {
                    byte[] assembly = new byte[stream.Length];
                    stream.Read(assembly, 0, assembly.Length);
                    Trace.WriteLine("Dll file load : " + resourceName);
                    return Assembly.Load(assembly);
                }
            }
            return null;
        }
    }
}