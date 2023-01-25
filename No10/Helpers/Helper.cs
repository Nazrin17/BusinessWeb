namespace No10.Helpers
{
    public static  class Helper
    {
        public static void DeleteFile(string env,string imgname,string path)
        {
            string fullpath=Path.Combine(env,path, imgname);
            File.Delete(fullpath);
        }
    }
}
