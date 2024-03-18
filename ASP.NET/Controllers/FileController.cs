using Microsoft.AspNetCore.Mvc;
using System.Text;

public class FileController : Controller
{
    [Route("")]
    public IActionResult Index()
    {
        return RedirectToAction("DownloadFile", "File");
    }
    [HttpGet]
    [Route("DownloadFile")]
    public IActionResult DownloadFile()
    {
        return View();
    }

    [HttpPost]
    [Route("DownloadFile")]
    public IActionResult DownloadFilePut(string fileName, string firstName, string lastName)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(firstName).Append("\n").Append(lastName).Append("\n");
           
            using (StreamWriter writer = new StreamWriter(ms, Encoding.UTF8, 1024, true))
            {
                writer.WriteLine(firstName+"\n"+ lastName+"\n");
                writer.Flush();
            }
            
            ms.Position = 0;

            string contentType = "text/plain";

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
            {
                fileName = fileName.Replace(invalidChar.ToString(), "");
            }

            return File(ms.ToArray(), contentType, fileName+".txt");
        }
    }
 
   
    
}