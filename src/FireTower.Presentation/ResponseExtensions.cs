using System.Text;
using Nancy;

namespace FireTower.Presentation
{
    public static class ResponseExtensions
    {
        public static Response WithStringContents(this Response response, string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            response.Contents = s => s.Write(bytes, 0, bytes.Length);
            return response;
        }
    }
}