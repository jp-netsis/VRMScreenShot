using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace jp.netsis.VRMScreenShot.Editor.BuildProcess
{
    public class WebGLCustomPostProcess : IPostprocessBuildWithReport
    {
	    private const string TEXT_TO_COMMENT_OUT = "unityShowBanner('WebGL builds are not supported on mobile devices.');";
        public int callbackOrder { get { return 0; } }
        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log("MyCustomBuildProcessor.OnPostprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
			if (report.summary.platform != BuildTarget.WebGL)
			{
				return;
			}

			var info = new DirectoryInfo(report.summary.outputPath);
			var files = info.GetFiles("index.html");
			for (int i = 0; i < files.Length; i++)
			{
				var file = files[i];
				var filePath = file.FullName;
				var text = File.ReadAllText(filePath);
				text = text.Replace(TEXT_TO_COMMENT_OUT, "//"+TEXT_TO_COMMENT_OUT);

				Debug.Log("Removing mobile warning from " + filePath);
				File.WriteAllText(filePath, text);
			}
	    }
    }
}